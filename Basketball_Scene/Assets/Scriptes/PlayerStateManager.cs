using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private Transform ballSlot;
    public bool IsHaveBall { get; set; }
    public bool IsMovingBlocked { get; set; }
    public bool IsLookAroundBlocked { get; set; }
    public bool IsControlTotalBlocked { get; set; }

    [SerializeField] private BallBehavior ballBeh;

    void Awake()
    {
        if (ballSlot.childCount > 0)
        {
            IsHaveBall = true;
            ballBeh.Ball = ballSlot.GetChild(0).gameObject;
        }
        else IsHaveBall = false;

        IsMovingBlocked = false;
        IsLookAroundBlocked = false;
    }
}
