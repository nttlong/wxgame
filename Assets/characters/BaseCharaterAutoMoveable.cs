using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharaterAutoMoveable : BaseCharacter
{
    public bool IsMoving { get; internal set; }

    // Start is called before the first frame update
    public IEnumerator MovetoTarget(Vector3 targetPos)
    {
        // Game 2D → khóa Y và Z
        targetPos.y = transform.position.y;
        targetPos.z = transform.position.z;

        // Xác định hướng trái/phải
        if (targetPos.x >= transform.position.x)
            direction = DirectionEnum.Forward;
        else
            direction = DirectionEnum.Backward;

        ShowDirection(direction);

        // Chọn tốc độ theo trạng thái hiện tại
        float speed = 0f;
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
                speed = 0f;
                break;
        }

        // Bật animation tương ứng
        ShowMotion(currentStatus);
        Debug.Log("MovetoTarget with currentStatus=" + currentStatus.ToString());
        IsMoving = true;
        // Di chuyển cho đến khi tới nơi
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
            yield return null;
        }

        // Đến nơi
        currentStatus = MotionEnum.Idle;
        ShowMotion(currentStatus);
        IsMoving = false;
    }

}
