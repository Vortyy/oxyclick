using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform lookAt;

    [Header("Tweaks")]
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);

    [Header("Bounds")]
    [SerializeField] private bool enableBounds = true;
    [SerializeField] private float bounds = 3f;

    [Header("Smooth")]
    [SerializeField] private bool enableSmooth = true;
    [SerializeField] private float smoothSpeed = 10.0f;
    private Vector3 desiredPosition;

    private void LateUpdate()
    {
        desiredPosition = lookAt.position + offset;

        if (enableBounds)
        {
            float deltaX = lookAt.position.x - transform.position.x;
            if (Mathf.Abs(deltaX) > bounds)
            {
                if (deltaX > 0)
                {
                    desiredPosition.x = lookAt.position.x - bounds;
                }
                else
                {
                    desiredPosition.x = lookAt.position.x + bounds;
                }
            }
            else
            {
                desiredPosition.x = lookAt.position.x - deltaX;
            }
        }

        if (enableSmooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
