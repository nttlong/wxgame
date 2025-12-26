using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
public enum InteractStatusEnum
{
    None,
    Moving,
    Interact,
    Wait
}
public class BaseCharaterAutoMoveable : BaseCharacter
{
    public bool IsMoving { get; internal set; }

    private Transform currentTragetObject;
    private Coroutine curentMoveRoutine;
    protected InteractStatusEnum interactStatus;
    public GameObject currentInteractObject;

    // Start is called before the first frame update
    public IEnumerator MovetoTarget(Vector3 targetPos,Action OnFinish, Action OnMoving)
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
        if (currentStatus==MotionEnum.Idle)
        {
            currentStatus = MotionEnum.Walk;
        }
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
        Debug.Log("I'm moving to " + this.currentTragetObject.name);
        // Bật animation tương ứng
        //ShowMotion(currentStatus);
        Debug.Log("MovetoTarget with currentStatus=" + currentStatus.ToString());
        this.interactStatus = InteractStatusEnum.Moving;
        // Di chuyển cho đến khi tới nơi
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
            this.interactStatus = InteractStatusEnum.Moving;
            OnMoving();
            yield return null;
        }


        // Đến nơi
        currentStatus = MotionEnum.Idle;
        //ShowMotion(currentStatus);
        this.interactStatus = InteractStatusEnum.Wait;
        OnFinish();
    }
    public void StartMoveRoutine(Transform currentTragetObject, Vector3 targetPos,Action OnFinish,Action OnMoving)
    {
        this.currentTragetObject = currentTragetObject;
       
        curentMoveRoutine = StartCoroutine(MovetoTarget(targetPos, () =>
        {
            OnFinish();
            if (this.currentTragetObject != null)
            {
                Debug.Log("I stop at  " + this.currentTragetObject.name);
                this.currentTragetObject = null;
            } else
            {
                Debug.Log("I stop at  nothing");
            }
        },OnMoving));
    }
    public void StopMoveRoutine(Action AfterStop)
    {
        IsMoving = false;
        if (curentMoveRoutine != null)
            StopCoroutine(curentMoveRoutine);
        curentMoveRoutine = null;
        this.interactStatus = InteractStatusEnum.None;
        AfterStop();
    }

    public bool IsInInteractionObject(Action onGameplyMoving)
    {


        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        Vector2 mousePos2D = new Vector2(mouseWorld.x, mouseWorld.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (this.interactStatus==InteractStatusEnum.Moving)
        {
            this.StopMoveRoutine(() =>
            {
                
            });
            //return ;
        }
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == this.currentInteractObject && this.interactStatus==InteractStatusEnum.Moving)
            {
                return true;
            }
            this.currentInteractObject = hit.collider.gameObject;
            
            Debug.Log("Collider hit: " + hit.collider.name);
            // Debug.Log("di chuyển đến vị trí click thực sự");

           
            StartMoveRoutine(this.transform, hit.point, () =>
            {
                this.currentInteractObject=null;
            },onGameplyMoving);
            return true;
        } else
        {
            this.interactStatus=InteractStatusEnum.None;
            return false;
        }
        
    }
}
