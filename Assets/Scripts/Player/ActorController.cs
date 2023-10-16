using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject _model;
    public PlayerInput _playerInput;
    public float _walkSpeed;
    public float _runSpeed = 2;
    [SerializeField]
    private Animator _aniPlayer;
    private Rigidbody _rigidbody;
    private Vector3 _movingVect = new Vector3(0, 0, 1f);
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _aniPlayer = _model.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _aniPlayer.SetFloat(MyConstDefine.PLAYER_FORWARD, _playerInput.Dmag * Mathf.Lerp(_aniPlayer.GetFloat(MyConstDefine.PLAYER_FORWARD), _playerInput._isRun ? _runSpeed : 1f, 0.5f));

        if (_playerInput.Dmag > 0.1f)//若小于该阈值说明松手了
        {
            _model.transform.forward = Vector3.Slerp(_model.transform.forward, _playerInput.Dvect, 0.3f);
        }

        _movingVect = _playerInput.Dmag * _model.transform.forward * _walkSpeed * (_playerInput._isRun ? _runSpeed : 1f);
    }

    private void FixedUpdate()
    {
        //_rigidbody.position += _movingVect * Time.fixedDeltaTime;
        //_rigidbody.velocity = new Vector3(_movingVect.x, _rigidbody.velocity.y, _movingVect.z);

    }
}
