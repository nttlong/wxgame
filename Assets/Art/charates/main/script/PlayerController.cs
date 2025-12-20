using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0f); // đảm bảo Idle khi bắt đầu
    }

    void Update()
    {
        bool mouseHold = Input.GetMouseButton(0);
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (mouseHold)
        {
            if (isShift)
            {
                animator.SetFloat("Speed", 2.0f); // Run
            }
            else
            {
                animator.SetFloat("Speed", 1.0f); // Walk
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f); // Idle
        }
    }
}
