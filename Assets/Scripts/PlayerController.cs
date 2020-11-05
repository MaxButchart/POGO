using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bouncePower;
    public Rigidbody rigidbody;
    
    public float smooth = 5.0f;
    public float tiltAngle = 50.0f;

    public float tiltX;
    public float tiltZ;

    public float vert;
    public float hori;

    const float upperDeadzone = 0.2f;
    const float lowerDeadzone = -0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        vert = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");

        //rotate the player
        if (Input.GetAxis("Horizontal") > upperDeadzone || Input.GetAxis("Horizontal") < lowerDeadzone)
        {
            tiltZ = Input.GetAxis("Horizontal") * tiltAngle;
        }
        else
        {
            tiltZ = 0f;
        }
        if (Input.GetAxis("Vertical") > upperDeadzone || Input.GetAxis("Vertical") < lowerDeadzone)
        {
            tiltX = Input.GetAxis("Vertical") * tiltAngle;
        }
        else
        {
            tiltX = 0f;
        }

        Quaternion target = Quaternion.Euler(tiltX, 0, tiltZ);
        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    //make the player bounce when hitting the ground and move if they are on an angle
    void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = new Vector3(((bouncePower * tiltZ / tiltAngle) * -1), bouncePower, (bouncePower * tiltX / tiltAngle));
    }
}
