
using UnityEngine;

public class NhanVatNuController : BaseCharaterAutoMoveable
{
   

    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    private Vector3 destination;
    private bool isMoving = false;

    
    protected override void OnUpdate()
    {
        if (base.IsMoving)
        {
            return;
        }
        HandleMouseInput();
        MoveCharacter();

        this.ShowMotion(this.currentStatus);
        this.ShowDirection(this.direction);
       
       
    }
    /// <summary>
    /// Bắt input từ chuột: click để di chuyển.
    /// Shift + click = chạy
    /// </summary>
    void HandleMouseInput()
    {
        // Người dùng đang giữ chuột trái
        if (Input.GetMouseButton(0))
        {
            // Chuột ở đâu → đi về đó
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;  // Giữ Z cố định
            destination = mouseWorld;
            isMoving = true;

            // Xác định trái/phải
            Vector3 dir = destination - transform.position;
            direction = dir.x >= 0 ? DirectionEnum.Forward : DirectionEnum.Backward;

            // Shift → chạy
            currentStatus = Input.GetKey(KeyCode.LeftShift) ? MotionEnum.Run : MotionEnum.Walk;
        }
        else
        {
            // Khi thả chuột → dừng lại
            isMoving = false;
            currentStatus = MotionEnum.Idle;
        }
    }
    /// <summary>
    /// Di chuyển nhân vật tới destination
    /// </summary>
    void MoveCharacter()
    {
        if (!isMoving) return;

        float speed = 0;

        switch (currentStatus)
        {
            case MotionEnum.SlowWalk:
                speed = slowWalkStepDistance;
                break;

            case MotionEnum.Walk:
                speed = walkStepDistance;
                break;

            case MotionEnum.Run:
                speed = runStepDistance;
                break;

            default:
                speed = 0;
                break;
        }

        // Giữ nhân vật ở mặt phẳng 2D
        destination.z = transform.position.z;
        destination.y = transform.position.y;
        Vector3 moveDir = (destination - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        // Tới nơi
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            isMoving = false;
            currentStatus = MotionEnum.Idle;
        }
    }
   
}
