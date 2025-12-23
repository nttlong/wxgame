using UnityEngine;
[ExecuteInEditMode]
public class SetSortingOrder : MonoBehaviour
{
    public string sortingLayerName = "Default"; // Tên layer
    public int orderInLayer = 0; // Giá trị order

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = orderInLayer;
        }

        // Nếu có nhiều child:
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var r in renderers) { r.sortingLayerName = sortingLayerName; }
    }
}