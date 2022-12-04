using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
#nullable enable
    private Slider? volumeSlider;
#nullable disable
    private AudioSource audioSource;
    private float volume = 1f;

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += delegate { OnSceneLoaded(); };
        }
    }

    void Update()
    {
        audioSource.volume = volume;

    }

    void OnSceneLoaded()
    {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("volume", 1f);
        audioSource.volume = volume;
        if (GameObject.Find("Volume"))
        {
            volumeSlider = GameObject.Find("Volume").GetComponent<Slider>();
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(volumeSlider); });
        }
        else
        {
            volumeSlider = null;
        }
    }

    public void UpdateVolume(Slider slider)
    {
        volume = slider.value;
        audioSource.volume = slider.value;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
