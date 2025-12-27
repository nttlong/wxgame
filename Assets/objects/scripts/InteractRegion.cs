using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;



[ExecuteAlways]
public class InteractRegion : MonoBehaviour, IInteractable
{
    public GameObject emptyGameObject;
    
    public BoxCollider2D Colider;

    public DoorDirectionEnum direction;

    public string GetName()
    {
        if(emptyGameObject== null) return null;
        return emptyGameObject.name;
    }

    public void StopCharacterStopMotion(BaseCharacter baseCharacter)
    {
        switch (direction)
        {
            case DoorDirectionEnum.Forward:
                baseCharacter.direction = DirectionEnum.Backward; break;
            case DoorDirectionEnum.Backward:
                baseCharacter.direction = DirectionEnum.Forward;
                break;

            case DoorDirectionEnum.Front:
                baseCharacter.direction = DirectionEnum.BackView;
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

    private void Awake()
    {
        if (emptyGameObject == null)
        {
            emptyGameObject = new GameObject();
            emptyGameObject.transform.position = transform.position;
            emptyGameObject.transform.rotation = transform.rotation;
            emptyGameObject.transform.localScale = transform.localScale;
            emptyGameObject.transform.SetParent(transform);
            Colider = emptyGameObject.transform.gameObject.AddComponent< BoxCollider2D>();
            Colider.isTrigger = true;
            //ColiderBox = transform.gameObject.AddComponent<BoxCollider2D>();
            //ColiderBox.isTrigger = true;

        }
    }

    void IInteractable.Awake()
    {
        Awake();
    }
}