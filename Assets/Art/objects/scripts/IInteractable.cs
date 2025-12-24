using UnityEngine;

public interface IInteractable
{
    string GetHintText();
    Transform GetInteractionPoint();
    void Interact(PlayerInteraction player);
}
