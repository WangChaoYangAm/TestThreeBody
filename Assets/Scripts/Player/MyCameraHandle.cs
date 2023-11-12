using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraHandle : MonoBehaviour
{
    public PlayerInput _playerInput;
    public float _speedHor = 1;
    public float _speedVert = 10;
    //private float tmpEulerX;
    //private float tmpEulerY;
    [SerializeField]
    private Vector2 _rangeHor, _rangeVert;
    [SerializeField]
    private Vector3 _startOffset;
    private Vector3 _tmpVertEuler,_tmpHorEuler;



    private GameObject _playerHandle,_cameraHandle;
    // Start is called before the first frame update
    void Start()
    {
        _cameraHandle = transform.parent.gameObject;
        _playerHandle = _cameraHandle.transform.parent.gameObject;
        this.transform.localPosition = _startOffset;
        _tmpHorEuler = Vector3.zero;
        _tmpVertEuler = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //_playerHandle.transform.Rotate(Vector3.up, _playerInput.Jup * _speedHor);
        //_cameraHandle.transform.Rotate(Vector3.right, -_playerInput.Jright * _speedVert);
        _tmpVertEuler.y += _playerInput.Jup * _speedHor;
        _tmpHorEuler.x -= _playerInput.Jright * _speedVert;
        //_tmpVertEuler.y = Mathf.Clamp(_tmpVertEuler.y, _rangeVert.x, _rangeVert.y);
        _tmpHorEuler.x = Mathf.Clamp(_tmpHorEuler.x, _rangeHor.x, _rangeHor.y);
        _playerHandle.transform.localEulerAngles = _tmpVertEuler;
        _cameraHandle.transform.localEulerAngles = _tmpHorEuler;

    }
}
