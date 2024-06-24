using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IATransition
{
    public IADecision decision;
    public IAState trueState;
    public IAState falseState;
}
