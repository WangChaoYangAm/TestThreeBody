using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInput : MonoBehaviour
{
    public string _keyUp = "w";
    public string _keyDownp = "s";
    public string _keyLeft = "a";
    public string _keyRight = "d";
    public string _keyRun = "left shift";
    public string _keyJump = "space";

    public string _keyJRight;
    public string _keyJLeft;
    public string _keyJUp;
    public string _keyJDown;

    public float Dup;
    public float Dright;
    public float Dmag;//归一化向量长度？
    public Vector3 Dvect;

    //摄像机的旋转
    public float Jup;
    public float Jright;

    private float _targetDup;
    private float _targetDright;
    private float _velocityDup;
    private float _velocityDright;
    [SerializeField]
    private float _valChange;//平滑幅度

    public bool _inputEnable = true;
    public bool _isRun;
    public bool _isJump = false;
    private bool _isLastJump = false;

    private bool _isHitItem;
    private Vector3 _screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);

    // Update is called once per frame
    void Update()
    {
        _targetDup = (Input.GetKey(_keyUp) ? 1f : 0) - (Input.GetKey(_keyDownp) ? 1f : 0);
        _targetDright = (Input.GetKey(_keyRight) ? 1f : 0) - (Input.GetKey(_keyLeft) ? 1f : 0);
        Jup = Input.GetAxis("Mouse X");
        Jright = Input.GetAxis("Mouse Y");
        if (!_inputEnable)
        {
            _targetDup = 0;
            _targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, _targetDup, ref _velocityDup, _valChange);
        Dright = Mathf.SmoothDamp(Dright, _targetDright, ref _velocityDright, _valChange);

        Vector2 tmpAxis = SquareToCircle(new Vector2(Dright, Dup));
        //Vector2 tmpAxis = new Vector2(Dright, Dup);
        float Dright2 = tmpAxis.x;
        float Dup2 = tmpAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvect = Dright2 * transform.right + Dup2 * transform.forward;

        _isRun = Input.GetKey(_keyRun);
        bool isNewJump = Input.GetKey(_keyJump);
        if (isNewJump != _isLastJump && isNewJump == true)
        {
            _isJump = true;
        }
        else
            _isJump = false;
        _isLastJump = isNewJump;

        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer(Layers.ITEM))
            {
                _isHitItem = true;
                if (Input.GetKeyUp(KeyCode.F))
                {
                    var trrigerItem = hit.collider.GetComponent<Trigger_Item>();
                    MyFacade.SendMsg(MyCommand.ADD_ITEM, trrigerItem.GetItemData);
                }
            }
            else
            {
                _isHitItem = false;
            }
        }
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
