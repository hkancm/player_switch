using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float SmoothTime;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }
    void Update()
    {
        //Vector3 targetPos = target.TransformPoint(new Vector3(0, 40, -30));
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, SmoothTime);
    }
}
