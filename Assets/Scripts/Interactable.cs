using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public Slider interactProgress;
    public string interactText;
    public float interactTime;
    public static float maxInteractRange = 4f;
    public GameObject interactPanel;

    void Start()
    {
        interactPanel = GameObject.Find("Interact Panel");
    }

    public void ShowInteractText()
    {
        interactPanel.SetActive(true);
        interactPanel.GetComponentInChildren<TextMeshProUGUI>().text = interactText;
    }

    public void HideInteractText()
    {
        interactPanel.SetActive(false);
    }
    
    public virtual void Interact() { }
}
