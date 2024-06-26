using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public abstract class Entity : MonoBehaviour
{

    [SerializeField] public Health health;
    //public EntitySettings settings;
    public FlowFieldController gridController;
    //public int Health { get { return health; } }
    //public float Speed { get { return speed; } }
    //public int Damage {  get { return damage; } }

    [SerializeField] protected Rigidbody rb;
    void Awake() => gridController = GridController.Instance.GetComponent<FlowFieldController>();
    protected virtual void Move(Vector2 direction)
    { 
    
    }
    protected virtual void Hit()
    { 
    
    }
    public virtual void Death()
    {
        //Destroy(gameObject);
    }

    public virtual void FaceTo(Vector2 direction)
    {
        if (direction == Vector2.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    
}
