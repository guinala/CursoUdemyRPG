using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/EnableMovementPath")]
public class EnableMovementPath : IAAction
{
    public override void Execute(IAController controller)
    {
        if(controller.EnemyMovement == null)
        {
            return;
        }

        controller.EnemyMovement.enabled = true;
    }
}

