using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerState;

    public float ForwardMoving { get; private set; }
    public float SideMoving { get; private set; }
    public bool TurboModeOn { get; private set; }
    public bool TurboModeOff { get; private set; }
    public bool IsTurboModeInAction { get; private set; }
    public bool Jump { get; private set; }

    public bool TakeBall { get; private set; }
    public bool PutBall { get; private set; }

    public float Mouse_X { get; private set; }
    public float Mouse_Y { get; private set; }

    public bool GetThrowPower { get; private set; }
    public bool ReadyToThrow { get; private set; }
    public bool Throw { get; private set; }

    public bool Pause { get; private set; }


    void Update()
    {
        if (!playerState.IsControlTotalBlocked)
        {
            if (Time.timeScale == 1f)
            {
                ForwardMoving = Input.GetAxis("Vertical");
                SideMoving = Input.GetAxis("Horizontal");
                TurboModeOn = Input.GetKeyDown(KeyCode.LeftShift);
                TurboModeOff = Input.GetKeyUp(KeyCode.LeftShift);
                IsTurboModeInAction = Input.GetKey(KeyCode.LeftShift);
                Jump = Input.GetKeyDown(KeyCode.Space);

                TakeBall = Input.GetKeyDown(KeyCode.E);
                PutBall = Input.GetKeyDown(KeyCode.F);

                GetThrowPower = Input.GetMouseButtonDown(1);
                ReadyToThrow = Input.GetMouseButton(1);
                Throw = Input.GetMouseButtonUp(1);
            }

            Mouse_X = Input.GetAxis("Mouse X");
            Mouse_Y = Input.GetAxis("Mouse Y");

            Pause = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
