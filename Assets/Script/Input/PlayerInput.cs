using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : SingletonMonoBehaviour<PlayerInput>
{
    PlayerInputSystem _input;
    Vector2 _inputMove = new Vector2(0, 0);
    public Vector2 InputMove { get { return _inputMove; } }

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
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        Debug.Log(_inputMove);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
