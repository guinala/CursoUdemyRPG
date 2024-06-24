using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/DisableMovementPath")]
public class DisableMovementPath : IAAction
{
    public override void Execute(IAController controller)
    {
        if (controller.EnemyMovement == null)
        {
            return;
        }

        controller.EnemyMovement.enabled = false;
    }
}
