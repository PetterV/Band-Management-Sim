using UnityEngine;
using System.Collections;

public class Purchases : MonoBehaviour {
	public float bamseCost = 200;
	public GameObject gameControl;

	void Start (){
		gameControl = GameObject.FindWithTag("GameController");
	}

	// Use this for initialization
	public void Bamse (){
		//Create bamse ved døra
		print ("Kjøpte bamse!");
		gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - bamseCost;
	}
}
