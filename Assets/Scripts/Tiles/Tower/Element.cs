using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
namespace BuildSystem
{
    public abstract class Element : MonoBehaviour
    {
        [SerializeField] protected int health;
        public int Health => health;
        public Vector3 nextElementPivot;
        public virtual void Interact()
        {

        }
    }
}