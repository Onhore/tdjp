using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cost
{
    public int gold;
    public int mana;
    public int people;
    public int rock;

    Cost (int _gold, int _mana, int _people, int _rock)
    {
        gold = _gold;
        mana = _mana;
        people = _people;
        rock = _rock;
    } 

    public Cost Half()
    {
        return new Cost(gold/2,mana/2,people/2,rock/2);
    }
}

[System.Serializable]
public class TileBlueprint
{
    public GameObject prefab;
	public Cost cost;

}
