using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace BuildSystem
{
    public class Archery : Top
    {
        [SerializeField] private float BlindRadius;
        [SerializeField] private float Radius;
        [SerializeField] private float Height;
        [SerializeField] private float BlindHeight;
        [SerializeField] private float Coefficient;
        private float blindRadius => BlindRadius*Mathf.Max(coefficient/2, 1);
        private float radius => Radius*coefficient;
        private float height => Height;
        private float blindHeight => BlindHeight*Mathf.Max(coefficient/2, 1);

        private float coefficient => transform.localPosition.y*Coefficient;
        [SerializeField] private LayerMask EnemyMask;



        private bool debugField = false;
        public override void Interact()
        {
            base.Interact();
            debugField = GetCollides(EnemyMask).Length != 0;
            
        }

        public Collider[] GetCollides(LayerMask layerMask)
        {
            
            Vector3 point0 = transform.parent.position;
            Vector3 point1 = transform.position + new Vector3(0,height,0);
            Vector3 blindPoint = transform.parent.position + new Vector3(0,blindHeight,0);
            Collider[] collides = Physics.OverlapCapsule(point0, point1, radius, layerMask);
            Collider[] ignoreCollides = Physics.OverlapCapsule(point0, blindPoint, blindRadius, layerMask);
            IEnumerable<Collider> result = from col in collides.Except(ignoreCollides) select col;
            
            return result.ToArray();
        }
        // Debug
        private void OnDrawGizmos()
        {
            GizmoAdditions.DrawCapsule(transform.parent.position, transform.position + new Vector3(0,height,0), radius, debugField? Color.green: Color.blue);
            GizmoAdditions.DrawCapsule(transform.parent.position, transform.parent.position + new Vector3(0,blindHeight,0), blindRadius, Color.red);
        }
           
    }
}