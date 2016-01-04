using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float speed;
    public GameObject larget;
    public float offsetMaxOut;
    public float offsetMaxIn;
    public float offsetSpeed;

    void Start()
    {
        larget = GameObject.FindGameObjectWithTag("CameraTarget");
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 30) 
        {
            GetComponent<Camera>().fieldOfView -= speed;
            float offsetY = larget.transform.position.y - offsetSpeed;
            Vector3 largetPos = new Vector3(larget.transform.position.x, offsetY, larget.transform.position.z);
            larget.transform.position = largetPos;
                   }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 60)
        {
            GetComponent<Camera>().fieldOfView += speed;
            float offsetY = larget.transform.position.y + offsetSpeed;
            Vector3 largetPos = new Vector3(larget.transform.position.x, offsetY, larget.transform.position.z);
            larget.transform.position = largetPos;
        }

    }
}   