using UnityEngine;
using System.Collections;

public class CloneMachine : MonoBehaviour {

	public GameObject bandMemberToBeCloned;
	//Prefabs for bandmedlemmene skal inn her
	public GameObject vokalist;
	public GameObject gitarist;
	public GameObject bassist;
	public GameObject trommis;
	public BandMember.Role roleImminentClone;
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Cloning(){
		roleImminentClone = GameObject.Find("Player").GetComponent<PlayerInteractions>().bringingRole;
		print (roleImminentClone);
		//Need to know which band member you want to clone here... Oy, design lead!

		if(roleImminentClone == BandMember.Role.Drummer){
			bandMemberToBeCloned = trommis;
			print ("Klona trommis");
		}
		if(roleImminentClone == BandMember.Role.BassPlayer){
			bandMemberToBeCloned = bassist;
			print ("Klona bassist");
		}
		if(roleImminentClone == BandMember.Role.GuitarPlayer){
			bandMemberToBeCloned = gitarist;
			print ("Klona gitarist");
		}
		if(roleImminentClone == BandMember.Role.Singer){
			bandMemberToBeCloned = vokalist;
			print ("Klona vokalist");
		}
        
		GameObject clone = MakeNewBandMemberClone("Mr. M8", 13, roleImminentClone);
	}

    private GameObject MakeNewBandMemberClone(string name, int skill, BandMember.Role role)
    {
        GameObject clone = Instantiate(bandMemberToBeCloned);
        BandMember clonedBandMember = clone.GetComponent<BandMember>();
        clonedBandMember.InitializeBandMember(name, skill, role);

        return clone;
    }
}
