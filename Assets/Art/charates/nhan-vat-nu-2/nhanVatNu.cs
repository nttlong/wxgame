using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class nhanVatNu : MonoBehaviour
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
    [Header("Foot Step Interval")]
    public float idleInterval = 0f;
    public float slowInterval = 0.5f;
    public float walkInterval = 1f;
    public float runInterval = 2f;

    [Header("Footstep Settings")]



    public AudioSource footstepAudio;
    public float stepInterval = 0.5f; // Thời gian giữa các bước
    private float nextStepTime;
    [Header("Stop threshold")]

    float stopThreshold = 0.05f;

    [Header("Foot Sliding Fix")]
    public float acceleration = 15f;
    public float directionSmooth = 15f;

    private Animator animator;
    private Transform visual;

    private float moveVelocity = 0f;
    private float moveDir = 1f;
    private float moveDirOld = 1f;
    private float lastPlayTime;
    public float minTimeBetweenSteps = 0.15f;
    private string currentAction = "idle";
    public bool isAutoMoving = false; //<-- player dk
    private float targetSpeed;

    public float currentPosX
    {
        get
        {
            if (animator != null)
            {
                return animator.transform.position.x;
            }
            return 0f;
        }
       
    }
    
    public int currentLookDirection
    {
        get
        {
            if (animator != null)
            {
                return animator.GetInteger("LookDirection");
            }
            return -1;
        }

        set
        {
            if (animator == null) return;
            animator.SetInteger("LookDirection", value);
        }
    }
    void Start()
    {
        if (isAutoMoving) return; // <-- chỉ kiểm tra ở đây, trong khi hàm update kg có kiểm tra
        animator = GetComponentInChildren<Animator>();
        if (animator != null) visual = animator.transform;

        // Đảm bảo AudioSource đã được gán
        if (footstepAudio == null) footstepAudio = GetComponent<AudioSource>();
        this.currentLookDirection = -1;
        //animator.SetFloat("LookDirection", 1);
    }
    void Update()
    {
      
        if (isAutoMoving) return;

        // 1. Logic dừng lại khi không nhấn chuột
        if (!Input.GetMouseButton(0))
        {
            moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);
            if (animator != null) animator.SetFloat("Speed", idleAnim);

            nextStepTime = Time.time + stepInterval;
            return;
        }
        this.currentLookDirection = 0; // chuyển sang trạng thái side view
        // 2. Logic di chuyển theo chuột
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        float distance = mouse.x - transform.position.x;

        // -------------------------------
        // ⭐ DỪNG LẠI KHI GẦN TỚI VỊ TRÍ CHUỘT
        // -------------------------------
       

        if (Mathf.Abs(distance) < stopThreshold)
        {
            moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);

            if (animator != null) animator.SetFloat("Speed", idleAnim);

            // Reset sound timer để không phát âm thanh
            nextStepTime = Time.time + stepInterval;

            return; // QUAN TRỌNG
        }
        // -------------------------------

        float targetDir = Mathf.Sign(distance);
        moveDir = Mathf.Lerp(moveDir, targetDir, Time.deltaTime * directionSmooth);
        moveDirOld = moveDir;

        // 3. Chọn chế độ và tốc độ
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool slow = Input.GetKey(KeyCode.LeftControl);

        
        float animValue;
        float currentStepInterval;

        if (slow)
        {
            targetSpeed = slowWalkSpeed;
            animValue = slowWalkAnim;
            currentStepInterval = slowInterval;
            currentAction = "walk-slow";
        }
        else if (run)
        {
            targetSpeed = runSpeed;
            animValue = runAnim;
            currentStepInterval = runInterval;
            currentAction = "run";
        }
        else
        {
            targetSpeed = walkSpeed;
            animValue = walkAnim;
            currentStepInterval = walkInterval;
            currentAction = "walk";
        }

        // 4. Footstep (bạn có thể mở lại nếu muốn)
        // if (Time.time >= nextStepTime)
        // {
        //     PlayFootstep();
        //     nextStepTime = Time.time + currentStepInterval;
        // }

        // 5. Apply movement
        if (animator != null) animator.SetFloat("Speed", animValue);

        moveVelocity = Mathf.Lerp(moveVelocity, targetSpeed, Time.deltaTime * acceleration);

        transform.position += new Vector3(moveDir * moveVelocity * Time.deltaTime, 0, 0);

        if (visual != null)
            visual.localScale = new Vector3(moveDir > 0 ? 1 : -1, 1, 1);
    }

   

    void PlayFootstep(string action)
    {
        // biết rằng có biến parivate là  currentAction-> walk, run, walk-slow hoặc idel nó trùng với tên của file anim
        // làm sao lấy tên của file anim hiện tại để play
        if (action != currentAction) return;
        if (footstepAudio != null && footstepAudio.clip != null)
        {
            footstepAudio.pitch = Random.Range(0.85f, 1.15f); // Làm tiếng bước chân tự nhiên
            footstepAudio.PlayOneShot(footstepAudio.clip);
        }
    }
   
    public IEnumerator WalkToTarget(Vector3 targetPos, float speed, int finalDirection)
    {
        isAutoMoving = true; // Bật khóa
        targetSpeed = walkSpeed;
        float animValue = walkAnim;
        float currentStepInterval = walkInterval;
        float distance = targetPos.x - transform.position.x; // evalute distance
        float targetDir = Mathf.Sign(distance);
        //moveDir = Mathf.Lerp(moveDir, targetDir, Time.deltaTime * directionSmooth); // calculate walk direction
        Debug.Log("Huong nhinh moi:"+ moveDir.ToString());
        Debug.Log("Huong nhinh cu:" + moveDirOld.ToString());
        currentAction = "walk"; // current action is "walk" make sure correct footstep sound
        // 1. Bật animation đi bộ
        if (animator != null) animator.SetFloat("Speed", animValue); // tell tree blen use "walk.anim"

        moveVelocity = Mathf.Lerp(moveVelocity, targetSpeed, Time.deltaTime * acceleration);

        // 2. Quay mặt về hướng mục tiêu
        moveDir = targetPos.x > transform.position.x ? 1 : -1;
        if (visual != null) visual.localScale = new Vector3(moveDir, 1, 1);

        // 3. Di chuyển
        // 3. Di chuyển
        while (Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            // Tạo một điểm đích mới có Y bằng với Y hiện tại của nhân vật
            Vector3 flatTarget = new Vector3(targetPos.x, transform.position.y, transform.position.z);

            // Di chuyển tới flatTarget thay vì targetPos gốc
            // transform.position = Vector2.MoveTowards(transform.position, flatTarget, speed * Time.deltaTime);
            moveVelocity = Mathf.Lerp(moveVelocity, targetSpeed, Time.deltaTime * acceleration);
            transform.position = Vector2.MoveTowards(transform.position, flatTarget, targetSpeed * Time.deltaTime);
            // Nếu đã đến gần X mục tiêu thì dừng vòng lặp (tránh đi quá đà)
            if (Mathf.Abs(transform.position.x - targetPos.x) < stopThreshold) break;

            yield return null;
        }
        //moveDir = Mathf.Lerp(moveDir, targetPos.x, Time.deltaTime * directionSmooth);
        // 4. Đến nơi: Dừng lại và quay hướng theo ý muốn
        // 4. ĐẾN NƠI: Dừng hẳn và thực hiện quay hướng
        // Cập nhật lại moveVelocity về 0 để tránh bị trượt do Lerp cũ
        moveVelocity = 0;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0); // Chuyển về trạng thái đứng yên

            // Ép Animator cập nhật ngay lập tức giá trị quay lưng
            //animator.SetInteger("LookDirection", finalDirection);
            this.currentLookDirection = finalDirection;
            // Debug thử xem giá trị thực tế trong Animator có nhảy không
            Debug.Log("Đã set LookDirection thành: " + animator.GetInteger("LookDirection"));
            //if (visual != null)
            //    visual.localScale = new Vector3(moveDir > 0 ? -1 : 1, 1, 1);
            
            
        }
        Debug.Log("Da dung");
        Debug.Log("Huong nhinh moi:" + moveDir.ToString());
        Debug.Log("Huong nhinh cu:" + moveDirOld.ToString());
        isAutoMoving = false; // Mở khóa để người chơi tiếp tục điều khiển
    }
}
