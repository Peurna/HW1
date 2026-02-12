using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{
    public InputActionReference action;

    void Start()
    {
        if (action != null)
        {
            action.action.Enable(); 
            
            action.action.performed += (ctx) =>
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            };
        }
    }
}