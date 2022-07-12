using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    [SerializeField] private BallBehavior ballBeh;
    [SerializeField] private Transform basketLoop;

    [SerializeField] private GameObject pref;
    private float angleInDegrees;
    public float DistanceXZ { get; set; }
    private float distanceY;
    public float ScaleCoeff { get; set; }

    public void GetDistanceToLoop()
    {
        Vector3 distance = basketLoop.position - ballBeh.Ball.transform.position;
        DistanceXZ = new Vector3(distance.x, 0f, distance.z).magnitude;
        distanceY = distance.y;
    }

    public void Throw()
    {
        GetDistanceToLoop();
        if (DistanceXZ < 4) angleInDegrees = 70f;
        else if (DistanceXZ > 8) angleInDegrees = 45f;
        else angleInDegrees = 60f;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        float startBallSpeedPow2 = (9.81f * Mathf.Pow(DistanceXZ, 2)) /
            (2 * (distanceY - Mathf.Tan(angleInRadians) * DistanceXZ) *
            Mathf.Pow(Mathf.Cos(angleInRadians), 2));

        float startBallSpeed = Mathf.Sqrt(Mathf.Abs(startBallSpeedPow2)) * ScaleCoeff;

        ballBeh.Ball.transform.rotation = transform.rotation;

        ballBeh.BallRb.velocity = startBallSpeed *
            (ballBeh.Ball.transform.up * Mathf.Tan(angleInRadians) +
            ballBeh.Ball.transform.forward).normalized;
    }   
}
