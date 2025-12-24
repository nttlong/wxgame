using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    [Header("Display name")]
    public string interactionText = "Interact";

    public virtual void Interact(Transform player)
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }

    public virtual string GetHighlightText()
    {
        return interactionText;
    }

    public string GetHintText()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetInteractionPoint()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteraction player)
    {
        throw new System.NotImplementedException();
    }
}
