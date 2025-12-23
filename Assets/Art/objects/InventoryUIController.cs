using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public float slideTime = 0.25f;
    public float TopPos = 1;
    private Vector2 showPos;
    private Vector2 hidePos;
    private bool isOpen = false;
    private bool sliding = false;
    private float t = 0;

    void Start()
    {
        float h = inventoryPanel.rect.height;

        showPos = Vector2.zero;        // ← Sửa chỗ này
        hidePos = new Vector2(0, -h);  // Ẩn xuống hoàn toàn

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
                inventoryPanel.anchoredPosition = Vector2.Lerp(hidePos, showPos, t); // InventoryPanel chỉ up lên 1/2 độ cao
            else
                inventoryPanel.anchoredPosition = Vector2.Lerp(showPos, hidePos, t);

            if (t >= 1)
                sliding = false;
        }
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        GameInputState.InventoryOpen = isOpen;

        sliding = true;
        t = 0;
    }
}
