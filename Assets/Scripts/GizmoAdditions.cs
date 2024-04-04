using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmoAdditions
{
    private const float GIZMO_DISK_THICKNESS = 0.01f;

        public static void DrawGizmoDisk(Vector3 position, Quaternion rotation, float radius)
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;
            //Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.5f); //this is gray, could be anything
            Gizmos.matrix = Matrix4x4.TRS(position, rotation, new Vector3(1, GIZMO_DISK_THICKNESS, 1));
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = oldMatrix;
        }
        public static void DrawCylinder(Vector3 point0, Vector3 point1, float radius, Color drawColor)
        {
            Gizmos.color = drawColor;
            //Vector3 center0 = new Vector3(point0.x, point0.y+radius, point0.z);
            //Vector3 center1 = new Vector3(point1.x, point1.y-radius, point1.z);
            //Gizmos.DrawWireSphere(center0,capsuleRadius);
            //Gizmos.DrawWireSphere(center1,capsuleRadius);

            GizmoAdditions.DrawGizmoDisk(point0, Quaternion.identity, radius);
            GizmoAdditions.DrawGizmoDisk(point1, Quaternion.identity, radius);

            Gizmos.DrawLine(new Vector3(point0.x + radius, point0.y, point0.z), new Vector3(point1.x + radius, point1.y, point1.z));
            Gizmos.DrawLine(new Vector3(point0.x - radius, point0.y, point0.z), new Vector3(point1.x - radius, point1.y, point1.z));

            //Gizmos.DrawLine(new Vector3(point0.x, point0.y + radius, point0.z), new Vector3(point1.x, point1.y + radius, point1.z));
            //Gizmos.DrawLine(new Vector3(point0.x, point0.y - radius, point0.z), new Vector3(point1.x, point1.y - radius, point1.z));

            Gizmos.DrawLine(new Vector3(point0.x, point0.y, point0.z + radius), new Vector3(point1.x, point1.y, point1.z + radius));
            Gizmos.DrawLine(new Vector3(point0.x, point0.y, point0.z - radius), new Vector3(point1.x, point1.y, point1.z - radius));

            Gizmos.DrawLine(new Vector3(point0.x, point0.y, point0.z), new Vector3(point1.x, point1.y, point1.z));

            //Vector3 cubeCenter = new Vector3(point0.x, Mathf.Abs(point0.y-point1.y)/2, point0.z);
            //Vector3 cubeSize = new Vector3(2*radius, Mathf.Abs(point0.y-point1.y), 2*radius);
            //Gizmos.DrawWireCube(cubeCenter, cubeSize);
        }
        public static void DrawCapsule(Vector3 point0, Vector3 point1, float radius, Color drawColor)
        {
            Gizmos.color = drawColor;

            Gizmos.DrawWireSphere(point0,radius);
            Gizmos.DrawWireSphere(point1,radius);


            Gizmos.DrawLine(new Vector3(point0.x + radius, point0.y, point0.z), new Vector3(point1.x + radius, point1.y, point1.z));
            Gizmos.DrawLine(new Vector3(point0.x - radius, point0.y, point0.z), new Vector3(point1.x - radius, point1.y, point1.z));

            Gizmos.DrawLine(new Vector3(point0.x, point0.y, point0.z + radius), new Vector3(point1.x, point1.y, point1.z + radius));
            Gizmos.DrawLine(new Vector3(point0.x, point0.y, point0.z - radius), new Vector3(point1.x, point1.y, point1.z - radius));

            

            Gizmos.DrawLine(new Vector3(point0.x, Mathf.Min(point0.y, point1.y) - radius, point0.z), new Vector3(point1.x, Mathf.Max(point0.y, point1.y) + radius, point1.z));

        }
}
