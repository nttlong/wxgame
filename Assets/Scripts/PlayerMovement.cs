using UnityEngine;

public class Player2DController : MonoBehaviour
{
    // Tốc độ di chuyển
    public float moveSpeed = 5f;

    // Lực nhảy
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = false; // Kiểm tra xem nhân vật có đang chạm đất không

    void Start()
    {
        // Lấy Rigidbody2D khi game bắt đầu
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- LOGIC DI CHUYỂN NGANG ---

        // 1. Lấy đầu vào (Horizontal Axis: Mặc định là A/D hoặc Left/Right Arrow)
        float moveInput = Input.GetAxis("Horizontal");

        // 2. Thiết lập vận tốc ngang
        // Đặt velocity X bằng đầu vào nhân với tốc độ. Giữ nguyên velocity Y (đang rơi hoặc đang nhảy).
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // --- LOGIC NHẢY (Jump) ---

        // Kiểm tra nếu phím "Jump" (mặc định là Space) được nhấn VÀ nhân vật đang chạm đất
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Áp dụng lực hướng lên (ForceMode.Impulse cho lực tức thời)
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    // --- LOGIC KIỂM TRA CHẠM ĐẤT (Cần thiết cho Jump) ---

    // Hàm này được gọi khi Collider của nhân vật bắt đầu chạm vào một Collider khác
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Giả sử các đối tượng nền đất (Ground) có tag là "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Hàm này được gọi khi Collider của nhân vật ngừng chạm vào một Collider khác
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}