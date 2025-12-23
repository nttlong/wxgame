using UnityEngine;

public class LanternFollower : MonoBehaviour
{
    public Transform socketFront;
    public Transform socketBack;
    public Transform lantern;

    public bool facingRight = true;

    public void UpdateLanternSocket()
    {
        if (facingRight)
            lantern.SetParent(socketFront);
        else
            lantern.SetParent(socketBack);

        lantern.localPosition = Vector3.zero;
        lantern.localRotation = Quaternion.identity;

        // Update sorting order
        var sr = lantern.GetComponent<SpriteRenderer>();
        sr.sortingOrder = facingRight ? 10 : -10;
    }
}
