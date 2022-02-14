using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHPlayableCharacter : EHActor
{
    [SerializeField]
    protected EHPlayerController PlayerController;
    [SerializeField]
    protected EHThirdPersonCamera ThirdPersonCamera;
    
    #region monobehaviour methods

    protected override void Awake()
    {
        base.Awake();
        InitializeController();
    }

    #endregion monobehaviour methods
    
    #region control functions
    private void InitializeController()
    {
        PlayerController.SetAxisInputAction(EHPlayerController.Axis_HorizontalCamera, LookRight);
        PlayerController.SetAxisInputAction(EHPlayerController.Axis_VerticalCamera, LookUp);
    }

    public void MoveUp(float VInput)
    {
        
    }
    public void MoveRight(float HInput)
    {
        
    }

    public void LookRight(float HInput)
    {
        ThirdPersonCamera.LookRight(HInput);
    }

    public void LookUp(float VInput)
    {
        ThirdPersonCamera.LookUp(VInput);
    }
    
    #endregion control functions
}
