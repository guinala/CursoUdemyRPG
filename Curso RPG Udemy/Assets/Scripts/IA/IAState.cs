using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/IAState")]
public class IAState : ScriptableObject
{
    public IAAction[] actions;
    public IATransition[] transitions;


    public void ExecuteState(IAController controller)
    {
        ExecuteActions(controller);
        ExecuteTransitions(controller);
    }

    private void ExecuteActions(IAController controller)
    {
        if(actions == null || actions.Length <= 0) return;

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Execute(controller);
        }
    }

    private void ExecuteTransitions(IAController controller)
    {
        if(transitions == null || transitions.Length <= 0) return;

        for (int i = 0; i < transitions.Length; i++)
        {
            bool decision = transitions[i].decision.Decide(controller);
            if(decision)
            {
                controller.changeState(transitions[i].trueState);
            }
            else
            {
                controller.changeState(transitions[i].falseState);
            }
        }
    }
}
