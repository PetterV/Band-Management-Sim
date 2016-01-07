using UnityEngine;
using System.Collections;

public class TrackBandMember : MonoBehaviour {

	public GameObject bandMember;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = bandMember.transform.position;	
	}
}
