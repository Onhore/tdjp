using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goblin : Entity
{
    public float smoothTimeMovement = 0.25f;
    [SerializeField] private LayerMask hitableLayer;
    [SerializeField] private float cooldownAttack;
    Vector3 velocity = Vector3.zero;
    private float lastTimeAttack;

    private Vector3 direction;
    private GameObject target;

    protected override void Move(Vector2 direction)
    {
        //rb.AddForce(new Vector3(direction.x, 0 ,direction.y) * speed, ForceMode.Force);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + Vector3.Scale(transform.forward.normalized, (Vector3.forward + Vector3.right)) * speed, ref velocity, smoothTimeMovement);
        
        //rb.velocity += new Vector3(direction.x, 0, direction.y) * speed;
        //transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y));
        
        FaceTo(direction);
    }
    
    protected void Hit(GameObject enemy)
    {
        enemy.GetComponent<IDamagable>().Damage(damage);
    }
    public override void Death()
    {
        base.Death();
        PlayerStats.Gold.Score += 10; 
    }
    private void Test(int a)
    {}

    void Start()
    {
        
    }

    void Update()
    {
        if (GridController == null)
            return;
        Vector2 dir = GridController.curFlowField.GetCellFromWorldPos(transform.position).bestDirection.Vector;
        direction = new Vector3(dir.x, 0, dir.y);
    
        target = GetTarget(hitableLayer, direction, 0.5f);
        if (GridController != null)
            Move(dir);
        if (target)
            UnityExtensions.Cooldowned(true, new UnityExtensions.CooldownHit(Hit), target, cooldownAttack, ref lastTimeAttack);
    }
    public GameObject GetTarget(LayerMask layerMask, Vector3 direction, float length)
    {
            
        Ray position = new Ray(transform.position + new Vector3(0, 0.5f, 0), direction);
        //Debug.DrawRay(transform.position+new Vector3(0, 0.5f, 0), direction, Color.red);
        if (Physics.Raycast(position, out var hitInfo, length, layerMask) && layerMask.Contains(hitInfo.transform.gameObject.layer))
        {
            return target = hitInfo.transform.gameObject;
        }
            
        return null;
    }
    // private void OnCollisionStay(Collision collision)
    //     {
    //         foreach (ContactPoint contact in collision.contacts)
    //         {
    //                 if(hitableLayer.Contains(contact.otherCollider.gameObject.layer))
    //                 {
    //                     UnityExtensions.Cooldowned(true, new UnityExtensions.CooldownHit(Hit), contact.otherCollider.gameObject, cooldownAttack, ref lastTimeAttack);
    //                 }
    //         }
    //     }

}
