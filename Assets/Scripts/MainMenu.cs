using UnityEngine;
using UnityEngine.SceneManagement; // Thư viện để chuyển cảnh

public class MainMenu : MonoBehaviour
{
    // Hàm này sẽ chạy khi nhấn nút Start
    public void PlayGame()
    {
        // Chuyển sang cảnh tiếp theo trong danh sách Build Settings
        // Lưu ý: Chỉ số 1 thường là màn chơi đầu tiên sau Menu (chỉ số 0)
        //chapter-1
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("chapter-1");
        Debug.Log("Đã thoát game!");
    }

    // Hàm này sẽ chạy khi nhấn nút Exit
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Thoát chế độ Play trong Editor
#else
        Application.Quit(); // Thoát ứng dụng thực tế
#endif
    }
}