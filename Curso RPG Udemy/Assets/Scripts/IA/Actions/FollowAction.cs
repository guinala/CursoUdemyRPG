using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Actions/FollowAction")]
public class FollowAction : IAAction
{
    public override void Execute(IAController controller)
    {
        Follow(controller); 
    }

    private void Follow(IAController controller)
    {
        if(controller.CharacterReference == null)
        {
            return;
        }

        if (controller.CharacterReference.GetComponent<CharacterHealth>().Defeated)
        {
            return;
        }

        Vector3 directionToCharacter = controller.CharacterReference.position - controller.transform.position;
        Vector3 direction = directionToCharacter.normalized;
        float distancia = directionToCharacter.magnitude;

        if(distancia >= 1.15f)
        {
            controller.transform.Translate(direction * controller.Speed * Time.deltaTime);
        }
    }
}

