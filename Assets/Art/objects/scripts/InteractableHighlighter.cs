using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableHighlighter : MonoBehaviour
{
    public static InteractableHighlighter Instance;

    public GameObject panel;
    public TextMeshProUGUI label;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public static void Show(string text, Transform worldTarget)
    {
        Instance.panel.SetActive(true);
        Instance.label.text = text;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldTarget.position + Vector3.up * 0.5f);
        Instance.panel.transform.position = screenPos;
    }

    public static void Hide()
    {
        Instance.panel.SetActive(false);
    }
}
