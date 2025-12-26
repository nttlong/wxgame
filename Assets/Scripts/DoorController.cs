using UnityEngine;
using System.Collections;

public class DoorControllerTest : MonoBehaviour
{
    public Transform AutoMovableObjec;

    void OnMouseDown()
    {

        


    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = AutoMovableObjec.position.z;

            Vector2 mousePos2D = new Vector2(mouseWorld.x, mouseWorld.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                var scriptNV = AutoMovableObjec.GetComponent<BaseCharaterAutoMoveable>();

                // di chuyển đến vị trí click thực sự
                StartCoroutine(scriptNV.MovetoTarget(hit.point));
            }
            else
            {
                Debug.Log("KHÔNG TRÚNG");
            }
        }
    }

}