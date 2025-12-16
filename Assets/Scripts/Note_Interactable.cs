using UnityEngine;

// [RequireComponent] đảm bảo đối tượng này luôn có Collider để Raycast tìm thấy
[RequireComponent(typeof(BoxCollider2D))]
public class Note_Interactable : MonoBehaviour, IInteractable // Kế thừa IInteractable
{
    public string noteContent = "Đây là một tờ ghi chú cũ kỹ, bạn cảm thấy rùng mình.";

    public void Interact() // Hàm được gọi từ Script Interacter
    {
        // Khi nhân vật tương tác, in nội dung ghi chú ra Console
        Debug.Log("Đọc ghi chú: " + noteContent);

        // (Đây là nơi bạn sẽ thêm code mở UI hiển thị text trong game thật)
    }
}