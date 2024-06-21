using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotateTest : MonoBehaviour
{
    [SerializeField] Transform _copyPos;


    //コピペであまり内容を理解していない
    [SerializeField] AxisState _horizontal;
    [SerializeField] AxisState _vertical;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //AxisStateを動かすにはUpdateを実行する必要がある
        _horizontal.Update(Time.deltaTime);
        _vertical.Update(Time.deltaTime);

        var horizontalRotation = Quaternion.AngleAxis(_horizontal.Value, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(_vertical.Value, Vector3.right);
        Quaternion hv = horizontalRotation * verticalRotation;
        transform.rotation = hv;
        this.transform.position = _copyPos.position;
    }
}
