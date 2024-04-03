using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BuildSystem
{
    public class Base : Element
    {
        public override void Interact()
        {
            base.Interact();
            //Debug.Log("Base Interact");
        }
    }
}