using UnityEngine;
using DG.Tweening;
using System;
public class PlayerMove : MonoBehaviour
{
    Rigidbody _rb;
    PlayerInput _pi;
    Animator _anim;
    GameObject _cm;

    [SerializeField] float _playerLockDis = 10;
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _moveSpeedMax = 5;
    Vector3 _moveVec;

    bool _playerForwardChange;


    float _easingInputH;
    DG.Tweening.Core.TweenerCore<float,float,DG.Tweening.Plugins.Options.FloatOptions> DtEasingH;
    float _inputH;
    public float InputH
    {
        set 
        {
            if (value != _inputH)
            {
                _inputH = value;
                DtEasingH = DOTween.To
                    (
                    () => _easingInputH,
                    (x) => _easingInputH = x,
                    value,
                    1
                    ) ;
            }
        }
    }
    
    float _easingInputV;
    float _inputV;
    DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> DtEasingV;
    public float InputV
    {
        set
        {
            if (value != _inputV)
            {
                _inputV = value;
                DtEasingV = DOTween.To
                    (
                    () => _easingInputV,
                    (x) => _easingInputV = x,
                    value,
                    1
                    );
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float a = 10f;
        var b = a;
        _pi = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _cm = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pi.InputMove);
        //Vector3 inputVec3 = new Vector3(_pi.InputMove.x, 0, _pi.InputMove.y);
        _moveVec = (Camera.main.transform.forward * _pi.InputMove.y + Camera.main.transform.right * _pi.InputMove.x).normalized;
        if ((_pi.InputMove.x != 0 || _pi.InputMove.y != 0) && _playerForwardChange)
        {
            Vector3 cmfo = _cm.transform.forward;
            cmfo.y = -0.1f;
            this.transform.forward = cmfo;
            _playerForwardChange = false;
        }
        else
        {
            _playerForwardChange= true;
        }
        _moveVec.y = 0;
        /*
        Vector3 pForward = _rb.velocity;
        pForward.y = 0;
        
        if (_rb.velocity.magnitude > 0.5f)
        {
            this.transform.forward = pForward;
        }*/

        Debug.Log(_pi.InputMove);

        InputH = _pi.InputMove.x;
        InputV = _pi.InputMove.y;
        _anim.SetFloat("speedX", _easingInputH);
        _anim.SetFloat("speedY", _easingInputV);
        _anim.SetFloat("moveMag", _rb.velocity.magnitude);

        //_rb.AddForce(cameraVec * _moveSpeed);
        //_rb.velocity = cameraVec * Time.deltaTime * _moveSpeed;
        
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_moveVec * _moveSpeed);
        if(_rb.velocity.magnitude > _moveSpeedMax)
        {
            Debug.Log("はやすぎ！！");
        }
    }
}
