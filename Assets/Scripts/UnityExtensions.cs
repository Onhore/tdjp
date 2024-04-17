using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions
{

/// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public delegate void CooldownMethod();
    public delegate void CooldownHit(GameObject hitObject);
    public static void Cooldowned(bool condition, CooldownMethod method, float cooldown, ref float lastTime)
    {
            if(Time.time < lastTime + cooldown)
                return;
            if (condition)
            {
                lastTime = Time.time;
                method();
            }
    }
    public static void Cooldowned(bool condition, CooldownHit method, GameObject hitObject, float cooldown, ref float lastTime)
    {
            if(Time.time < lastTime + cooldown)
                return;
            if (condition)
            {
                lastTime = Time.time;
                method(hitObject);
            }
    }
    // public static void SetPosition(Transform _transform, Vector3 position)
    // {

    // }
}
