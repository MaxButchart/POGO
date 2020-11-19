using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float UPPER_ROTATION_LIMIT = 75f;
    const float LOWER_ROTATION_LIMIT = -75f;

    public Transform bean;
    public Vector3 offset;

    public float desiredAngleY = 0f;
    public float desiredAngleX = 0f;

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        offset = bean.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        //move camera based on rotation of the target
        desiredAngleY = bean.eulerAngles.y;

        //move camera vertically based on player input
        if(desiredAngleX < UPPER_ROTATION_LIMIT && desiredAngleX > LOWER_ROTATION_LIMIT)
        {
            desiredAngleX += Input.GetAxis("CameraVertical") * rotateSpeed;
        }
        else if (desiredAngleX > UPPER_ROTATION_LIMIT)
        {
            if(Input.GetAxis("CameraVertical") < 0f)
            {
                desiredAngleX += Input.GetAxis("CameraVertical") * rotateSpeed;
            }
        }
        else if (desiredAngleX < LOWER_ROTATION_LIMIT)
        {
            if (Input.GetAxis("CameraVertical") > 0f)
            {
                desiredAngleX += Input.GetAxis("CameraVertical") * rotateSpeed;
            }
        }

        Quaternion rotation = Quaternion.Euler(desiredAngleX, desiredAngleY, 0);
        transform.position = bean.position - (rotation * offset);

        transform.LookAt(bean);
    }
}
