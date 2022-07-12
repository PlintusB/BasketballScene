using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private InputManager inputs;

    [Header("Run Settings")]
    [SerializeField] private float speed;
    public float Speed 
    {
        get { return speed; }
        private set { speed = value; } 
    }

    [SerializeField] private float coeffTurboMode;
    [SerializeField] private AnimationCurve forwardMoveCurve;
    [SerializeField] private AnimationCurve sideMoveCurve;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundMask;
    public bool IsGrounded { get; private set; }

    private Rigidbody playerRb;
    private Collider playerColl;
    private PlayerStateManager playerState;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerColl = GetComponent<Collider>();
        playerState = GetComponent<PlayerStateManager>();
    }

    void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(playerColl.bounds.center,
                                     Vector3.down,
                                     playerColl.bounds.extents.y + 0.1f,
                                     groundMask);

        if (!playerState.IsMovingBlocked)
        {
            MovePlayer();
        }
    }

    private void Update()
    {
        if (!playerState.IsMovingBlocked)
        {
            ChangeTurboMode();
            if (inputs.Jump && IsGrounded) Jump();
        }
    }

    public void MovePlayer()
    {
        Vector3 forwardMove =
            transform.forward * forwardMoveCurve.Evaluate(inputs.ForwardMoving);
        Vector3 sideMove =
            transform.right * sideMoveCurve.Evaluate(inputs.SideMoving);
        Vector3 moveDirection = (forwardMove + sideMove) * Speed * Time.fixedDeltaTime;

        playerRb.MovePosition(playerRb.position + moveDirection);
    }
    
    public void ChangeTurboMode()
    {
        if (inputs.TurboModeOn) Speed *= coeffTurboMode;
        if (inputs.TurboModeOff) Speed /= coeffTurboMode;
    }    

    public void Jump()
    {       
        playerRb.velocity = 
            new Vector3(playerRb.velocity.x, jumpPower, playerRb.velocity.z);        
    }
}
