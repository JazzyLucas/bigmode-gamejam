using System.Collections;
using UnityEngine;
using FMOD.Studio;
using BigModeGameJam.Core;


namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Handles basic player movement using rigidbody.
    /// NOTE: Rigidbody must be attached with no friction, no gravity and frozen rotation.
    /// </summary>
    [RequireComponent(typeof(PlayerRefs))]
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// Multiplier to collider extents to detect ground.
        /// </summary>
        private const float GROUND_LENIENCE = 1.05f;
        /// <summary>
        /// Uncrouch animation will temporarily hold until there is this amount of clearance.
        /// </summary>
        private const float CROUCH_HEADROOM = 0.25f;
        /// <summary>
        /// Movement attributes for human character
        /// </summary>
        private const float HUMAN_ACC = 8, HUMAN_SPD = 12, HUMAN_JUMP = 10, HUMAN_AIR_FRIC = 10,
        HUMAN_GRND_FRIC = 35, HUMAN_GRAV = 25;
        /// <summary>
        /// Movement attributes for electricity character
        /// </summary>
        private const float ELEC_ACC = 120, ELECT_SPD = 40, ELEC_JUMP = 35, ELEC_AIR_FRIC = 15,
        ELEC_GRND_FRIC = 95, ELEC_GRAV = 50;
        private const float LOOK_RANGE = 89.5f; // Going to the fll 90deg causes issues in third person
        private const float CROUCH_ANIMATION_PERIOD = 0.25f, CROUCH_SPEED_MULTIPLIER = 0.4f;

        // SOUND CONSTANTS
        private const float FOOTSTEP_SPEED_THRESH = 2;

        public enum PlayerType : int
        {
            Human = 1,
            Electric = 2,
            Custom = 3
        }

        public PlayerType playerType = PlayerType.Custom;
        [Header("Movement Attributes")]

        public float acceleration = 10;
        public float maxSpeed = 10, jumpVelocity = 5, airFriction = 1,
        groundFriction = 10, gravity = 9.8f;
        new private Rigidbody rigidbody;
        new private CapsuleCollider collider;
        private Dash dash;
        private PlayerRefs playerRefs;
        [SerializeField] new private Camera camera;
        // Referencing camera's rotation causes issues for some reason. keep track of it here instead.
        private float camX;
        /// <summary>
        /// Prevents movement while true. Should only be touced by Stun() and its coroutine.
        /// Also affects gravity.
        /// </summary>
        private bool stunned;
        private Coroutine stunCoroutine;
        private bool grounded;

        private bool crouched = false;
        private float initColliderHeight = 2;
        private float crouchColliderHeight = 1;
        private float initCameraHeight = 0.65f;
        private float crouchCameraHeight = 0.325f;
        private Coroutine crouchAnimation;

        public void Crouch()
        {
            if (crouched) return;
            if (crouchAnimation != null)
            {
                StopCoroutine(crouchAnimation);
            }
            crouched = true;
            crouchAnimation = StartCoroutine(CrouchAnimation(crouchColliderHeight, crouchCameraHeight, CROUCH_ANIMATION_PERIOD));
        }

        public void Uncrouch()
        {
            if (!crouched) return;
            if (crouchAnimation != null)
            {
                StopCoroutine(crouchAnimation);
            }
            crouched = false;
            crouchAnimation = StartCoroutine(CrouchAnimation(initColliderHeight, initCameraHeight, CROUCH_ANIMATION_PERIOD));
        }

        public void Dash(Vector3 dir)
        {
            if (playerType == PlayerType.Human || dash == null) return;
            dash.StartDash(dir);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Dash, this.transform.position);
        }

        /// <summary>
        /// Checks if entity is touching (or close to) ground using raycasts.
        /// Also handles ramps and dash cooldowns.
        /// </summary>
        /// <returns>True if entity is grounded. False otherwise.</returns>
        public bool IsGrounded()
        {
            float distToBottom = collider.bounds.extents.y * GROUND_LENIENCE - collider.bounds.extents.x;
            if (Physics.SphereCast(transform.position, collider.bounds.extents.x, -transform.up, 
                    out RaycastHit hit, distToBottom))
            {
                if (dash != null) dash.Replenish();
                return true;
            }
            return false;
        }

        public void Jump(bool force = false)
        {
            if ((!grounded && !force) || rigidbody.linearVelocity.y > jumpVelocity) return;
            rigidbody.linearVelocity = new Vector3(
                rigidbody.linearVelocity.x, jumpVelocity, rigidbody.linearVelocity.z
            );
            if(AudioManager.instance && jumpVelocity > 0)
                AudioManager.instance.PlayOneShot(FMODEvents.instance.JumpSound, this.transform.position);
            grounded = false;
        }

        /// <summary>
        /// Controls player camera looking and rotation.
        /// </summary>
        /// <param name="delta">Difference in rotation.</param>
        public void Look(Vector2 delta)
        {
            if (!enabled) return;
            // Look left and right
            transform.Rotate(0, delta.x, 0);
            // Look up and down
            camX -= delta.y;
            camX = Mathf.Clamp(camX, -LOOK_RANGE, LOOK_RANGE);
            camera.transform.localRotation = Quaternion.Euler(
                camX, 0, 0
            );
        }

        /// <summary>
        /// Moves player on xz plane
        /// </summary>
        /// <param name="dir">Direction to move relative to forward</param>
        public void Move(Vector3 dir)
        {
            if (stunned) return;
            if (!enabled)
            {
                if (playerRefs.electricMode != null && playerRefs.electricMode.enabled)
                {
                    playerRefs.electricMode.Move(dir);
                }
                return;
            }
            dir = dir.normalized;
            dir = transform.TransformDirection(dir);

            // Add friction to movement acceleration for tighter controls
            float totalAcc = acceleration;
            totalAcc += grounded ? groundFriction : airFriction;

            // new vector needed in order to ignore y axis
            Vector2 hVel = new Vector2(
                rigidbody.linearVelocity.x + (dir.x * totalAcc * Time.deltaTime),
                rigidbody.linearVelocity.z + (dir.z * totalAcc * Time.deltaTime)
            );

            // Handle max speed
            float targetSpeed = maxSpeed;
            if (crouched)
            {
                targetSpeed *= CROUCH_SPEED_MULTIPLIER;
            }
            hVel = Vector2.MoveTowards(hVel,
                Vector2.ClampMagnitude(hVel, targetSpeed),
                totalAcc * Time.deltaTime
            );

            // Set velocity to new calculated value
            rigidbody.linearVelocity = new Vector3(
                hVel.x, rigidbody.linearVelocity.y, hVel.y
            );
        }

        

        public void Stun(float stunTime)
        {
            // prevent stun overlap
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(StunCoroutine(stunTime));
        }

        private void ApplyAttributes()
        {
            switch (playerType)
            {
                case PlayerType.Human:
                    acceleration = HUMAN_ACC;
                    maxSpeed = HUMAN_SPD;
                    jumpVelocity = HUMAN_JUMP;
                    airFriction = HUMAN_AIR_FRIC;
                    groundFriction = HUMAN_GRND_FRIC;
                    gravity = HUMAN_GRAV;
                    return;
                case PlayerType.Electric:
                    acceleration = ELEC_ACC;
                    maxSpeed = ELECT_SPD;
                    jumpVelocity = ELEC_JUMP;
                    airFriction = ELEC_AIR_FRIC;
                    groundFriction = ELEC_GRND_FRIC;
                    gravity = ELEC_GRAV;
                    return;
            }
        }

        //movement audio

        private EventInstance Footstep;
        private void Start()
        {
            if (AudioManager.instance && playerType == PlayerType.Human)
                Footstep = AudioManager.instance.CreateEventInstance(FMODEvents.instance.FootstepNormal);
            else if (AudioManager.instance && playerType == PlayerType.Electric)
                Footstep = AudioManager.instance.CreateEventInstance(FMODEvents.instance.FootstepElectric);
        }

        private void ApplyFriction()
        {
            float delta = Time.deltaTime;
            if (grounded)
            {
                delta *= groundFriction;
            }
            else
            {
                delta *= airFriction;
            }
            // Horizontal plane only. Do not affect y axis.
            Vector3 target = Vector3.MoveTowards(
                rigidbody.linearVelocity, Vector3.zero, delta);
            rigidbody.linearVelocity = new Vector3(
                target.x, rigidbody.linearVelocity.y, target.z
            );
        }

        private void ApplyGravity()
        {
            // Continue to use IsGrounded method here to prevent floating
            if (IsGrounded() || stunned) return;
            rigidbody.linearVelocity += Vector3.down * gravity * Time.deltaTime;
        }

        private void Awake()
        {
            playerRefs = GetComponent<PlayerRefs>();
            rigidbody = playerRefs.rigidbody;
            collider = playerRefs.collider;
            initColliderHeight = collider.height;
            initCameraHeight = camera.transform.localPosition.y;
            dash = playerRefs.dash;
            ApplyAttributes();
        }

        private IEnumerator StunCoroutine(float stunTime)
        {
            stunned = true;
            yield return new WaitForSeconds(stunTime);
            stunned = false;
            stunCoroutine = null;
        }

        private IEnumerator CrouchAnimation(float targetHeight, float targetCameraHeight, float period)
        {
            float colliderRate = Mathf.Abs((targetHeight - collider.height) / period);
            float cameraRate = Mathf.Abs((targetCameraHeight - camera.transform.localPosition.y) / period);
            while (collider.height != targetHeight)
            {
                yield return new WaitForEndOfFrame();
                if(targetHeight > collider.height && 
                    Physics.SphereCast(transform.position, collider.bounds.extents.x, transform.up, 
                    out RaycastHit hit, collider.bounds.extents.y + CROUCH_HEADROOM - collider.bounds.extents.x))
                {
                    continue;
                }
                collider.height = Mathf.MoveTowards(collider.height, targetHeight, colliderRate * Time.deltaTime);
                camera.transform.localPosition = new Vector3(
                    camera.transform.localPosition.x,
                    Mathf.MoveTowards(camera.transform.localPosition.y, targetCameraHeight, cameraRate * Time.deltaTime),
                    camera.transform.localPosition.z
                );
                // Correct gap
                if (grounded)
                {
                    Vector3 translate = Vector3.up * colliderRate * Time.deltaTime;
                    if (crouched)
                    {
                        translate *= -1;
                    }
                    transform.Translate(translate);
                }
            }
            camera.transform.localPosition = new Vector3(
                    camera.transform.localPosition.x,
                    targetCameraHeight,
                    camera.transform.localPosition.z
                );
            crouchAnimation = null;
        }

        private void Update()
        {
            ApplyFriction();
            ApplyGravity();
            UpdateSound();
        }

        private void OnCollisionEnter(Collision collision)
        {
            grounded = IsGrounded();
        }

        private void OnCollisionExit(Collision collision)
        {
            // Only check for ground when leaving ground to avoid false positives
            if(grounded)
                grounded = IsGrounded();
        }

        private void OnDisable()
        {
            Footstep.stop(STOP_MODE.ALLOWFADEOUT);
        }

        private void UpdateSound()
        {
            //start footsteps event if player has an x velocity and is on the ground
            if (grounded && new Vector2(rigidbody.linearVelocity.x, rigidbody.linearVelocity.z).magnitude >= FOOTSTEP_SPEED_THRESH)
            {
                //get playback state
                PLAYBACK_STATE playbackState;
                Footstep.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    Footstep.start();
                }
                
            }
            //otherwise, stop the footsteps event
            else
            {
                Footstep.stop(STOP_MODE.ALLOWFADEOUT);
                
            }
        }
    }
}