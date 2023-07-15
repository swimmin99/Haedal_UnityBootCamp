using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public bool followPlayer = true;
    public GameObject playerObj;
    public float followSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {if (followPlayer == true)
        {
            Vector3 targetPosition = playerObj.transform.position + new Vector3(0, 0, transform.position.z);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.position = smoothPosition;
        }
    }
}
