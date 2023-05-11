using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPosition = player.transform.position;
        myPosition.z -= 1f;
        myPosition.y += 0.5f;
        transform.position = myPosition;
    }
}
