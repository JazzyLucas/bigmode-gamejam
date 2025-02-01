using BigModeGameJam.Core;
using System.Runtime.CompilerServices;
using UnityEngine;
using FMODUnity;
using System.Data.SqlTypes;

namespace BigModeGameJam.Level.Interactables
{
    public class DestroyOnInteractable : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            base.Interact(interacter);
            Destroy(gameObject);
        }
    }
}
