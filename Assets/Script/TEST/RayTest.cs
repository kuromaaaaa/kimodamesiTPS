using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayTest : MonoBehaviour
{
    [SerializeField]
    LayerMask _hitRayLayer;
    [SerializeField]
    Material _hitMaterial;
    RaycastHit _rayhit;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out _rayhit, 30f, _hitRayLayer))
        {
            Debug.Log(_rayhit.collider.gameObject.name);
            if(Input.GetMouseButtonDown(0))
            {
                _rayhit.collider.gameObject.GetComponent<MeshRenderer>().material = _hitMaterial;
            }
        }
        Debug.DrawLine(transform.position, this.transform.position + Camera.main.transform.forward * 30, new Color(0,0,1.0f));
    }
}
