using UnityEngine;

public class ScopeCamera : MonoBehaviour
{
    public Transform lens;

    void Start()
    {
        transform.parent = null;
    }

    void LateUpdate()
    {
        if (lens != null && Camera.main != null)
        {
            transform.position = lens.position;
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, lens.up);
        }
    }
}