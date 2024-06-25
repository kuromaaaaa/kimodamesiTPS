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
    RaycastHit _rayhitCollider;
    bool _rayHit = false;

    Vector3 _rayHitPos;
    public Vector3 RayHitPos { get { return _rayHitPos; } }

    [SerializeField]
    GameObject _tamaPrefub;

    PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        //カーソルを非表示、中央に固定化
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _cm = Camera.main.gameObject;
    }

    private void OnEnable()
    {
        _playerInput = this.GetComponent<PlayerInput>();
        _playerInput.OneShot += Fire;
    }

    private void OnDisable()
    {
        _playerInput.OneShot -= Fire;
    }

    // Update is called once per frame
    void Update()
    {
        _rayHitPos = _cm.transform.position + (_cm.transform.forward * _rayDistance);
        _rayHit = false;

        if (Physics.Raycast(_cm.transform.position, _cm.transform.forward, out _rayhitCollider, _rayDistance, _hitRayLayer))
        {
            _rayHitPos = _rayhitCollider.point;
            _rayHit = true;
        }
        Debug.DrawRay(_cm.transform.position, _cm.transform.forward * _rayDistance, new Color(0, 0, 1.0f));
    }

    void Fire()
    {
        if (_rayHit && _rayhitCollider.collider.gameObject.TryGetComponent<PartsCollider>(out PartsCollider pc))
        {
            pc.damage();
        }
    }
}
