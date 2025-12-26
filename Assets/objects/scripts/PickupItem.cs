using UnityEngine;

public class PickupItem : InteractableBase
{
    public string itemId;

    public override void Interact(Transform player)
    {
        Debug.Log($"Picked up item {itemId}");
        gameObject.SetActive(false);

        // truyền lên Inventory
        InventoryEvents.OnItemClicked?.Invoke(itemId);
    }

    public override string GetHighlightText()
    {
        return "Pick Up";
    }
}
