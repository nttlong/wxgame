using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteactRegionOnGround : InteractRegion, IInteractable
{
    public new void Interact(BaseCharacter baseCharacter, Action AfterInteract)
    {
        //StopCharacterStopMotion(baseCharacter);
        
        //baseCharacter.ShowMotion(MotionEnum.SitDown);
        baseCharacter.currentStatus=MotionEnum.SitDown;
        Debug.Log("Interact with :" + transform.name);
        AfterInteract();
    }
}
