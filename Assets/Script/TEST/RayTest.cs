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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out _rayhit, 100f, _hitRayLayer))
        {
            Debug.Log(_rayhit.collider.gameObject.name);
            if(Input.GetMouseButtonDown(0))
            {
                _rayhit.collider.gameObject.GetComponent<MeshRenderer>().material = _hitMaterial;
            }
        }

    }
}
