using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField] protected uint health;
    [SerializeField] protected float speed;
    [SerializeField] protected uint damage;
    public FlowFieldController GridController;
    public uint Health { get { return health; } }
    public float Speed { get { return speed; } }
    public uint Damage {  get { return damage; } }

    [SerializeField] protected Rigidbody rb;
    protected virtual void Move(Vector2 direction)
    { 
    
    }
    protected virtual void Hit()
    { 
    
    }
    public virtual void GetDamage(int inDamage)
    { 
    
    }
    public virtual void FaceTo(Vector2 direction)
    {
        if (direction == Vector2.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    
}
