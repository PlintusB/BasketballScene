using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private InputManager inputs;
    [SerializeField] private PlayerStateManager playerState;    

    public GameObject Ball { get; set; }
    
    private GameObject ballGreenShining;
    private Collider freeBallTrigger;

    public Rigidbody BallRb { get; private set; }
    public bool ReadyToBeTaken { get; private set; }
    private bool isBallArrivedToHand;
    private bool isBallPulsate;
    private float ballScale;
    private float startScale;
    private float maxScale;
    private float scaleDelta;

    void Awake()
    {
        ReadyToBeTaken = false;
        isBallArrivedToHand = true;
        isBallPulsate = false;
        startScale = 0.158f;
        maxScale = 0.19f;
        scaleDelta = maxScale - startScale;
        freeBallTrigger = GetComponent<Collider>();
    }

    private void Start()
    {
        if (playerState.IsHaveBall)
        {
            freeBallTrigger.gameObject.SetActive(false);
            BallRb = Ball.GetComponent<Rigidbody>();
            BallRb.isKinematic = true;
        }
    }

    private void Update()
    {
        if (ReadyToBeTaken)
        {
            if (isBallPulsate)
            {
                ballScale = startScale + Mathf.PingPong(Time.time / 10f, scaleDelta);
                Ball.transform.localScale = Vector3.one * ballScale;
            }

            if (inputs.TakeBall)
            {
                isBallArrivedToHand = false;
                PulsateBall(false);
            }            
        }

        if (!isBallArrivedToHand)
        {
            Vector3 handsVector =
                freeBallTrigger.bounds.center - Ball.transform.position;

            print(handsVector.magnitude);

            BallRb = Ball.GetComponent<Rigidbody>();
            BallRb.velocity = handsVector.normalized;            

            if (handsVector.magnitude < 0.02f)
            {
                ReadyToBeTaken = false; 
                BallRb.velocity = Vector3.zero;
                BallRb.isKinematic = true;
                isBallArrivedToHand = true;
                playerState.IsHaveBall = true;
                freeBallTrigger.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball") && !playerState.IsHaveBall)
        {
            ReadyToBeTaken = true;
            Ball = other.gameObject;
            ballGreenShining = Ball.transform.GetChild(0).gameObject;
            PulsateBall(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && !playerState.IsHaveBall)
        {
            ReadyToBeTaken = false;
            PulsateBall(false);
            //Ball = null;
        }
    }

    private void PulsateBall(bool flag)
    {
        isBallPulsate = flag;
        ballGreenShining.SetActive(flag);
        if(!flag) Ball.transform.localScale = Vector3.one * startScale;
    }
}
