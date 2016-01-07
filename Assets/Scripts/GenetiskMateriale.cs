using UnityEngine;
using System.Collections;

public class GenetiskMateriale : MonoBehaviour {

	public bool beingCarried = false;
	public float skillForCloning = 0;
	public int medgjørlighetForCloning = 0;
	public BandMember.Role roleForCloning;
	public GameObject hand;

	void Start (){
		hand = GameObject.FindWithTag("HandAttachment");
	}

	// Update is called once per frame
	void Update () {
		if (beingCarried == true){
			this.transform.position = hand.transform.position;
		}
	}
}
