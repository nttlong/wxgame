using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
{
    public string itemId;    // ví dụ: "lamp"
    

    void Start()
    {
       
       
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked inventory item: " + itemId);
        InventoryEvents.OnItemClicked?.Invoke(itemId);
    }
}
