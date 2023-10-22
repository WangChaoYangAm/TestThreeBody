using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MySensorOnGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask _maskLayer;
    [SerializeField]
    private CapsuleCollider _capCollider;
    private Vector3 _point1, _point2;
    private float _radius;
    [SerializeField]
    private float _offsetRadius = 0.05f;

    private Action<bool> OnGroundAction;

  
    public void Init(Action<bool> onGround)
    {
        _radius = _capCollider.radius - _offsetRadius;
        OnGroundAction = onGround;
    }
    private void FixedUpdate()
    {
        _point1 = transform.position + transform.up * (_radius -_offsetRadius);
        _point2 = transform.position + transform.up * (_capCollider.height - _offsetRadius) - transform.up * _radius;
        var collidersOutput = Physics.OverlapCapsule(_point1, _point2, _radius, _maskLayer);
        if (collidersOutput.Length>0)
        {
            Debug.Log("OnGround");
            OnGroundAction?.Invoke(true);
            //foreach (var collider in collidersOutput)
            //{
            //    if(collider.com)
            //}
        }
        else
        {
            OnGroundAction?.Invoke(false);

        }
    }
}
