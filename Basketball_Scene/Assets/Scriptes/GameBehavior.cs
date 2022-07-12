using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private InputManager inputs;
    [SerializeField] private PlayerStateManager playerState;

    [Header("Objects")]
    [SerializeField] private GameObject endGameWindow;
    [SerializeField] private GameObject pauseGameWindow;
    [SerializeField] private GameObject scoreboardPanel;
    public GameObject ScoreboardPanel
    {
        get { return scoreboardPanel; }
        set { scoreboardPanel = value; }
    }

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI timeCountdownText;
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Animator startGameAnimator;

    [Header("Timer start values")]
    [SerializeField] private int startMinutes;
    [SerializeField] private int startSeconds;

    private int currentMinutes;
    private float currentSeconds;
    public bool IsTimerTurnedOn { get; set; }

    private int gameScore;
    public int GameScore
    {
        get { return gameScore; }
        set
        {
            gameScore = value;
            RefreshScoreboard();
        }
    }
    
    void Start()
    {
        ResetGameValues();
        CountdownToStartGame();
    }

    void Update()
    {
        Countdown();
        RefreshTimeCountdown();
        SetPauseByKey();
    }

    private void Countdown()
    {
        if (IsTimerTurnedOn)
        {
            currentSeconds -= Time.deltaTime;

            if (currentSeconds < 0)
            {
                if (currentMinutes > 0)
                {
                    currentMinutes--;
                    currentSeconds = 60;
                }
                else
                {
                    currentSeconds = 0;
                    FreezeTime(true);
                    endGameWindow.SetActive(true);
                    IsTimerTurnedOn = false;
                }
            }
        }
    }

    private void RefreshScoreboard()
    {
        finalScoreText.text = gameScore.ToString();
        string score = gameScore.ToString();
        if (gameScore < 10)
            gameScoreText.text = "0 " + score;
        else
        {
            char[] nums = score.ToCharArray();
            gameScoreText.text = nums[0].ToString() + " " + nums[1].ToString();
        }
    }

    private void RefreshTimeCountdown()
    {
        if (currentMinutes == 0 && currentSeconds < 10)
        {
            if (currentMinutes == currentSeconds)
                timeCountdownText.text = "00.00";
            else
                timeCountdownText.text = $"0{System.Math.Round(currentSeconds, 2)}";
        }
        else
        {
            string minutesStr = currentMinutes.ToString();
            string secondsStr = Mathf.Floor(currentSeconds).ToString();

            if (currentMinutes < 10)
                minutesStr = "0" + minutesStr;
            if (currentSeconds < 10)
                secondsStr = "0" + secondsStr;
            timeCountdownText.text = $"{minutesStr}:{secondsStr}";
        }
    }

    private void SetPauseByKey()
    {
        if (inputs.Pause &&
            !endGameWindow.activeInHierarchy &&
            !inputs.ReadyToThrow)
            SetPauseByButton();
    }

    public void SetPauseByButton()
    {
        bool needToPause = Time.timeScale == 1f;
        FreezeTime(needToPause);
        pauseGameWindow.SetActive(needToPause);
        UnlockCursor(needToPause);
    }

    private void FreezeTime(bool freeze)
    {
        Time.timeScale = freeze ? 0 : 1;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
        ResetGameValues();
        //CountdownToStartGame();
        //   1. ввести булевую в АниматорКонтроллере,
        // через которую в этом месте будет запускаться стартовая анимация игрока
        // с пролётом камеры!
        //   2. Добавить покачивание второй руки при стартовом отсчёте (или сместить
        // в левый угол анимацию отсчёта (или вообще сместить это в левый верхний угол,
        // сделав маленьким! И вообще, посмотреть, как эта анимация отсчёта будет
        // смотреться на фоне темноты и неона, а так же как будет эффектно пролетать
        // камера, показывая интересности. Добавить под обратный отсчёт включение
        // прожекторов (а камера в пролёте увидит только неон?). 
        //   3. Осталось пофиксить баг с попаданием в кольцо, сделать препятствия,
        // музыку и свет! И возможно таблицу рекордов эвер! И совсем уж по желанию - 
        // анимацию с тем, как игрок играет в баскет своей головой.
    }

    private void ResetGameValues()
    {
        gameScore = 0;
        RefreshScoreboard();

        currentSeconds = startSeconds;
        currentMinutes = startMinutes;

        endGameWindow.SetActive(false);
        pauseGameWindow.SetActive(false);        
        FreezeTime(false);
    }

    private void CountdownToStartGame()
    {
        IsTimerTurnedOn = false;
        playerState.IsControlTotalBlocked = true;
        scoreboardPanel.SetActive(false);
        startGameAnimator.SetTrigger("StartAnim");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    private void UnlockCursor(bool unlock)
    {
        if(unlock) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;        
    }
}
