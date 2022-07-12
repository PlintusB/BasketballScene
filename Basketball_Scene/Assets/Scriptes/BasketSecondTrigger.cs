using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSecondTrigger : MonoBehaviour
{
    [SerializeField] private GameBehavior gameBeh;
    [SerializeField] private BasketFirstTrigger firstTrigger;
    private Rigidbody ballRb;

    private int loopTriggerIndex;
    public int LoopTriggerIndex
    {
        get { return loopTriggerIndex; }
        set 
        {
            loopTriggerIndex = value;
            if (loopTriggerIndex > 1) gameBeh.GameScore++;
        }
    }

    void Start()
    {
        loopTriggerIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            loopTriggerIndex++;
            ballRb = other.GetComponent<Rigidbody>();
            ballRb.drag = 3f;
            ballRb.angularDrag = 3f;
            ballRb.AddForce(Vector3.forward);
            // звук сетки
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballRb.drag = 0f;
            ballRb.angularDrag = 0f;
            // звук сирены
        }
    }
}
