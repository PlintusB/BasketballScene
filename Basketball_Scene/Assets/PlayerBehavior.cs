using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float forwardMoving;
    private float sideMoving;
    private float speed;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10f;
    }

    void Update()
    {
        forwardMoving = Input.GetAxis("Vertical") * speed;
        sideMoving = Input.GetAxis("Horizontal") * speed;
        Vector3 playerMoving = new Vector3(sideMoving, 0f, forwardMoving);

        rb.MovePosition(transform.position + playerMoving * Time.deltaTime);
    }
}
