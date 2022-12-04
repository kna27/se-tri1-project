using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float wantedness;
    int score;
    public float wantednessDecayScale;
    public Image fadePanel;
    public Slider wantednessSlider;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeWantedness(wantednessDecayScale * Time.deltaTime);
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
            EndGame();
        }
    }

    void EndGame()
    {
        StartCoroutine(FadeOutGame());
    }

    IEnumerator FadeOutGame()
    {
        for (float fade = 0f; fade <= 1; fade += 0.05f)
        {
            Color c = fadePanel.color;
            c.a = fade;
            fadePanel.color = c;
            Time.timeScale = 1 - fade;
            yield return new WaitForSeconds(.1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
