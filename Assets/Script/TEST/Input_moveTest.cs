using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_moveTest : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField]
    float _moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDirec = Camera.main.transform.forward * v + Camera.main.transform.right * h;
        _rb.AddForce(moveDirec * _moveSpeed);
    }
}
