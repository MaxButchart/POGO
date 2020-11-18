using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform bean;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = bean.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        //move camera based on rotation of the target
        float desiredAngleY = bean.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredAngleY, 0);
        transform.position = bean.position - (rotation * offset);

        transform.LookAt(bean);
    }
}
