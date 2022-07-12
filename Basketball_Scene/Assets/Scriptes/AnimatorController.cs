using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private PlayerMovement playerMov;
    [SerializeField] private InputManager inputs;
    [SerializeField] private BallBehavior ballBeh;

    private PlayerStateManager playerState;
    private Animator anim;
    private float forwardMove;

    void Awake()
    {
        playerState = GetComponent<PlayerStateManager>();
        anim = GetComponentInChildren<Animator>();
        playerMov = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        anim.SetBool("isHaveBall", playerState.IsHaveBall);
    }

    void Update()
    {
        forwardMove = inputs.ForwardMoving;

        if (playerState.IsHaveBall) PlayAnimationsWithBall();
        if (!playerState.IsHaveBall) PlayEmptyAnimations();
    }

    private void PlayAnimationsWithBall()
    {
        if (playerMov.IsGrounded && inputs.Jump)
            anim.SetTrigger("JumpWithBall");

        if (inputs.PutBall &&
            playerMov.IsGrounded &&
            forwardMove < 0.01f)
            anim.SetTrigger("PutBall");

        if (forwardMove > 0.3f)
            anim.SetBool("RunWithBall", true);

        if (forwardMove < 0.3f)
            anim.SetBool("RunWithBall", false);

        if (inputs.GetThrowPower)
            anim.SetTrigger("GetThrowPower");

        if (inputs.Throw)
            anim.SetTrigger("Throw");
    }

    private void PlayEmptyAnimations()
    {
        if (playerMov.IsGrounded && inputs.Jump)
            anim.SetTrigger("JumpEmpty");

        if (inputs.TakeBall &&
            playerMov.IsGrounded &&
            forwardMove < 0.01f &&
            ballBeh.ReadyToBeTaken)
            anim.SetTrigger("TakeBall");

        if (forwardMove < 0.3f)
            anim.SetBool("RunEmpty", false);

        if (forwardMove > 0.3f)
            anim.SetBool("RunEmpty", true);

        anim.SetBool("TurboRunEmpty", inputs.IsTurboModeInAction);
    }
}
