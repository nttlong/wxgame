using UnityEngine;
public class LimitRotation : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 rot = transform.localEulerAngles;
        // Chặn trục Z trong khoảng 0 đến 140 độ
        float z = rot.z;
        if (z > 180) z -= 360;
        z = Mathf.Clamp(z, 0, 140);
        transform.localEulerAngles = new Vector3(rot.x, rot.y, z);
    }
}