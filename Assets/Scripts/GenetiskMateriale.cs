using UnityEngine;
using System.Collections;

public class GenetiskMateriale : MonoBehaviour {

	public bool beingCarried = false;
	public float skillForCloning = 0;
	public float medgjørlighetForCloning = 0;
	public BandMember.Role roleForCloning;
	public GameObject hand;
	public float timerIncrease = 1f;
	public float timerDestroyAfter = 40f;
	public float lifeTimer = 0f;

	void Start (){
		hand = GameObject.FindWithTag("HandAttachment");
	}

	// Update is called once per frame
	void Update () {
		if (beingCarried == true){
			lifeTimer = 0f;
			this.transform.position = hand.transform.position;
		}
		if (beingCarried == false){
			float timerStep = timerIncrease * Time.deltaTime;
			lifeTimer = lifeTimer + timerStep;
			if (lifeTimer > timerDestroyAfter)
				Destroy(this.gameObject);
		}
	}
}
