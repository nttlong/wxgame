using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public Transform lampSocket;      // kéo socket vào đây
    public GameObject lampPrefab;     // prefab cây đèn (không phải UI)
    private GameObject currentLamp;

    void OnEnable()
    {
        InventoryEvents.OnItemClicked += HandleEquip;
    }

    void OnDisable()
    {
        InventoryEvents.OnItemClicked -= HandleEquip;
    }

    void HandleEquip(string itemId)
    {
        if (itemId != "cay-den") return;

        // Nếu đã có đèn thì xóa trước
        if (currentLamp != null)
            Destroy(currentLamp);

        // Spawn và gắn
        currentLamp = Instantiate(lampPrefab, lampSocket);

        currentLamp.transform.localPosition = Vector3.zero;
        currentLamp.transform.localRotation = Quaternion.identity;

        // Scale nếu cần
        currentLamp.transform.localScale = Vector3.one;
    }
}
