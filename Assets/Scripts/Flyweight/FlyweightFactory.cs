using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
namespace Flyweight
{
public class FlyweightFactory : MonoBehaviour
{
    #region Singleton
    private static FlyweightFactory _instance;
    public static FlyweightFactory Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<FlyweightFactory>();
                if (_instance ==null)
                {
                    _instance = new GameObject("FlyweightFactory").AddComponent<FlyweightFactory>();

                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultCapacity = 10;
    [SerializeField] int maxPoolSize = 100;

    readonly Dictionary<Type, IObjectPool<Flyweight>> pools = new();
    
    public static Flyweight Spawn(FlyweightSettings s) => Instance.GetPoolFor(s)?.Get();
    public static void ReturnToPool(Flyweight f) => Instance.GetPoolFor(f.settings)?.Release(f);

    IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
    {
        IObjectPool<Flyweight> pool;

        if (pools.TryGetValue(settings.GetType(), out pool)) return pool;

        pool = new ObjectPool<Flyweight>(
            settings.Create, 
            settings.OnGet,
            settings.OnRelease,
            settings.OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxPoolSize);
        pools.Add(settings.GetType(), pool);
        return pool;
    }
}
}