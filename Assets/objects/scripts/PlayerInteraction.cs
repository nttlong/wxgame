using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float autoMoveSpeed = 3f;
    public KeyCode interactKey = KeyCode.E;

    private IInteractable currentTarget;
    private bool isInteracting = false;

    private nhanVatNu playerMovement; // script di chuyển của bạn

    void Start()
    {
        playerMovement = GetComponent<nhanVatNu>();
    }

    void Update()
    {
        if (currentTarget != null && !isInteracting)
        {
            if (Input.GetKeyDown(interactKey))
            {
                StartCoroutine(DoInteraction());
            }
        }
    }

    private System.Collections.IEnumerator DoInteraction()
    {
        isInteracting = true;

        // Tắt control người chơi
        playerMovement.enabled = false;

        // Đi đến đúng điểm tương tác
        Transform targetPoint = currentTarget.GetInteractionPoint();
        yield return StartCoroutine(playerMovement.WalkToTarget(targetPoint.position, autoMoveSpeed, 1));

        // Thực hiện tương tác
        currentTarget.Interact(this);

        // Bật lại control
        playerMovement.enabled = true;
        isInteracting = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IInteractable interact))
        {
            currentTarget = interact;
            InteractableHighlighter.Show(interact.GetHintText(), col.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent(out IInteractable interact))
        {
            if (currentTarget == interact)
            {
                currentTarget = null;
                InteractableHighlighter.Hide();
            }
        }
    }
}
