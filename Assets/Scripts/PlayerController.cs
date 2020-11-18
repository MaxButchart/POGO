using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    const float MAX_VELOCITY = 1;

    public float bouncePower;
    public Rigidbody rigidbody;
    
    public float smooth = 5.0f;
    public float tiltAngle = 50.0f;

    public float tiltX;
    public float tiltY;
    public float tiltZ;

    public float xAngle, zAngle;

    public float rotateSpeed;

    //must be less than 1 and greater than 0 to function
    public float decel;

    public float vert;
    public float hori;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //rotate the player based on the left thumbs
        tiltZ = Input.GetAxis("MoveHorizontal") * tiltAngle * -1f;
        
        tiltX = Input.GetAxis("MoveVertical") * tiltAngle * -1f;

        // rotate the player based on right thumb stick
        tiltY += (Input.GetAxis("CameraHorizontal") * rotateSpeed);

        Quaternion target = Quaternion.Euler(tiltX, tiltY, tiltZ);
        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        //calculate the x and z angle to make the player move in the direction the camera is pointing
        xAngle = bouncePower * ((tiltX / tiltAngle) * (float)Math.Cos(tiltY * Math.PI / 180) + ((tiltZ / tiltAngle) * (float)Math.Sin(tiltY * Math.PI / 180))); 
        zAngle = bouncePower * ((tiltX / tiltAngle) * (float)Math.Sin(tiltY * Math.PI / 180) + ((tiltZ / tiltAngle) * (float)Math.Cos(tiltY * Math.PI / 180) * -1f)); 
    }

    //make the player bounce when hitting the ground and move if they are on an angle
    void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = new Vector3(zAngle, bouncePower,  xAngle);
    }
}
