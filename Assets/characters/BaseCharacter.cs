using System.Collections;
using Unity.Properties;
using UnityEngine;

public enum MotionEnum
{
    None,
    Idle,
    SlowWalk,
    Walk,
    Run,
    SitDown
   
}

public enum DirectionEnum
{
    Forward,
    Backward,
    BackView,
    FrontView
}
public enum HoldEquipmentEnum
{
    None,
    Up,
    Down,
}

public class BaseCharacter : MonoBehaviour
{
    public HoldEquipmentEnum holdEquipment;
    protected Animator animator;
    private Collider2D col;

    //private float beforeStopSpeed;
    protected float currentSpeed;
    private MotionEnum _currentStatus;
    public MotionEnum currentStatus
    {
        get
        {
            return _currentStatus;
        } set
        {
            if(value==MotionEnum.Idle)
            {
                _currentStatus = value;
            }
            _currentStatus = value;
        }
    }
    public DirectionEnum direction;
    public int currentDirection { get; private set; }
    [Header("Step distances per animation cycle")]
    public float slowWalkStepDistance = 0.25f;
    public float walkStepDistance = 0.55f;
    public float runStepDistance = 1.1f;

    [Header("Animation cycle durations (seconds per loop)")]
    public float slowWalkCycle = 0.6f;   // thời gian 1 vòng slow walk
    public float walkCycle = 0.45f;      // thời gian 1 vòng walk
    public float runCycle = 0.30f;       // thời gian 1 vòng run
   

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        col = GetComponentInChildren<Collider2D>();
        if (animator ==null)
        {
            Debug.LogError("animator is null");
        }
        if (col == null)
        {
            Debug.LogError("Colider 2D not setup");
        }
    }
    public bool IsMouseOnPlayer()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Collider2D col = GetComponent<Collider2D>();

        return col.OverlapPoint(mouseWorld);
    }
    #region MotionV iew
    /// <summary>
    /// Show motion Walk
    /// </summary>
    public void ShowWalk()
    {
        currentSpeed = 2f;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Sit", false);
        this.HoldEquipment(holdEquipment);

    }
    public void ShowSitDown()
    {
        currentSpeed = 4f;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Sit", true);
        this.HoldEquipment(holdEquipment);

    }
    /// <summary>
    /// Show motion Idle
    /// </summary>
    public void ShowIdle()
    {
        currentSpeed = 0;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Sit", false);
        this.HoldEquipment(holdEquipment);
    }
    /// <summary>
    /// Show motion Run
    /// </summary>
    public void ShowIRun()
    {
        currentSpeed = 3;
        animator.SetInteger("LookDirection", 0);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Sit", false);
        this.HoldEquipment(holdEquipment);
    } 
    #endregion
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
        currentDirection = 1;
        animator.SetInteger("LookDirection", 0);
        animator.transform.localScale = new Vector3(currentDirection, 1, 1);
        //currentSpeed = beforeStopSpeed;
        //animator.SetFloat("Speed", currentSpeed);
    }
    public void DirectionBackward()
    {
        currentDirection = -1;
        animator.SetInteger("LookDirection", 0);
        animator.transform.localScale = new Vector3(currentDirection, 1, 1);
        
        //currentSpeed = beforeStopSpeed;
        //animator.SetFloat("Speed", currentSpeed);
    }
    public void HoldEquipment(HoldEquipmentEnum holdEquipment)
    {
        if (holdEquipment == HoldEquipmentEnum.None) {
            SetLayerWeights(0, 0);
            return;
        }
        if (currentDirection>0)
        {
            SetLayerWeights(1f, 0f);
        } else
        {
            SetLayerWeights(0f, 1f);
        }
           
    }

    void SetLayerWeights(float backLayerWeight, float frontLayerWeight)
    {

        if (animator == null) return;

        animator.SetLayerWeight(1, backLayerWeight);

        animator.SetLayerWeight(2, frontLayerWeight);
       

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
                ShowIdle();
                break;

            case MotionEnum.SlowWalk:     // slow walk
                animator.SetFloat("Speed", 1);
                break;

            case MotionEnum.Walk:         // walk
                ShowWalk();
                break;

            case MotionEnum.Run:
                ShowIRun();
                break;
            case MotionEnum.SitDown:
                ShowSitDown();
                break;

           

            default:
                Debug.LogWarning("Unknown status: " + status);
                break;
        }
    }
    

    protected virtual void OnUpdate() { }

    
    
    void Update()
    {
        
        OnUpdate();
        
    }
    
}
