
using UnityEngine;

public class NhanVatNuController : BaseCharacter
{
    public MotionEnum currentStatus;
    public DirectionEnum direction;
    protected override void OnUpdate()
    {
        this.ShowMotion(this.currentStatus);
        this.ShowDirection(this.direction);
       
       
    }
}
