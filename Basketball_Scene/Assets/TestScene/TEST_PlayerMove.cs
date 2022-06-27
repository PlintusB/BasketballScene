using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST_PlayerMove : MonoBehaviour
{
    public GameObject ball;
    public Transform target;

    public Image scale;

    float maxTime = 1f;
    float currentTime = 0f;

    private void Start()
    {
        scale.fillAmount = 0;
    }


    void Update()
    {
        float forwardMove = Input.GetAxis("Vertical") * Time.deltaTime * 3;
        float sideMove = Input.GetAxis("Horizontal") * Time.deltaTime * 3;

        float rotateY = Input.GetAxis("Mouse X");

        transform.position += transform.forward * forwardMove +
            transform.right * sideMove;
        transform.Rotate(Vector3.up, Mathf.Repeat(rotateY, 360));

        currentTime = Mathf.PingPong(Time.time, maxTime);
        scale.fillAmount = currentTime / maxTime;


        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBall = Instantiate(ball, transform.position + 
                Vector3.up, transform.rotation * Quaternion.Euler(-45f, 0f, 0f));

            float distance = 
                Vector3.Distance(newBall.transform.position, target.position);

            float optimalSpeed = Mathf.Sqrt(distance * 9.81f);
            float speed = optimalSpeed * (currentTime / maxTime) * 2;


            newBall.GetComponent<Rigidbody>().velocity =
                (newBall.transform.forward) * speed;

            Destroy(newBall, 5f);


        }
    }
}
