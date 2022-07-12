using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketFirstTrigger : MonoBehaviour
{
    [SerializeField] private BasketSecondTrigger secondTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            secondTrigger.LoopTriggerIndex++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
            secondTrigger.LoopTriggerIndex = 0;
    }
}
