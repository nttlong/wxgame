using UnityEngine;

public class Interacter : MonoBehaviour
{
    // Khoảng cách tối đa nhân vật có thể tương tác
    public float interactionDistance = 1.5f;

    // Update được gọi mỗi frame
    void Update()
    {
        // Kiểm tra nếu người chơi nhấn nút Tương tác (ví dụ: Phím E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Bắn tia (Raycast) từ vị trí nhân vật sang phải để tìm vật thể tương tác
            // Tạm thời chỉ kiểm tra hướng Vector2.right (Bên phải)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionDistance);

            if (hit.collider != null)
            {
                // Lấy component IInteractable từ vật thể bị tia bắn trúng
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    Debug.Log("Đã phát hiện và tương tác với: " + hit.collider.gameObject.name);
                    interactable.Interact();
                }
            }
        }
    }
}