using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisions/RangeAttackDecision")]
public class RangeAttackDecision : IADecision
{
    public override bool Decide(IAController controller)
    {
       return IsCharacterInRange(controller);
    }

    private bool IsCharacterInRange(IAController controller)
    {
        if(controller.CharacterReference == null)
        {
            return false;
        }

        float distance = (controller.CharacterReference.position - controller.transform.position).sqrMagnitude;

        if(distance < Mathf.Pow(controller.RangeDetermined, 2))
        {
            return true;
        }

        return false;
    }
}
