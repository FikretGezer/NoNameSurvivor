using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class NPC : Test
    {
        private void Start() {
            Debug.Log("<color=cyan>Started...</color>");
        }
        public override void Interact()
        {
            base.Interact();
            Debug.Log("<color=red>NPC is interacted.</color>");
        }
    }
}
