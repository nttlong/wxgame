using UnityEngine;

public class ElbowLimit : MonoBehaviour
{
    // Chỉnh số này để cánh tay dừng lại đúng đường màu đỏ của bạn
    public float maxAngle = 145f;
    public float minAngle = 0f;

    void LateUpdate()
    {
        Vector3 angles = transform.localEulerAngles;
        float z = angles.z;

        // Chuyển đổi góc để tính toán chính xác
        if (z > 180) z -= 360;

        // Chặn đứng xương tại góc maxAngle
        z = Mathf.Clamp(z, minAngle, maxAngle);

        transform.localEulerAngles = new Vector3(angles.x, angles.y, z);
    }
}