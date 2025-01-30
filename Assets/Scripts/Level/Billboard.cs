using UnityEngine;

namespace BigModeGameJam.Level
{
    /// <summary>
    /// Forces object to look at camera
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            if(PlayerRefs.curCam == null) return;
            transform.forward = PlayerRefs.curCam.transform.forward;
        }
    }
}
