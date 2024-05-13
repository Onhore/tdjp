using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Flyweight
{
public class Flyweight : MonoBehaviour
{
    public virtual FlyweightSettings settings 
    {
        set
        {
            mainSettings=value;
        }
        get
        {
            return mainSettings;
        }
    }
    public FlyweightSettings mainSettings;
    public void OnDestroy()
    {
        //FlyweightFactory.ReturnToPool(this);
    }
}

}
