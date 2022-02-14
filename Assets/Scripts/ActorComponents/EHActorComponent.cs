using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class EHActorComponent : MonoBehaviour
{
    public EHActor AssociatedActor;
    
    #region monobehaviour methods
    protected virtual void Awake()
    {
        AssociatedActor = GetComponent<EHActor>();
    }
    #endregion monobehaviour methods

    public Vector3 GetActorLocation()
    {
        return AssociatedActor.Location;
    }

    public void SetActorLocation(Vector3 Location, bool SetSafe = false)
    {
        AssociatedActor.SetLocation(Location, SetSafe);
    }

    public Vector3 GetActorScale()
    {
        return AssociatedActor.LocalScale;
    }

    public Quaternion GetActorRotation()
    {
        return AssociatedActor.Rotation;
    }
}
