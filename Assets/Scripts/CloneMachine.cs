using UnityEngine;
using System.Collections;

public class CloneMachine : MonoBehaviour {

	public GameObject bandMemberToBeCloned;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider coll){
		if(coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			//Need to know which band member you want to clone here... Oy, design lead!
			print("band member cloned!");
			GameObject clone = Instantiate(bandMemberToBeCloned);
			bandMemberToBeCloned.GetComponent<BandMember>().skill = 10;
		}
	}
}
