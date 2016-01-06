using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Purchases : MonoBehaviour {
	public float bamseCost = 400f;
	public float medisinCost = 1000f;
	public float matCost = 300f;
	public float drekkeCost = 600f;
	public float caymanCost = 10000f;
	public GameObject bamse;
	public GameObject medisin;
	public GameObject mat;
	public GameObject drekke;
	//Sett dette der hvor bestillinger skal dukke opp.
	public GameObject deliveryPoint;
	Vector3 deliveryPos;
	Quaternion deliveryRot;
	//
	//Purchase limit
	private float limitTimer;
	public float startLimitTimer = 25f;
	private float limitTimerStep;
	public Image limitTimerImage;
	//

	public GameObject gameControl;

	void Start (){
		gameControl = GameObject.FindWithTag("GameController");
		deliveryPos = deliveryPoint.transform.position;
		deliveryRot = deliveryPoint.transform.rotation;
	}

	void Update (){
		limitTimerStep = 1 * Time.deltaTime;
		limitTimer = limitTimer - limitTimerStep;
		limitTimerImage.fillAmount = limitTimer / startLimitTimer;
	}

	// Use this for initialization
	public void Bamse (){
		//Create bamse ved døra
		if (limitTimer <= 0){
			print ("Kjøpte bamse!");
			gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - bamseCost;
			Instantiate(bamse, deliveryPos, deliveryRot);
			limitTimer = startLimitTimer;
		}
	}

	public void Medisin (){
		if (limitTimer <= 0){
			print ("Kjøpte medisin!");
			gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - medisinCost;
			Instantiate(medisin, deliveryPos, deliveryRot);
			limitTimer = startLimitTimer;
		}
	}

	public void Mat () {
		if (limitTimer <= 0){
			print ("Kjøpte mat");
			gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - matCost;
			Instantiate(mat, deliveryPos, deliveryRot);
			limitTimer = startLimitTimer;
		}
	}

	public void Drekke (){
		if (limitTimer <= 0){
			print ("Kjøpte drekke");
			gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - drekkeCost;
			Instantiate(drekke, deliveryPos, deliveryRot);
			limitTimer = startLimitTimer;
		}
	}

	public void Cayman (){
		if (limitTimer <= 0){
			print ("Overførte penger.");
			gameControl.GetComponent<GameControl>().penger = gameControl.GetComponent<GameControl>().penger - caymanCost;
			gameControl.GetComponent<GameControl>().caymanKonto = gameControl.GetComponent<GameControl>().caymanKonto + caymanCost;
			limitTimer = startLimitTimer;
		}
	}
}
