using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    protected bool DDOL= false;
    private static T instance;
    public static T Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        if (DDOL)
        {
            DontDestroyOnLoad(instance);
        }
    }
}
