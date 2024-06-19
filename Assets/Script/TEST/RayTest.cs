using UnityEngine;

public class RayTest : MonoBehaviour
{
    GameObject _cm;
    [SerializeField]
    float _rayDistance = 100f;

    [SerializeField]
    LayerMask _hitRayLayer;
    [SerializeField]
    Material _hitMaterial;
    RaycastHit _rayhit;

    Vector3 _rayHitPos;
    public Vector3 RayHitPos { get { return _rayHitPos; } }

    [SerializeField]
    GameObject _tamaPrefub;
    // Start is called before the first frame update
    void Start()
    {
        //カーソルを非表示、中央に固定化
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _cm = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _rayHitPos = _cm.transform.position + (_cm.transform.forward * _rayDistance);

        if (Physics.Raycast(_cm.transform.position, _cm.transform.forward, out _rayhit, _rayDistance, _hitRayLayer))
        {
            Debug.Log(_rayhit.collider.gameObject.name);
            _rayHitPos = _rayhit.point;
            if (Input.GetMouseButtonDown(0))
            {
                _rayhit.collider.gameObject.GetComponent<MeshRenderer>().material = _hitMaterial;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (_tamaPrefub)
            {
                GameObject tama = Instantiate(_tamaPrefub);
                tama.transform.position = this.transform.position;
                tama.GetComponent<Rigidbody>().AddForce((_rayHitPos - transform.position).normalized * 100, ForceMode.Impulse);
                Destroy(tama, 3f);
            }
        }

        Debug.DrawLine(_cm.transform.position, _cm.transform.forward * _rayDistance, new Color(0, 0, 1.0f));


        /*
         * FPSの時のやつ
         * if(Physics.Raycast(transform.position, Camera.main.transform.forward, out _rayhit, 30f, _hitRayLayer))
         * {
         *     Debug.Log(_rayhit.collider.gameObject.name);
         *     if(Input.GetMouseButtonDown(0))
         *     {
         *         _rayhit.collider.gameObject.GetComponent<MeshRenderer>().material = _hitMaterial;
         *     }
         * }
         * Debug.DrawLine(transform.position, this.transform.position + Camera.main.transform.forward * 30, new Color(0,0,1.0f));
        */
    }
}
