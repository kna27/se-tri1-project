using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Playing,
        Paused,
        Won,
        Caught
    };
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider wantednessSlider;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverState;
    [SerializeField] private TextMeshProUGUI gameOverTip;
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private float wantednessDecayScale;
    [SerializeField] private float gameFadeoutTime;
    [SerializeField] private float TimerMinutes = 5f;
    [SerializeField] private float TimerSeconds = 0f;
    [SerializeField] private string addZero = "";
    static GameState currentGameState;
    float wantedness;
    int score;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        currentGameState = GameState.Playing;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        
        if (TimerMinutes <= 0 && TimerSeconds <= 0)
        {
            currentGameState = GameState.Won;
        }
        
        else if (TimerSeconds <= 0)
        {
            TimerMinutes--;
            TimerSeconds = 59f;
        }
        else
        {
            TimerSeconds -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        ChangeWantedness(wantednessDecayScale * Time.deltaTime);
        if (currentGameState == GameState.Caught)
        {
            StartCoroutine(FadeOutGame());
        }
        if (currentGameState == GameState.Won)
        {
            StartCoroutine(FadeOutGame());
        }
        
        
        
        timerText.text = TimeFormat.FormatTime(TimerMinutes, TimerSeconds);
        
    }

    public void PauseGame()
    {
        if (currentGameState == GameState.Paused)
        {
            currentGameState = wantedness == 100f ? GameState.Caught : GameState.Playing;
        }
        else if(currentGameState != GameState.Caught || currentGameState != GameState.Won)
        {
            currentGameState = GameState.Paused;
        }
        if (currentGameState == GameState.Paused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void ChangeWantedness(float changeAmount)
    {
        wantedness = WantednessCalculations.AddWantedness(wantedness, changeAmount);
        wantednessSlider.value = wantedness;
        if (wantedness == 100f)
        {
            currentGameState = GameState.Caught;
        };
    }

    IEnumerator FadeOutGame()
    {
        while (fadePanel.color.a < 1)
        {
            fadePanel.color = Color.Lerp(fadePanel.color, new Color(0, 0, 0, 1), gameFadeoutTime * Time.deltaTime);
            if (fadePanel.color.a > 0.99 && (currentGameState == GameState.Caught || currentGameState == GameState.Won))
            {
                Time.timeScale = 0f;
                gameOverPanel.SetActive(true);
                gameOverState.text = currentGameState == GameState.Caught ? "You Got Caught!" : "You Won!";
                gameOverTip.text = currentGameState == GameState.Caught ? "Try staying out out of sight of cameras and the guard" : "Try going for more risky plays to increase your score";
                gameOverScore.gameObject.SetActive(currentGameState == GameState.Won);
                gameOverScore.text = "Score: " + score.ToString();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
            }
            yield return null;
        }
        yield return new WaitForSeconds(gameFadeoutTime);

    }
}
