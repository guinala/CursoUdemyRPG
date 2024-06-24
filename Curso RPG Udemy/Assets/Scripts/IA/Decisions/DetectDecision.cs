using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisions/DetectDecision")]
public class DetectDecision : IADecision
{
    public override bool Decide(IAController controller)
    {
        return Detect(controller);
    }

    private bool Detect (IAController controller)
    {
        Collider2D characterDetected = Physics2D.OverlapCircle(controller.transform.position,
            controller.DetectionRange, controller.CharacterLayerMask);

        if (characterDetected != null)
        {
            controller.CharacterReference = characterDetected.transform;
            Debug.Log("Character Detected");
            return true;
        }

        Debug.Log("Character Not Detected");
        controller.CharacterReference = null;
        return false;
    }
}

