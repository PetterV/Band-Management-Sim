using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayMoney : MonoBehaviour {

	private float currentMoney;
	private Text moneyDisplay;

	// Use this for initialization
	void Start () {
		moneyDisplay = GameObject.FindWithTag("MoneyDisplay").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		currentMoney = GameObject.FindWithTag("GameController").GetComponent<GameControl>().penger;
		string displayMoney = "" + currentMoney;
		moneyDisplay.text = displayMoney;
	}
}
