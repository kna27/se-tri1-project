using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
#nullable enable
    [SerializeField] private Slider? volumeSlider;
#nullable disable
    private AudioSource audioSource;
    private float volume = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("volume", 1f);
        audioSource.volume = volume;
        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
        }
    }

    void Update()
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void UpdateVolume(float vol)
    {
        volume = vol;
    }
}
