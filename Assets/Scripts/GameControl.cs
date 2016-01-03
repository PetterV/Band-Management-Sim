using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public List<GameObject> waypoints = new List<GameObject>();
	public List<GameObject> stairs = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0;;i++) {
			GameObject wp = GameObject.Find ("Waypoint " + (i + 1));
			if (wp == null)
				break;
			else
				waypoints.Add (wp);
		}
		for (int i = 0;;i++) {
			GameObject stair = GameObject.Find ("Stairs " + (i + 1));
			if (stair == null)
				break;
			else
				stairs.Add (stair);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
