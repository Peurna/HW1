using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public Transform playerTransform;
    public Transform roomPosition;
    public Transform outsidePosition;
    public InputActionReference toggleAction;
    private bool isOutside = false;

    void Start()
    {
        if (toggleAction != null)
            toggleAction.action.Enable();
        if (playerTransform != null && roomPosition != null)
            playerTransform.position = roomPosition.position;
    }

    void Update()
    {
        if (toggleAction != null && toggleAction.action.triggered)
        {
            TogglePosition();
        }
    }

    void TogglePosition()
    {
        isOutside = !isOutside;

        if (isOutside)
        {
            playerTransform.position = outsidePosition.position;
        }
        else
        {
            playerTransform.position = roomPosition.position;
        }
    }
}