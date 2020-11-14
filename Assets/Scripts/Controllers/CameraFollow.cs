﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desieredPosition = new Vector3(target.position.x, target.position.y, -100) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -100), desieredPosition, smothSpeed);
        transform.position = smoothedPosition;
    }
}
