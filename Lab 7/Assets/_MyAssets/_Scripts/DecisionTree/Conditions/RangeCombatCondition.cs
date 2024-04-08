using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCombatCondition : ConditionNode
{
    public bool IsWithCombatRange { get; set; }

    public RangeCombatCondition()
    {
        name = "Range Combat Condition";
        IsWithCombatRange = false;
    }

    public override bool Condition()
    {
        Debug.Log("Checking " + name);
        return IsWithCombatRange;
    }

}
