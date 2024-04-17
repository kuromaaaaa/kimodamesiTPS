using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FlagManager.Instance.IsGoalFlag = true;
            Debug.Log("Goal" + FlagManager.Instance.IsGoalFlag);
        }
    }
}
