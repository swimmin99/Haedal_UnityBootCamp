using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x > 0)
        {
            myRigidbody.AddForce(Vector3.right * moveSpeed);
        }
        else if (x < 0)
        {
            myRigidbody.AddForce(Vector3.left * moveSpeed);

        }

        if (z > 0)
        {
            myRigidbody.AddForce(Vector3.forward * moveSpeed);
        }
        else if (z < 0)
        {
            myRigidbody.AddForce(Vector3.back * moveSpeed);

        }

        transform.eulerAngles = new Vector3(0, 0, -x*20);
    }
}
