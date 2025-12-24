using UnityEngine;

public class TestClick : MonoBehaviour
{
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        Debug.Log("Collider World Pos = " + transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse = " + mouse + " | Collider = " + transform.position);

            if (col.OverlapPoint(mouse))
            {
                Debug.Log("🎉 CLICK TRÚNG COLLIDER NHÂN VẬT!");
            }
            else
            {
                Debug.Log("❌ Click KHÔNG trúng collider.");
            }
        }
    }
}
