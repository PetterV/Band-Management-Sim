using UnityEngine;
using System.Collections.Generic;

public class Stairs : MonoBehaviour {
	public int floor;
	public float floorDifference = 4f;
	public Stairs upExit;
	public Stairs downExit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveDown(GameObject toMove){
        if(downExit != null)
		    toMove.transform.position = downExit.gameObject.transform.position;
	}

	public void MoveUp(GameObject toMove){
        if(upExit != null)
		    toMove.transform.position = upExit.gameObject.transform.position;
	}
}
