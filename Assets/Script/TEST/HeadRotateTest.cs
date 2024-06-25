using Cinemachine;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using System.Runtime.ConstrainedExecution;

public class HeadRotateTest : MonoBehaviour
{
    [SerializeField] Transform _copyPos;
    [SerializeField] CinemachineVirtualCamera _ADSCamera;
    EnemyAimAssistPosList _AimAssistPosList;

    [SerializeField] float _stickMulti = 0.3f;
    [SerializeField] float _ADSAimSpeed = 0.5f;
    [SerializeField] float _aimAssistSize = 10;

    bool _isTweening = false;
    Tween _bodyAlphaZero;

    [SerializeField, Range(0, 1)] float _aimAssistPower = 0.5f;

    [SerializeField] List<GameObject> _ADSAlphaZero = new List<GameObject>();

    //コピペであまり内容を理解していない
    [SerializeField] AxisState _horizontal;
    [SerializeField] AxisState _vertical;
    PlayerInput _playerInput;

    void Start()
    {
        _playerInput = _copyPos.gameObject.transform.parent.GetComponent<PlayerInput>();
        _AimAssistPosList = EnemyAimAssistPosList.Instance;
    }

    void Update()
    {

        //AxisStateを動かすにはUpdateを実行する必要がある
        _horizontal.Update(Time.deltaTime);
        _vertical.Update(Time.deltaTime);

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

        float x = 0;
        float y = 0;

        if (_AimAssistPosList.AAPL.Count > 0)
        {
            var a = _AimAssistPosList.AAPL.OrderBy((a) => (Vector3.Angle((a.transform.position -
                Camera.main.gameObject.transform.position)
                , Camera.main.transform.forward))).ToList();

            Vector3 enemyDire = (a[0].transform.position - Camera.main.gameObject.transform.position).normalized;
            Vector3 cross = Vector3.Cross(Camera.main.gameObject.transform.forward, enemyDire);


            if (Vector3.Angle(enemyDire, Camera.main.transform.forward) < _aimAssistSize)
            {
                if (_playerInput.InputLook.x != 0)
                {
                    x = Mathf.Abs(_playerInput.InputLook.x) * (cross.y < 0 ? -1 : 1) * _aimAssistPower;
                }
                if (_playerInput.InputLook.y != 0)
                {
                    y = Mathf.Abs(_playerInput.InputLook.y) * (Camera.main.transform.forward.y - enemyDire.y < 0 ? 1 : -1) * _aimAssistPower;
                }
            }
        }
        _horizontal.Value += (_playerInput.InputLook.x + x) * _stickMulti * _horizontal.m_MaxSpeed;
        _vertical.Value   += (_playerInput.InputLook.y + y) * -1 * _stickMulti * _vertical.m_MaxSpeed;

        var horizontalRotation = Quaternion.AngleAxis(_horizontal.Value, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(_vertical.Value, Vector3.right);
        Quaternion hv = horizontalRotation * verticalRotation;
        transform.rotation = hv;
    }
}
