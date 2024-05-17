using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class PlayerStats : MonoBehaviour
{
    public static int Rounds;

    public class PlayerState<T>
	{
		public T Score 
		{
			get
			{
				return score;
			}
			set
			{
				score = value;
				UpdateText();
			}
		}
		private TextMeshProUGUI Text;
		private T score;

        public PlayerState(T startScore, TextMeshProUGUI text)
		{ 
			Text = text;
			Score = startScore;
		}
		void UpdateText()
		{
			Text.text = Score.ToString();
		}

	}

    [Header("Stats")]
    public static PlayerState<int> Gold;
	public static PlayerState<int> Rock;

	[SerializeField] int startGold;
	[SerializeField] int startRock;
	[SerializeField] TextMeshProUGUI GoldText;
	[SerializeField] TextMeshProUGUI RockText;

	void Start ()
	{
		Gold = new PlayerState<int>(startGold, GoldText);
		Rock = new PlayerState<int>(startRock, RockText);
	}
}
