using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameBehavior gameBeh;
    [SerializeField] private PlayerStateManager playerState;

    public void GameStart()
    {
        gameBeh.IsTimerTurnedOn = true;
        gameBeh.ScoreboardPanel.SetActive(true);
        playerState.IsControlTotalBlocked = false;
        print("!!!");
        // звук сирены
    }

    public void CountdownSoundSign()
    {

    }
}
