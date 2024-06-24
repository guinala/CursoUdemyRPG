using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/AttackCharacterAction")]
public class AttackCharacterAction : IAAction
{
    public override void Execute(IAController controller)
    {
        Attack(controller);
    }

    private void Attack(IAController controller)
    {
        if(controller.CharacterReference == null)
        {
            return;
        }

        if(controller.IsTimeToAttack() == false)
        {
            return;
        }

        if(controller.IsCharacterInRange(controller.RangeDetermined))
        {
            if(controller.AttackType == AttackTypes.Assault)
            {
                controller.Assault(controller.Damage);
            }
            else
            {
                controller.MeleeAttack(controller.Damage);
            }
            controller.UpdateAttackTimeBetweenAttacks();
        }
    }
}

