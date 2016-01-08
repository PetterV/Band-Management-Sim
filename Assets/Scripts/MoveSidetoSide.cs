using UnityEngine;
using System.Collections;

public class MoveSidetoSide : MonoBehaviour {
	private float newPositionX;
	private float newPositionY;
	public float moveSpeed = 0.5f;
	public int currentFloor = 1;
	public float stairUseDelay = 0.1f;
	private float timeSinceLastStairUse = 0f;
    private Animator animator;
    public bool headedRight;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        headedRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")){
			newPositionX = transform.position.x-moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, transform.position.z);
            this.gameObject.transform.eulerAngles = new Vector3(0, 270.0f, 0);
            /*if (headedRight)
            {
                transform.Rotate(0, -180, 0);
                headedRight = false;
                
            }*/
            animator.SetInteger("WalkMaybe", 2);
		}
		if (Input.GetKey("right")){
			newPositionX = transform.position.x+moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, transform.position.z);
            this.gameObject.transform.eulerAngles = new Vector3(0, 90.0f, 0);
            /*if (!headedRight)
            {
                headedRight = true;
                transform.Rotate(0, 180, 0);
            }*/

            animator.SetInteger("WalkMaybe", 2);
        }

        if (!Input.GetKey("right") && !Input.GetKey("left"))
        {
            animator.SetInteger("WalkMaybe", 0);
            
        }

        

		timeSinceLastStairUse += Time.deltaTime;
	}

	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Stairs" && (Input.GetKeyUp("up") || Input.GetKeyUp("down"))) {
			if (timeSinceLastStairUse > stairUseDelay) {
				if (Input.GetKeyUp ("up"))
					coll.gameObject.SendMessage ("MoveUp", this.gameObject);
				else if (Input.GetKeyUp ("down"))
					coll.gameObject.SendMessage ("MoveDown", this.gameObject);
				timeSinceLastStairUse = 0f;
			}
		}

	}
}
