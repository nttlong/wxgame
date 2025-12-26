using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public Transform interactPoint; // tạo 1 empty object → đặt trước cửa

    public string GetHintText() => "Open Door";

    public Transform GetInteractionPoint() => interactPoint;

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("Door opened!");
        // mở animation cửa tại đây
    }
}
