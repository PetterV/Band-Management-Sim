using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HappinessBar : MonoBehaviour {

	public Image happinessBar;
	public float currentHappy = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentHappy = GetComponentInParent<BandMember>().myMedgjørlighet;
		happinessBar.fillAmount = currentHappy / 100;
	}
}
