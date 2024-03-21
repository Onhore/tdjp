using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goblin : Entity
{
    public float smoothTimeMovement = 0.25f;
    Vector3 velocity = Vector3.zero;
    protected override void Move(Vector2 direction)
    {
        //rb.AddForce(new Vector3(direction.x, 0 ,direction.y) * speed, ForceMode.Force);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + Vector3.Scale(transform.forward.normalized, (Vector3.forward + Vector3.right)) * speed, ref velocity, smoothTimeMovement);
        //rb.velocity += new Vector3(direction.x, 0, direction.y) * speed;
        //transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y));
        FaceTo(direction);
    }
    public override void GetDamage(int inDamage)
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        Move(GridController.curFlowField.GetCellFromWorldPos(transform.position).bestDirection.Vector);
    }
}
