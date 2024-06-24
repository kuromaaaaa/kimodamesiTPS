using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : SingletonMonoBehaviour<PlayerInput>
{
    PlayerInputSystem _input;
    Vector2 _inputMove = new Vector2(0, 0);
    public Vector2 InputMove { get { return _inputMove; } }
    Vector2 _inputLook = new Vector2(0, 0);
    public Vector2 InputLook { get { return _inputLook; } }
    bool _ADS;
    public bool ADS => _ADS;
    bool _fire;
    public bool Fire => _fire;

    public delegate void OneShotFire();
    public OneShotFire OneShot;

    private void Awake()
    {
        _input = new PlayerInputSystem();
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    void Start()
    {
        _input.Player.Move.started += OnMove;
        _input.Player.Move.performed += OnMove;
        _input.Player.Move.canceled += OnMove;
        _input.Player.ADS.started += OnADS;
        _input.Player.ADS.canceled += OnADS;
        _input.Player.Fire.started += OnFire;
        _input.Player.Fire.canceled += OnFire;
        _input.Player.Look.started += OnLook;
        _input.Player.Look.performed += OnLook;
        _input.Player.Look.canceled += OnLook;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _inputLook = context.ReadValue<Vector2>();
    }

    private void OnADS(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() > 0) _ADS = true;
        else _ADS = false;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0) 
        {
            _fire = true;
            OneShot();
        }
        else _fire = false;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
