using UnityEngine;

public class CameraFollowBounds1 : MonoBehaviour
{
    public Transform target;
    public float minX;
    public float maxX;

    void Start()
    {
        Debug.Log("CameraFollowBounds STARTED");
    }

    void LateUpdate()
    {
        Debug.Log("CameraFollowBounds UPDATE RUNNING");

        if (target == null)
        {
            Debug.LogWarning("CameraFollowBounds: TARGET = NULL");
            return;
        }

        float newX = Mathf.Clamp(target.position.x, minX, maxX);
        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
