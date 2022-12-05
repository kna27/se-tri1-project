using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] Slider sensitvitySlider;

    void Start()
    {
        sensitvitySlider.value = PlayerPrefs.GetFloat("sensitivity", 200f);
    }

    public void UpdateSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat("sensitivity", slider.value);
    }
}
