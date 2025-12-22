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
    private float lastPlayTime;
    public float minTimeBetweenSteps = 0.15f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator != null) visual = animator.transform;

        // Đảm bảo AudioSource đã được gán
        if (footstepAudio == null) footstepAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 1. Logic dừng lại khi không nhấn chuột
        if (!Input.GetMouseButton(0))
        {
            moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);
            if (animator != null) animator.SetFloat("Speed", idleAnim);

            nextStepTime = Time.time + stepInterval;
            return;
        }

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

        // 3. Chọn chế độ và tốc độ
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool slow = Input.GetKey(KeyCode.LeftControl);

        float targetSpeed;
        float animValue;
        float currentStepInterval;

        if (slow)
        {
            targetSpeed = slowWalkSpeed;
            animValue = slowWalkAnim;
            currentStepInterval = slowInterval;
        }
        else if (run)
        {
            targetSpeed = runSpeed;
            animValue = runAnim;
            currentStepInterval = runInterval;
        }
        else
        {
            targetSpeed = walkSpeed;
            animValue = walkAnim;
            currentStepInterval = walkInterval;
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

    void UpdateOld()
    {
        // 1. Logic dừng lại khi không nhấn chuột
        //if (!Input.GetMouseButton(0))
        //{
        //    moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);
        //    if (animator != null) animator.SetFloat("Speed", idleAnim);
        //    return;
        //}
        // 1. Logic dừng lại khi không nhấn chuột
        if (!Input.GetMouseButton(0))
        {
            moveVelocity = Mathf.Lerp(moveVelocity, 0, Time.deltaTime * acceleration);
            if (animator != null) animator.SetFloat("Speed", idleAnim);

            // --- THÊM DÒNG NÀY ---
            // Reset thời gian để khi bấm chuột lại, nó không phát âm thanh ngay lập tức
            nextStepTime = Time.time + stepInterval;
            // ---------------------

            return;
        }
        // 2. Logic di chuyển theo chuột
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        float distance = mouse.x - transform.position.x;
        float targetDir = Mathf.Sign(distance);
        moveDir = Mathf.Lerp(moveDir, targetDir, Time.deltaTime * directionSmooth);

        // 3. Chọn chế độ và Tốc độ phát âm thanh
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool slow = Input.GetKey(KeyCode.LeftControl);

        float targetSpeed;
        float animValue;
        float currentStepInterval = stepInterval; // Mặc định

        if (slow)
        {
            targetSpeed = slowWalkSpeed;
            animValue = slowWalkAnim;
            currentStepInterval = slowInterval; // Đi chậm thì tiếng bước chân thưa ra
        }
        else if (run)
        {
            targetSpeed = runSpeed;
            animValue = runAnim;
            currentStepInterval =runInterval; // Chạy thì tiếng bước chân dồn dập hơn
        }
        else
        {
            targetSpeed = walkSpeed;
            animValue = walkAnim;
            currentStepInterval = walkInterval;
        }

        // 4. Phát âm thanh bước chân
        //if (Time.time >= nextStepTime)
        //{
        //    PlayFootstep();
        //    nextStepTime = Time.time + currentStepInterval;
        //}

        // 5. Áp dụng di chuyển và Animation
        if (animator != null) animator.SetFloat("Speed", animValue);
        moveVelocity = Mathf.Lerp(moveVelocity, targetSpeed, Time.deltaTime * acceleration);
        transform.position += new Vector3(moveDir * moveVelocity * Time.deltaTime, 0, 0);

        if (visual != null) visual.localScale = new Vector3(moveDir > 0 ? 1 : -1, 1, 1);
    }

    void PlayFootstep()
    {
        if (footstepAudio != null && footstepAudio.clip != null)
        {
            footstepAudio.pitch = Random.Range(0.85f, 1.15f); // Làm tiếng bước chân tự nhiên
            footstepAudio.PlayOneShot(footstepAudio.clip);
        }
    }
    void PlayFootstepOnHitGround(string clipName)
    {
        // 1. Chống lặp tiếng quá dày trong thời gian cực ngắn
        if (Time.time - lastPlayTime < minTimeBetweenSteps) return;

        // 2. Kiểm tra trọng số (Weight) của Animation
        // Nếu Clip đó đang mờ dần (Weight thấp) thì không phát âm thanh
        if (animator != null)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Nếu bạn dùng Blend Tree, chúng ta kiểm tra xem tốc độ có khớp không
            // Hoặc đơn giản là kiểm tra xem Clip truyền vào có đang đóng vai trò chính không
            float currentSpeed = animator.GetFloat("Speed");

            if (clipName == "walk" && currentSpeed > 1.2f) return; // Đang chạy thì bỏ qua tiếng đi bộ
            if (clipName == "run" && currentSpeed < 0.8f) return;  // Đang đi bộ thì bỏ qua tiếng chạy
        }

        // 3. Phát âm thanh
        if (footstepAudio != null && footstepAudio.clip != null)
        {
            footstepAudio.pitch = Random.Range(0.9f, 1.1f);
            footstepAudio.PlayOneShot(footstepAudio.clip);
            lastPlayTime = Time.time;
        }
    }
    //footstepAudio.PlayOneShot(footstepAudio.clip);
    //footstepAudio.pitch = Random.Range(0.85f, 1.15f); // Làm tiếng bước chân tự nhiên
    //footstepAudio.PlayOneShot(footstepAudio.clip);
}
