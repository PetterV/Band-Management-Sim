using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayMoney : MonoBehaviour {

	private float currentMoney;
	private string iHaveMoney;
	private Text moneyDisplay;

	// Use this for initialization
	void Start () {
		currentMoney = GameObject.FindWithTag("GameController").GetComponent<GameControl>().penger;
		moneyDisplay = GameObject.FindWithTag("MoneyDisplay").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		currentMoney.ToString(iHaveMoney);
		moneyDisplay.text = iHaveMoney;
	}
}
