using UnityEngine;
using System.Collections;

public class DoorControllerTest : MonoBehaviour
{
    public Transform player; // Kéo nhân vật vào đây
    public Animator playerAnim; // Kéo Animator của nhân vật vào đây
    public float moveSpeed = 5f;
    private bool isMoving = false;

    // XÓA đoạn Animator anim; ở đây vì cửa không có anim

    void Start()
    {
        // XÓA dòng anim = GetComponent<Animator>(); vì nó gây lỗi MissingComponent
    }

    void OnMouseDown()
    {
        var scriptNV = player.GetComponent<nhanVatNu>();
        StartCoroutine(scriptNV.WalkToTarget(transform.position, moveSpeed, 1));
       
    }

    
}