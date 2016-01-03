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
            GameObject clone = MakeNewBandMemberClone("Mr. M8", 13, BandMember.Role.BassPlayer);
            
		}
	}

    private GameObject MakeNewBandMemberClone(string name, int skill, BandMember.Role role)
    {
        GameObject clone = Instantiate(bandMemberToBeCloned);
        BandMember clonedBandMember = clone.GetComponent<BandMember>();
        clonedBandMember.InitializeBandMember(name, skill, role);

        return clone;
    }
}
