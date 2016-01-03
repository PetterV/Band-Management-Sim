using UnityEngine;
using System.Collections.Generic;

public class Stairs : MonoBehaviour {
	public int floor;
	public float floorDifference = 4f;
	public Stairs upExit;
	public Stairs downExit;
	private GameControl gameControl;
	private List<GameObject> stairs = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		this.gameControl = GameObject.Find ("GameControl").GetComponent<GameControl>();
		this.stairs = gameControl.stairs;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveDown(GameObject toMove){
		toMove.transform.position = downExit.gameObject.transform.position;
	}

	public void MoveUp(GameObject toMove){
        print("Moving up!");
		toMove.transform.position = upExit.gameObject.transform.position;
	}
}
