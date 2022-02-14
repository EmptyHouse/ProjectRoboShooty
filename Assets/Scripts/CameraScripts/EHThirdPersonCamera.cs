using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHThirdPersonCamera : MonoBehaviour
{
    [SerializeField, Tooltip("The speed of our camera when moving around the Y axis")]
    protected float HorizontalSpeed = 10f;
    [SerializeField, Tooltip(("The speed of our camera when moving around the X axis"))]
    protected float VerticalSpeed = 10f;
    [SerializeField, Tooltip("The radius of the camera around the target")]
    protected float CameraRadius = 2f;

    public EHPlayableCharacter TargetCharacter;
    private Vector2 CurrentCameraRotation;
    private Vector2 PreviousCameraRotation;
    private Vector3 TargetPositionOffset;

    private void Awake()
    {
        SetTargetTransform(GetComponentInParent<EHPlayableCharacter>());
        if (TargetCharacter)
        {
            SetTargetTransformOffset(transform.position - TargetCharacter.Location);
        }
        transform.SetParent(null);
    }

    private void Update()
    {
        UpdatePositionAndRotation();
    }

    public void LookUp(float VInput)
    {
        if (VInput == 0) return;
        CurrentCameraRotation.x += VerticalSpeed * VInput * Time.deltaTime;
        CurrentCameraRotation.x %= 360;
    }

    public void LookRight(float HInput)
    {
        if (HInput == 0) return;
        CurrentCameraRotation.y += HorizontalSpeed * HInput * Time.deltaTime;
        CurrentCameraRotation.y %= 360;
    }

    public void SetTargetTransform(EHPlayableCharacter TargetCharacter)
    {
        this.TargetCharacter = TargetCharacter;
    }

    public void SetTargetTransformOffset(Vector3 Offset)
    {
        this.TargetPositionOffset = Offset;
    }

    private void UpdatePositionAndRotation()
    {
        transform.rotation = Quaternion.Euler(CurrentCameraRotation);
        PreviousCameraRotation = CurrentCameraRotation;
        Vector3 OffsetRadius = -transform.forward * CameraRadius;
        transform.position = TargetCharacter.Location + TargetPositionOffset + OffsetRadius;
    }
}
