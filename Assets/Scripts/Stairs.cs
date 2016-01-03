using UnityEngine;
using System.Collections.Generic;

public class Stairs : MonoBehaviour {
	public int floor;
	public float floorDifference = 1f;
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
		float newPositionY = transform.position.y-floorDifference;
		float newPositionX = this.stairs [this.floor - 2].transform.position.x;
		toMove.transform.position = new Vector3 (newPositionX, newPositionY, 0);
	}

	public void MoveUp(GameObject toMove){
		float newPositionY = transform.position.y + floorDifference;
		float newPositionX = this.stairs [this.floor].transform.position.x;
		toMove.transform.position = new Vector3 (newPositionX, newPositionY, 0);
	}
}
