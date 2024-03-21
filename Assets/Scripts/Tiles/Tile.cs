using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected uint health;
    
    public uint Health { get { return health; } }
    // Start is called before the first frame update
    public virtual void Build()
    { 
    
    }
    public virtual void GetDamage(int inDamage)
    {

    }
    protected virtual void Destroy()
    {
        
    }

}
