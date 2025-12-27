using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorDirectionEnum { 
    Forward, Backward, Front, Back 
}
public class Door : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
   
    public DoorDirectionEnum direction;
    private BoxCollider2D col;

    public void StopCharacterStopMotion(BaseCharacter baseCharacter)
    {
        switch (direction)
        {
            case DoorDirectionEnum.Forward:
                baseCharacter.direction=DirectionEnum.Backward; break;
            case DoorDirectionEnum.Backward:
                baseCharacter.direction=DirectionEnum.Forward;
                break;
            
            case DoorDirectionEnum.Front:
                baseCharacter.direction=DirectionEnum.BackView;
                break;
            case DoorDirectionEnum.Back:
                baseCharacter.direction = DirectionEnum.FrontView;
                break;
        }
    }
    public void Interact(BaseCharacter baseCharacter,Action AfterInteract)
    {
        StopCharacterStopMotion(baseCharacter);
        Debug.Log("Interact with :" + transform.name);
        AfterInteract();
    }

    public void Awake()
    {
        col=GetComponent<BoxCollider2D>();
        if (col==null)
        {
            throw new Exception(transform.name + " require Box Colider 2D");
        }
        col.isTrigger=true;
    }
}
