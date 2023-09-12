using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string _keyUp = "w";
    public string _keyDownp = "s";
    public string _keyLeft = "a";
    public string _keyRight = "d";

    public float Dup;
    public float Dright;
    public float Dmag;//归一化向量长度？
    public Vector3 Dvect;

    private float _targetDup;
    private float _targetDright;
    private float _velocityDup;
    private float _velocityDright;
    [SerializeField]
    private float _valChange;//平滑幅度

    public bool _inputEnable= true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _targetDup = (Input.GetKey(_keyUp) ? 1f : 0) - (Input.GetKey(_keyDownp) ? 1f : 0);
        _targetDright = (Input.GetKey(_keyRight) ? 1f : 0) - (Input.GetKey(_keyLeft) ? 1f : 0);
        if(!_inputEnable)
        {
            _targetDup = 0;
            _targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, _targetDup, ref _velocityDup, _valChange);
        Dright = Mathf.SmoothDamp(Dright,_targetDright, ref _velocityDright, _valChange);

        Dmag = Mathf.Sqrt(Dup * Dup) + (Dright * Dright);
        Dvect = Dright * transform.right + Dup * transform.forward;
    }
}
