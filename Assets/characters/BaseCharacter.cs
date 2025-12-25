using UnityEngine;

public enum MotionEnum
{
    Idle,
    SlowWalk,
    Walk,
    Run,
   
}

public enum DirectionEnum
{
    Forward,
    Backward,
    BackView,
    FrontView
}


public class BaseCharacter : MonoBehaviour
{
    protected Animator animator;
    //private float beforeStopSpeed;
    protected float currentSpeed;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator ==null)
        {
            Debug.Log("animator is null");
        }
    }
    /// <summary>
    /// Show motion Walk
    /// </summary>
    public void ShowWalkAnim()
    {
        currentSpeed = 2f;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
        
    }
    /// <summary>
    /// Show motion Idle
    /// </summary>
    public void ShowIdleAnim()
    {
        currentSpeed = 0;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
    }
    /// <summary>
    /// Show motion Run
    /// </summary>
    public void ShowIdleRun()
    {
        currentSpeed = 3;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
    }
    /// <summary>
    /// Character look direct Camera (Character-><- camera)
    /// </summary>
    public void DirectionFront()
    {
        //beforeStopSpeed = currentSpeed;
        currentSpeed = 0;
        //animator.SetFloat("Speed", currentSpeed);
        animator.SetInteger("LookDirection", 2);
    }
    /// <summary>
    /// Character look same  Camera direction (<- Character->camera)
    /// </summary>
    public void DirectionBack()
    {
        //beforeStopSpeed = currentSpeed;
        currentSpeed = 0;
        //animator.SetFloat("Speed", currentSpeed);
        animator.SetInteger("LookDirection", 1);
    }
    public void DirectionForward()
    {
        animator.SetInteger("LookDirection", 0);
        animator.transform.localScale = new Vector3(1, 1, 1);
        //currentSpeed = beforeStopSpeed;
        //animator.SetFloat("Speed", currentSpeed);
    }
    public void DirectionBackward()
    {
        animator.SetInteger("LookDirection", 0);
        animator.transform.localScale = new Vector3(-1, 1, 1);
        //currentSpeed = beforeStopSpeed;
        //animator.SetFloat("Speed", currentSpeed);
    }


    void SetLayerWeights(float backLayerWeight, float frontLayerWeight)
    {

        if (animator == null) return;

        animator.SetLayerWeight(1, backLayerWeight);

        animator.SetLayerWeight(2, frontLayerWeight);
        Debug.Log("backLayerWeight:" + backLayerWeight.ToString());
        Debug.Log("frontLayerWeight:" + frontLayerWeight.ToString());

    }
    public void ShowDirection(DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.Forward:
                this.DirectionForward(); break;
            case DirectionEnum.Backward:
                this.DirectionBackward(); break;
            case DirectionEnum.FrontView:
                this.DirectionFront(); break;
            case DirectionEnum.BackView:
                this.DirectionBack(); break;
        }
    }
    public void ShowMotion(MotionEnum status)
    {
        switch (status)
        {
            case MotionEnum.Idle:
                ShowIdleAnim();
                break;

            case MotionEnum.SlowWalk:     // slow walk
                animator.SetFloat("Speed", 1);
                break;

            case MotionEnum.Walk:         // walk
                ShowWalkAnim();
                break;

            case MotionEnum.Run:
                ShowIdleRun();
                break;

           

            default:
                Debug.LogWarning("Unknown status: " + status);
                break;
        }
    }
    protected virtual void Start() { }

    protected virtual void OnUpdate() { }

    void Update()
    {
        Debug.Log("BaseCharacter Update");
        OnUpdate();
        SetLayerWeights(0f, 1f);
    }
}
