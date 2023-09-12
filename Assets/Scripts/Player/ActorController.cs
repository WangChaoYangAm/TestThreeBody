using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject _model;
    public PlayerInput _playerInput;
    public float _walkSpeed;
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
        _aniPlayer.SetFloat(MyConstDefine.PLAYER_FORWARD, _playerInput.Dmag);
        if (_playerInput.Dmag > 0.1f)//若小于该阈值说明松手了
            _model.transform.forward = _playerInput.Dvect;
        _movingVect = _playerInput.Dmag * _model.transform.forward * _walkSpeed;
    }

    private void FixedUpdate()
    {
        //_rigidbody.position += _movingVect * Time.fixedDeltaTime;
        _rigidbody.velocity = _movingVect;
    }
}
