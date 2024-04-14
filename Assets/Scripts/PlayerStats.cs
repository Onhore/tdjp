using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public static int Gold;
	public int startGold = 400;

	public static int Rock;
	public int startRock = 20;

	public static int Mana;
	public int startMana = 20;

    public static int People;
	public int startPeople = 20;


	void Start ()
	{
		Gold = startGold;
		Rock = startRock;
        Mana = startMana;
        People = startPeople;

	}
}
