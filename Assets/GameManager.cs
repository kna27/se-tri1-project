using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float wantedness;
    int score;
    public float wantednessDecayScale;
    bool gameOver;
    public Image fadePanel;
    public Slider wantednessSlider;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = true;
        StartCoroutine(FadeOutGame());
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWantedness(wantednessDecayScale * Time.deltaTime);
        if (gameOver)
        {
            gameOver = true;
            StartCoroutine(FadeOutGame());
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
        gameOver = wantedness == 100f;
    }

    IEnumerator FadeOutGame()
    {
        while ((fadePanel.color.a < 1) || (gameOver == true))
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
