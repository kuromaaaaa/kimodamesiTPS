using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsCollider : MonoBehaviour
{
    EnemyData _parentEnemyData;
    [SerializeField] int _damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        _parentEnemyData = transform.root.GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void damage()
    {
        _parentEnemyData.Damage(_damage);
    }
}
