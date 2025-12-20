using UnityEngine;

public class PointClickController : MonoBehaviour
{
    [Header("Move Speeds (khớp animation)")]
    public float walkSpeed = 1.5f;
    public float slowWalkSpeed = 0.7f;
    public float runSpeed = 10.2f;

    [Header("Animation Values (khớp Blend Tree)")]
    public float idleAnim = 0f;
    public float slowWalkAnim = 0.5f;
    public float walkAnim = 1f;
    public float runAnim = 2f;

    [Header("Foot Sliding Fix")]
    public float acceleration = 15f;
    public float directionSmooth = 15f;

    private Animator animator;
    private Transform visual;

    private float moveVelocity = 0f;
    private float moveDir = 1f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        visual = animator.transform;
    }

    void Update()
    {
        // Không giữ chuột → idle
        if (!Input.GetMouseButton(0))
        {
            moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);
            animator.SetFloat("Speed", idleAnim);
            return;
        }

        // Lấy vị trí chuột
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        float distance = mouse.x - transform.position.x;

        // Xác định hướng
        float targetDir = Mathf.Sign(distance);
        moveDir = Mathf.Lerp(moveDir, targetDir, Time.deltaTime * directionSmooth);

        // Chọn mode di chuyển
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool slow = Input.GetKey(KeyCode.LeftControl);

        float targetSpeed;
        float animValue;

        if (slow)
        {
            targetSpeed = slowWalkSpeed;
            animValue = slowWalkAnim;
        }
        else if (run)
        {
            targetSpeed = runSpeed;
            animValue = runAnim;
        }
        else
        {
            targetSpeed = walkSpeed;
            animValue = walkAnim;
        }

        // Set animator
        animator.SetFloat("Speed", animValue);

        // Làm mượt tốc độ
        moveVelocity = Mathf.Lerp(moveVelocity, targetSpeed, Time.deltaTime * acceleration);

        // Di chuyển
        transform.position += new Vector3(moveDir * moveVelocity * Time.deltaTime, 0, 0);

        // Flip theo hướng
        visual.localScale = new Vector3(moveDir > 0 ? 1 : -1, 1, 1);
    }
}
