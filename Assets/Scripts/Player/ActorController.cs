using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject _model;
    public PlayerInput _playerInput;
    public float _walkSpeed;
    public float _runSpeed = 2;
    public float _jumpVelocity = 2;
    public float _rollVelocity = 1;
    public float _thresholdRoll = 2;//ǰ�����ж��ٶ���ֵ
    [SerializeField]
    private Animator _aniPlayer;
    private Rigidbody _rigidbody;
    private Vector3 _vecPlanar = new Vector3(0, 0, 1f);//ƽ����ƶ�
    private Vector3 _vecThurst;//��Ծ�ĳ���
    private bool _isLockPlanar;

    private MySensorOnGround _sensorOnGround;
    //private MyFSMOnExit _fsmOnExit;
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _aniPlayer = _model.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _sensorOnGround = GetComponentInChildren<MySensorOnGround>();
        _sensorOnGround.Init(OnGround);
        //_fsmOnExit = _aniPlayer.GetComponent<MyFSMOnExit>();
        //_fsmOnExit.AddEvent(OnGroundEnter);
        _isLockPlanar = false;
        _playerInput._inputEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        _aniPlayer.SetFloat(MyConstDefine.PLAYER_FORWARD, _playerInput.Dmag * Mathf.Lerp(_aniPlayer.GetFloat(MyConstDefine.PLAYER_FORWARD), _playerInput._isRun ? _runSpeed : 1f, 0.5f));
        if (_rigidbody.velocity.magnitude > _thresholdRoll)
        {
            _aniPlayer.SetTrigger(MyConstDefine.PLAYER_ROLL);
        }

        if (_playerInput._isJump)
        {
            //Debug.Log("������");
            _aniPlayer.SetTrigger(MyConstDefine.PLAYER_JUMP);
        }
        if (_playerInput.Dmag > 0.1f)//��С�ڸ���ֵ˵��������
        {
            _model.transform.forward = Vector3.Slerp(_model.transform.forward, _playerInput.Dvect, 0.3f);
        }
        if (_isLockPlanar == false)
            _vecPlanar = _playerInput.Dmag * _model.transform.forward * _walkSpeed * (_playerInput._isRun ? _runSpeed : 1f);
    }

    private void FixedUpdate()
    {
        _rigidbody.position += _vecPlanar * Time.fixedDeltaTime;
        _rigidbody.velocity = new Vector3(_vecPlanar.x, _rigidbody.velocity.y, _vecPlanar.z) + _vecThurst;
        _vecThurst = Vector3.zero;//�����˳��������̹���
    }
    void OnJumpEnter()
    {
        Debug.Log("Jump");
        _vecThurst = new Vector3(0, _jumpVelocity, 0);
        _playerInput._inputEnable = false;
        _isLockPlanar = true;
    }
    void OnJumpExit()
    {
        Debug.Log("JumpDown");

    }
    void OnGround(bool isOnGround)
    {
        _aniPlayer.SetBool(MyConstDefine.PLAYER_ONGROUND, isOnGround);
    }
    void OnGroundEnter()
    {
        _playerInput._inputEnable = true;
        _isLockPlanar = false;
    }

    void OnFallEnter()
    {
        _playerInput._inputEnable = false;
        _isLockPlanar = true;
    }
    void OnRollEnter()
    {
        _vecThurst = new Vector3(0, _rollVelocity, 0);
        _playerInput._inputEnable = false;
        _isLockPlanar = true;
    }
}