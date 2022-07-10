using UnityEngine;

public class GetAnimationsEvents : MonoBehaviour
{
    //[SerializeField] private InputManager inputs;
    [SerializeField] private PlayerMovement playerMov;

    [SerializeField] private PlayerStateManager playerState;

    [SerializeField] private BallBehavior ballBeh;
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private GameObject ballSlot;

    public void InputBanOn() => playerState.IsMovingBlocked = true;
    public void InputBanOff() => playerState.IsMovingBlocked = false;
    public void LookAroundBanOn() => playerState.IsLookAroundBlocked = true;
    public void LookAroundBanOff() => playerState.IsLookAroundBlocked = false;
    public void JumpDuringThrow() => playerMov.Jump();
    //public void Throw() => throwManager.Throw();

    public void BallBecameCharacterChild()
    {

        ballBeh.Ball.transform.SetParent(ballSlot.transform);
        playerState.IsHaveBall = true;
    }

    public void BallLeaveCharacter()
    {
        ballBeh.Ball.transform.SetParent(null);
        playerState.IsHaveBall = false;
    }

    public void ResetBallLocalPosition()
    {
        ballBeh.Ball.transform.localPosition = Vector3.zero;
    }
}
