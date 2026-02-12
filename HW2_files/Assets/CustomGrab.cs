using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public InputActionProperty leftInput;
    public InputActionProperty rightInput;

    public float Distance = 0.2f;

    private List<Transform> activeHands = new List<Transform>();
    private Dictionary<Transform, Vector3> prevPos = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> prevRot = new Dictionary<Transform, Quaternion>();

    void Update()
    {
        CheckHandInput(leftHand, leftInput);
        CheckHandInput(rightHand, rightInput);

        if (activeHands.Count == 0) return;

        Vector3 totalTranslation = Vector3.zero;
        Quaternion totalRotation = Quaternion.identity;

        foreach (Transform hand in activeHands)
        {
            Vector3 dPos = hand.position - prevPos[hand];
            Quaternion dRot = hand.rotation * Quaternion.Inverse(prevRot[hand]);

            Vector3 offset = transform.position - prevPos[hand];
            Vector3 rotatedOffset = dRot * offset;
            Vector3 moveFromOrbit = rotatedOffset - offset;

            totalTranslation += (dPos + moveFromOrbit);
            totalRotation = dRot * totalRotation;
        }

        transform.position += totalTranslation / activeHands.Count;
        transform.rotation = totalRotation * transform.rotation;
        UpdatePreviousTransforms();
    }

    void CheckHandInput(Transform hand, InputActionProperty input)
    {
        if (input.action == null) return;

        bool isGripping = input.action.ReadValue<float>() > 0.5f;
        bool alreadyInList = activeHands.Contains(hand);

        if (isGripping && !alreadyInList)
        {
            if (Vector3.Distance(hand.position, transform.position) <= Distance)
            {
                activeHands.Add(hand);
                prevPos[hand] = hand.position;
                prevRot[hand] = hand.rotation;
            }
        }
        else if (!isGripping && alreadyInList)
        {
            activeHands.Remove(hand);
        }
    }

    void UpdatePreviousTransforms()
    {
        foreach (Transform hand in activeHands)
        {
            prevPos[hand] = hand.position;
            prevRot[hand] = hand.rotation;
        }
    }
}
