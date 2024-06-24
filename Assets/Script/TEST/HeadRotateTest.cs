using Cinemachine;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;

public class HeadRotateTest : MonoBehaviour
{
    [SerializeField] Transform _copyPos;
    [SerializeField] CinemachineVirtualCamera _ADSCamera;

    [SerializeField] float _stickMulti = 0.3f;
    [SerializeField] float _ADSAimSpeed = 0.5f;

    [SerializeField] GameObject _enemy;
    bool _isTweening = false;
    Tween _bodyAlphaZero;

    [SerializeField] List<GameObject> _ADSAlphaZero = new List<GameObject>();

    //コピペであまり内容を理解していない
    [SerializeField] AxisState _horizontal;
    [SerializeField] AxisState _vertical;
    PlayerInput _playerInput;

    void Start()
    {
        _playerInput = _copyPos.gameObject.transform.parent.GetComponent<PlayerInput>();
    }

    void Update()
    {

        //AxisStateを動かすにはUpdateを実行する必要がある
        _horizontal.Update(Time.deltaTime);
        _vertical.Update(Time.deltaTime);

        var horizontalRotation = Quaternion.AngleAxis(_horizontal.Value, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(_vertical.Value, Vector3.right);
        Quaternion hv = horizontalRotation * verticalRotation;
        transform.rotation = hv;
        this.transform.position = _copyPos.position;
       


        if (_playerInput.ADS)
        {
            _ADSCamera.Priority = 20;
            _horizontal.m_MaxSpeed = _ADSAimSpeed;
            _vertical.m_MaxSpeed = _ADSAimSpeed;
            float timer = 0;
            if (!_isTweening)
            {
                _isTweening = true;
                _bodyAlphaZero = DOTween.To
                        (
                        () => timer,
                        (x) => timer = x,
                        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time,
                        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time
                        ).OnComplete(() => 
                        {
                            _isTweening = false;
                            foreach (var g in _ADSAlphaZero) g.gameObject.SetActive(false); 
                        });
            }
        }
        else
        {
            _ADSCamera.Priority = 0;
            _horizontal.m_MaxSpeed = 1;
            _vertical.m_MaxSpeed = 1;
            if(_isTweening)
            {
                _bodyAlphaZero.Kill();
                _isTweening = false;
            }
            foreach (var g in _ADSAlphaZero)
                { g.gameObject.SetActive(true); }
        }

        Vector3 enemyDire = ((_enemy.transform.position + new Vector3(0,0.5f,0)) - Camera.main.gameObject.transform.position).normalized;
        Vector3 cross = Vector3.Cross(Camera.main.gameObject.transform.forward, enemyDire);

        if (cross.y < 0)
        {
            if (cross.z < 0) Debug.Log("右下");
            else Debug.Log("右上");
        }
        else
        {
            if (cross.x < 0) Debug.Log("左下");
            else Debug.Log("左上");
        }

        _horizontal.Value += _playerInput.InputLook.x * _stickMulti * _horizontal.m_MaxSpeed;
        _vertical.Value += _playerInput.InputLook.y * -1 * _stickMulti * _vertical.m_MaxSpeed;
    }
}
