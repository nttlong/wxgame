using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public float slideTime = 0.25f;

    private Vector2 showPos;
    private Vector2 hidePos;
    private bool isOpen = false;
    private bool sliding = false;
    private float t = 0;

    void Start()
    {
        float h = inventoryPanel.rect.height;

        showPos = new Vector2(0, 0);
        hidePos = new Vector2(0, -h);

        inventoryPanel.anchoredPosition = hidePos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Toggle();

        if (sliding)
        {
            t += Time.deltaTime / slideTime;

            if (isOpen)
                inventoryPanel.anchoredPosition = Vector2.Lerp(hidePos, showPos, t);
            else
                inventoryPanel.anchoredPosition = Vector2.Lerp(showPos, hidePos, t);

            if (t >= 1)
                sliding = false;
        }
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        sliding = true;
        t = 0;
    }
}
