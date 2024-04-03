using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildSystem
{
    public abstract class Tile : MonoBehaviour
    {
        [SerializeField] protected int health;
        public int Health => health;

        public virtual void Build(Vector3 position)
        {

        }
        public virtual void GetDamage(int inDamage)
        {

        }
        protected virtual void Destroy()
        {

        }

    }
}