using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mine : BuildSystem.Tile
{
    [SerializeField] private int value;
    [SerializeField] private float cooldown;
    private float lastTime;
    private void GetRock()
    {
        PlayerStats.Rock.Score += value;
    }
    void Update()
    {
        UnityExtensions.Cooldowned(true, GetRock, cooldown, ref lastTime);
    }
    public override void Death()
    {
        GridController.Instance.grid.GetCellFromWorldPos(transform.position).Tile = null;
        GridController.Instance.UpdateStaticFields();
        base.Death();
    }
}
