using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimAssistPosList : SingletonMonoBehaviour<EnemyAimAssistPosList>
{
    List<GameObject> _aapl = new List<GameObject>();
    public List<GameObject> AAPL
    {
        get { return _aapl; }
        set { _aapl = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
