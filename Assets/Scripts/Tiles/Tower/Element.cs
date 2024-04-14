using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
namespace BuildSystem
{
    public abstract class Element : MonoBehaviour
    {
        [SerializeField] protected int health;
        public int Health => health;
        public Vector3 nextElementPivot;

        //protected delegate void DelayMethod();
        

        public virtual void Build()
        {}
        public virtual void Interact()
        {}
        
        // protected void Delay(bool condition, DelayMethod method, float cooldown, ref float lastTime)
        // {
        //     if(Time.time < lastTime + cooldown)
        //         return;
        //     if (condition)
        //     {
        //         lastTime = Time.time;
        //         method();
        //     }
        // }
    }
}