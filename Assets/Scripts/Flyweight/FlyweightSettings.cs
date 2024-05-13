using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
//[CreateAssetMenu(menuName = "Flyweight/Flyweight Settings")]
public class FlyweightSettings : ScriptableObject
{
    public GameObject prefab;
    //public float despawnDelay = 5f;
    //public float speed = 10f;
    //public FlyweightType type;
    public Flyweight Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.AddComponent<Flyweight>();
        flyweight.settings = this;

        return flyweight;
    }
    public void OnGet(Flyweight f) => f.gameObject.SetActive(true);
    public void OnRelease(Flyweight f) => f.gameObject.SetActive(false);
    public void OnDestroyPoolObject(Flyweight f) => Destroy(f.gameObject);
}
/*public enum FlyweightType
{
    goblin,
    goblin1
}*/
}