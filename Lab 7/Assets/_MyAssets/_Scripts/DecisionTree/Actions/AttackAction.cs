using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Fill in for Lab 7a.
public class AttackAction : ActionNode
{
   public AttackAction()
    {
        name = "Attack Action";
    } 

    public override void Action()
    {
        if(Agent.GetComponent<AgentObject>().state != ActionState.ATTACK)
        {
            Debug.Log("Starship " + name);
            AgentObject ao = Agent.GetComponent<AgentObject>();
            ao.state = ActionState.ATTACK;

            if(AgentScript is CloseCombatEnemy cce)
            {

            }
            else if(AgentScript is RangeCombatEnemy rce)
            {

            }
        }
        // Every frame
        Debug.Log("Performing " + name);
    } 
}
