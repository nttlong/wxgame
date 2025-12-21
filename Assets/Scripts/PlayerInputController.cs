using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Characters")]
    public CharacterMovement rootCharator;     // nhân vật chính
    public CharacterMovement nhanVatNu;        // nhân vật test

    [Header("Options")]
    public bool useTestCharacter = false;      // bật để điều khiển nhan-vat-nu

    private CharacterMovement currentCharacter;

    void Start()
    {
        // Chọn nhân vật mặc định khi bắt đầu game
        if (useTestCharacter)
            currentCharacter = nhanVatNu;
        else
            currentCharacter = rootCharator;
    }

    void Update()
    {
        if (currentCharacter == null) return;

        // Điều khiển
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool run = Input.GetKey(KeyCode.LeftShift);

        currentCharacter.Move(h, v, run);

        // Attack (nếu bạn cần)
        if (Input.GetMouseButtonDown(0))
            currentCharacter.Attack();

        // Hotkey chuyển nhanh nhân vật trong lúc test
        if (Input.GetKeyDown(KeyCode.F1))
            currentCharacter = rootCharator;

        if (Input.GetKeyDown(KeyCode.F2))
            currentCharacter = nhanVatNu;
    }
}
