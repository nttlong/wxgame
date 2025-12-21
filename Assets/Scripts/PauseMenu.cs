using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Ẩn bảng Menu //<--pauseMenuUI null
        Time.timeScale = 1f;          // Cho game chạy bình thường
        IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Hiện bảng Menu
        Time.timeScale = 0f;          // Dừng toàn bộ thời gian trong game
        IsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;          // Reset thời gian trước khi chuyển cảnh
        SceneManager.LoadScene("MainMenu"); // Tên scene Menu của bạn
    }
}