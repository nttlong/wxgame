using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
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
        //if (!isMoving) // Kiểm tra để không bấm liên tục khi đang đi
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(MoveToDoor());
        //}
    }

    // Nếu bạn muốn dùng phím để di chuyển nhân vật thủ công thì dùng Update
    // Còn nếu chỉ muốn click chuột rồi nó tự đi thì có thể bỏ qua Update
    //void Update()
    //{
    //    // Kiểm tra nếu có playerAnim thì mới set (để tránh lỗi)
    //    if (playerAnim != null && !isMoving)
    //    {
    //        float move = Input.GetAxis("Vertical");
    //        // Nếu bạn đang di chuyển bằng phím:
    //        if (Mathf.Abs(move) > 0.1f)
    //        {
    //            playerAnim.SetFloat("Speed", Mathf.Abs(move));
    //        }
    //    }
    //}

    IEnumerator MoveToDoor()
    {
        // Lấy script điều khiển nhân vật
        var characterScript = player.GetComponent<nhanVatNu>();
        if (characterScript != null) characterScript.isAutoMoving = true; // Khóa Update lại

        isMoving = true;
        playerAnim.SetInteger("LookDirection", 0);
        playerAnim.SetFloat("Speed", 1f);

        while (Vector2.Distance(player.position, transform.position) > 0.1f)
        {
            player.position = Vector2.MoveTowards(player.position, transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        playerAnim.SetFloat("Speed", 0f);
        playerAnim.SetInteger("LookDirection", 1);

        isMoving = false;
        if (characterScript != null) characterScript.isAutoMoving = false; // Mở khóa sau khi đi xong
    }
}