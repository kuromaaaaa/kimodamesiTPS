using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] int _hp;
    int propatyHp
    {
        set
        {
            _hp = value;
            if (_hp <= 0) Destroy(this.gameObject);
        }
        get { return _hp; }
    }
    [SerializeField] GameObject _aimAssistPos;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAimAssistPosList.Instance.AAPL.Add(_aimAssistPos);
    }

    private void OnDisable()
    {
        if(EnemyAimAssistPosList.Instance)
            EnemyAimAssistPosList.Instance.AAPL.Remove(_aimAssistPos);
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void Damage(int damage)
    {
        propatyHp -= damage;
        Debug.Log(_hp);
    }
}
