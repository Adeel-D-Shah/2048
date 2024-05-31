using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform parentTransform; // The transform of the parent object to follow
    public float positionSmoothTime = 0.3f; // Smooth time for position
    public float rotationSmoothTime = 0.3f; // Smooth time for rotation

    private Vector3 positionVelocity = Vector3.zero;
    private Quaternion rotationVelocity;

    void Update()
    {
        // Smoothly follow the parent's position
        if (parentTransform != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, parentTransform.position, ref positionVelocity, positionSmoothTime);

            // Smoothly follow the parent's rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, parentTransform.rotation, rotationSmoothTime * Time.deltaTime);
        }
    }
}
