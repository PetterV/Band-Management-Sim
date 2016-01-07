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

	//Sett spawn point
	public GameObject cloneSpawnPoint;
	Vector3 spawnPos;
	Quaternion spawnRot;
	//

	public float cloneSkill;
	public int cloneMedgjørlighet;
	public string cloneName;


	void Start () {
		cloneSpawnPoint = GameObject.FindWithTag("CloneSpawn");
		spawnPos = cloneSpawnPoint.transform.position;
		spawnRot = cloneSpawnPoint.transform.rotation;
	}

	public void Cloning(){
		roleImminentClone = GameObject.Find("Player").GetComponent<PlayerInteractions>().bringingRole;
		cloneSkill = GameObject.Find("Player").GetComponent<PlayerInteractions>().bringingSkill;
		cloneMedgjørlighet = GameObject.Find("Player").GetComponent<PlayerInteractions>().bringingMedgjørlighet;
		print (roleImminentClone);
		//Need to know which band member you want to clone here... Oy, design lead!
		if(roleImminentClone == BandMember.Role.BassPlayer){
			bandMemberToBeCloned = bassist;
			print ("Klona bassist");
			cloneName = "Bassern";
		}
		if(roleImminentClone == BandMember.Role.GuitarPlayer){
			bandMemberToBeCloned = gitarist;
			print ("Klona gitarist");
			cloneName = "Gittarn";
		}
		if(roleImminentClone == BandMember.Role.Singer){
			bandMemberToBeCloned = vokalist;
			print ("Klona vokalist");
			cloneName = "Sangern";
		}
		if(roleImminentClone == BandMember.Role.Drummer){
			bandMemberToBeCloned = trommis;
			print ("Klona trommis");
			cloneName = "Magnus på trommer";
		}
        
		GameObject clone = MakeNewBandMemberClone(cloneName, cloneSkill, cloneMedgjørlighet, roleImminentClone);
		clone.GetComponent<CloneActivation>().active = false;
		clone.GetComponent<BandMember>().enabled = false;
		clone.GetComponent<Innfallsystemet>().enabled = false;
	}

    private GameObject MakeNewBandMemberClone(string name, float skill, int myMedgjørlighet, BandMember.Role role)
    {
		GameObject clone = (GameObject)Instantiate(bandMemberToBeCloned, spawnPos, spawnRot);
        BandMember clonedBandMember = clone.GetComponent<BandMember>();
        clonedBandMember.InitializeBandMember(name, skill, myMedgjørlighet, role);

        return clone;
    }
}
