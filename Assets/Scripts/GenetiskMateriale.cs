using UnityEngine;
using System.Collections;

public class GenetiskMateriale : MonoBehaviour {

	public bool beingCarried = false;
	public float skillForCloning = 0;
	public int medgjørlighetForCloning = 0;
	public BandMember.Role roleForCloning;

	// Update is called once per frame
	void Update () {
		if (beingCarried == true){
			transform.position = GameObject.Find("Player").transform.position;
		}
	}
}
