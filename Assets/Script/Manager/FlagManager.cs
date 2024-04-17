using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : SingletonMonoBehaviour<FlagManager>
{
    bool isGoalFlag = false;
    public bool IsGoalFlag
    {
        get { return isGoalFlag; }
        set { isGoalFlag = value; }
    }
}
