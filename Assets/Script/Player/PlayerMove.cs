using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField]float _moveSpeed = 3f;
    PlayerInput _pi;
    Vector3 _moveVec;
    // Start is called before the first frame update
    void Start()
    {
        _pi = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pi.InputMove);
        //Vector3 inputVec3 = new Vector3(_pi.InputMove.x, 0, _pi.InputMove.y);
        _moveVec = (Camera.main.transform.forward * _pi.InputMove.y + Camera.main.transform.right * _pi.InputMove.x).normalized;
        _moveVec.y = 0;
        Debug.Log(_rb.velocity.magnitude);
        Vector3 pForward = _rb.velocity;
        pForward.y = 0;

        if (_rb.velocity.magnitude > 0.5f)
        {
            this.transform.forward = pForward;
        }

        //_rb.AddForce(cameraVec * _moveSpeed);
        //_rb.velocity = cameraVec * Time.deltaTime * _moveSpeed;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_moveVec * _moveSpeed);
    }
}
