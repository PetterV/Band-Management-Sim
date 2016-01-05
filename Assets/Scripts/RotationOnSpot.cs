﻿using UnityEngine;
using System.Collections;

public class RotationOnSpot : MonoBehaviour
{
    public float speed = 10f;


    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}