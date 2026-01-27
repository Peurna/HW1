using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light roomLight;
    public InputActionReference toggleAction;

    void Start()
    {
        if (roomLight == null)
            roomLight = GetComponent<Light>();

        if (toggleAction != null)
            toggleAction.action.Enable();
    }

    void Update()
    {
        if (toggleAction != null && toggleAction.action.triggered)
        {
            ChangeLightColor();
        }
    }

    void ChangeLightColor()
    {
        roomLight.color = new Color(1, 1, 1);
    }
}