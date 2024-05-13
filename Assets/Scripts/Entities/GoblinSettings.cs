using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Goblin Settings")]
public class GoblinSettings : ScriptableObject
{
    public GameObject prefab;
    //public float despawnDelay = 5f;
    //public float speed = 10f;
    //public FlyweightType type;
    public Goblin Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.AddComponent<Goblin>();
        flyweight.settings = this;

        return flyweight;
    }
    public void OnGet(Goblin f) => f.gameObject.SetActive(true);
    public void OnRelease(Goblin f) => f.gameObject.SetActive(false);
    public void OnDestroyPoolObject(Goblin f) => Destroy(f.gameObject);
    public LayerMask hitableLayer;
    public float cooldownAttack;
    public float smoothTimeMovement = 0.25f;
    public float speed;
    public int damage;
}
