using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    private Animator animator;
    private Transform visual;   // nơi chứa sprite / animation

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        visual = animator.transform;
    }

    // -----------------------------
    //  CONTROL FROM PlayerInputController
    // -----------------------------
    public void Move(float horizontal, float vertical, bool run)
    {
        // Tính tốc độ
        float speed = run ? runSpeed : walkSpeed;

        Vector3 move = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;

        transform.position += move;

        // Update animation speed
        float animSpeed = move.magnitude > 0 ? (run ? 1.8f : 1.0f) : 0f;
        animator.SetFloat("Speed", animSpeed);

        // Flip
        if (horizontal != 0)
        {
            visual.localScale = new Vector3(horizontal > 0 ? 1 : -1, 1, 1);
        }
    }

    // -----------------------------------
    //  OPTIONAL - Attack (nếu bạn chưa dùng thì nó cũng không ảnh hưởng)
    // -----------------------------------
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
