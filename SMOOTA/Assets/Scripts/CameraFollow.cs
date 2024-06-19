using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float Followspeed = 2f;
    public Transform target;
    [SerializeField] bool verticalMovement = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new(target.position.x, verticalMovement ? target.position.y : transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, Followspeed * Time.deltaTime);
    }
}

