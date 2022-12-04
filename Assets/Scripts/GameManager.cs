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
        Over
    };
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider wantednessSlider;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float wantednessDecayScale;
    [SerializeField] private float gameFadeoutTime;
    static GameState currentGameState;
    float wantedness;
    int score;

    private void Start()
    {
        currentGameState = GameState.Playing;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        ChangeWantedness(wantednessDecayScale * Time.deltaTime);
        if (currentGameState == GameState.Over)
        {
            StartCoroutine(FadeOutGame());
        }
    }

    public void PauseGame()
    {
        if (currentGameState == GameState.Paused)
        {
            currentGameState = wantedness == 100f ? GameState.Over : GameState.Playing;
        }
        else
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
        wantedness = Mathf.Clamp(wantedness + changeAmount, 0, 100f);
        wantednessSlider.value = wantedness;
        if (wantedness == 100f)
        {
            currentGameState = GameState.Over;
        };
    }

    IEnumerator FadeOutGame()
    {
        while (fadePanel.color.a < 1)
        {
            fadePanel.color = Color.Lerp(fadePanel.color, new Color(0, 0, 0, 1), gameFadeoutTime * Time.deltaTime);
            // Load next scene when faded out
            if (fadePanel.color.a > 0.99)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            yield return null;
        }
        yield return new WaitForSeconds(gameFadeoutTime);

    }
}
