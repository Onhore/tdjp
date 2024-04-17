using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildSystem
{
    public abstract class Tile : MonoBehaviour
    {
        [SerializeField] public Health health;

        public virtual void Build(Vector3 position)
        {

        }
        public virtual void Death()
        {
            Destroy(gameObject);
        }

    }
}