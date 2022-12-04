using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        Over
    };
    float wantedness;
    int score;
    public float wantednessDecayScale;
    public Image fadePanel;
    public Slider wantednessSlider;
    public TextMeshProUGUI scoreText;
    public static GameState currentGameState;


    private void Start()
    {
        currentGameState = GameState.Playing;
    }
    void Update()
    {
        ChangeWantedness(wantednessDecayScale * Time.deltaTime);
        if (currentGameState == GameState.Over)
        {
            StartCoroutine(FadeOutGame());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.Paused)
            {
                currentGameState = wantedness == 100f ? GameState.Over : GameState.Playing;
            }
            else
            {
                currentGameState = GameState.Paused;
            }
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (currentGameState == GameState.Paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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
        while ((fadePanel.color.a < 1) || (currentGameState == GameState.Over))
        {
            fadePanel.color = Color.Lerp(fadePanel.color, new Color(0, 0, 0, 1), 0.75f * Time.deltaTime);
            if (fadePanel.color.a > 0.99)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.75f);

    }
}
