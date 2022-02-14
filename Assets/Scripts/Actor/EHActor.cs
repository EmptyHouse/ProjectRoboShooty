using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHActor : MonoBehaviour
{
    public Vector3 Location => transform.position;

    public Vector3 LocalScale => transform.localScale;

    public Quaternion Rotation => transform.rotation;
    public Collider PhysicsCollider { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody Rigid { get; private set; }
    
    #region monobehaviour methods

    protected virtual void Awake()
    {
        PhysicsCollider = GetComponent<Collider>();
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody>();
    }
    #endregion monobehaviour methods
    
    public void SetLocation(Vector3 Location, bool SetSafe = false)
    {
        if (SetSafe)
        {
            RaycastHit hit;
            Vector3 LocationOffset = Location - transform.position;
            if (Rigid.SweepTest(LocationOffset, out hit, LocationOffset.magnitude))
            {
                Location = hit.point;
            }
        }
        this.transform.position = Location;
    }
}
