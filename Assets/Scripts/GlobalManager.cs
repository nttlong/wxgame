using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    void Update()
    {
        // Kiểm tra nếu người dùng nhấn phím Escape (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quay trở về cảnh MainMenu
            // Thay "MainMenu" bằng tên chính xác của cảnh Menu trong Build Settings của bạn
            SceneManager.LoadScene("MainMenu");
        }
    }
}