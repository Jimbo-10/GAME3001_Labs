using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRangeAction : ActionNode
{
    public MoveToRangeAction()
    {
        name = "Mve to range action";
    }

    public override void Action()
    {
        if (Agent.GetComponent<AgentObject>().state != ActionState.MOVE_TO_RANGE)
        {
            Debug.Log("Starting " + name);
            AgentObject ao = Agent.GetComponent<AgentObject>();
            ao.state = ActionState.MOVE_TO_RANGE;

            if (AgentScript is RangeCombatEnemy rce)
            {

            }
        }

        Debug.Log("Performing " + name);
    }
}
