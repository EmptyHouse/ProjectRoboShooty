using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EInputState
{
    ButtonPressed,
    ButtonReleased,
}

public class EHPlayerController : MonoBehaviour
{
    public const string Axis_HorizontalCamera = "HCamera";
    public const string Axis_VerticalCamera = "VCamera";
    public const string Axis_MoveForward = "Forward";
    public const string Axis_MoveRight = "Right";
    public const string Button_Jump = "Jump";

    [SerializeField]
    private FButtonInput[] ButtonInputs;
    [SerializeField]
    private FAxisInput[] AxisInputs;
    private void Update()
    {
        foreach (FAxisInput Axis in AxisInputs)
        {
            if (Axis.AxisValue != Input.GetAxisRaw(Axis.AxisName))
            {
                Axis.AxisValue = Input.GetAxisRaw(Axis.AxisName);
                Axis.AxisChangedEvent.Invoke(Axis.AxisValue);
            }
        }
    }

    public void SetButtonInputAction(string InputName, UnityAction ActionEvent, EInputState InputState)
    {
        foreach (FButtonInput ButtonInput in ButtonInputs)
        {
            switch (InputState)
            {
                case EInputState.ButtonPressed:
                    ButtonInput.ButtonPressedEvent += ActionEvent;
                    return;
                case EInputState.ButtonReleased:
                    ButtonInput.ButtonReleasedEvent += ActionEvent;
                    return;
            }
        }
        Debug.LogWarning("Did not find Input for NAME: " + InputName);
    }

    public void SetAxisInputAction(string ActionName, UnityAction<float> ActionEvent)
    {
        foreach (FAxisInput AxisInput in AxisInputs)
        {
            if (AxisInput.AxisName == ActionName)
            {
                AxisInput.AxisChangedEvent += ActionEvent;
                return;
            }
        }
    }
    

    [System.Serializable]
    private class FButtonInput
    {
        public string ButtonInputName;
        public bool CurrentInput;
        public UnityAction ButtonPressedEvent { get; set; }
        public UnityAction ButtonReleasedEvent { get; set; }
    }

    [System.Serializable]
    private class FAxisInput
    {
        public string AxisName;
        public float AxisValue;
        public UnityAction<float> AxisChangedEvent { get; set; }
    }
}
