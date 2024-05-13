using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Goblin Settings")]
public class GoblinSettings : EntitySettings
{
    public LayerMask hitableLayer;
    public float cooldownAttack;
    public float smoothTimeMovement = 0.25f;
}
