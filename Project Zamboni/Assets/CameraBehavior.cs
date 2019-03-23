using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject playerGO = GameObject.FindWithTag("Player");
        //playerTransform = (Transform)playerGO.GetComponent("Transform");
        offset.z = -3;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
