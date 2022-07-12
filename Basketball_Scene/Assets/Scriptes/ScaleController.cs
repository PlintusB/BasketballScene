using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private PlayerStateManager playerState;
    [SerializeField] private InputManager inputs;
    [SerializeField] private PlayerMovement playerMov;

    [Header("Components")]
    [SerializeField] private Image scaleImage;
    [SerializeField] private GameObject scalePanel;
    [SerializeField] private GameObject aim;
    [SerializeField] private AnimationCurve scaleCurve;

    private float maxTime ;
    private float currentTime;
    private bool timerForScale;

    void Start()
    {
        ResetScale();
        timerForScale = false;
        scalePanel.SetActive(false);
        aim.SetActive(false);
    }    

    void Update()
    {
        ScaleBehavior();
    }

    private void ScaleBehavior()
    {
        if (playerState.IsHaveBall &&
            inputs.ForwardMoving == 0 &&
            inputs.SideMoving == 0 &&
            playerMov.IsGrounded)
        {
            if (inputs.GetThrowPower)
            {
                ResetScale();
                scalePanel.SetActive(true);
                aim.SetActive(true);
                playerState.IsMovingBlocked = true;

                throwManager.GetDistanceToLoop();
                maxTime /= throwManager.DistanceXZ;
                timerForScale = true;
            }

            if (timerForScale)
            {
                currentTime = Mathf.PingPong(Time.time, maxTime);
                scaleImage.fillAmount = currentTime / maxTime;
            }

            if (inputs.Throw)
            {
                timerForScale = false;
                float accurateScaleCoeff = currentTime / maxTime;
                Debug.Log(accurateScaleCoeff);

                throwManager.ScaleCoeff = scaleCurve.Evaluate(accurateScaleCoeff) * 2f;
                Debug.Log(throwManager.ScaleCoeff);

                StartCoroutine(ScaleTurnOff());
                aim.SetActive(false);
            }
        }
    }

    private void ResetScale()
    {
        maxTime = 5f;
        currentTime = 0f;
    }

    IEnumerator ScaleTurnOff()
    {
        yield return new WaitForSeconds(3f);
        scalePanel.SetActive(false);
    }
}
