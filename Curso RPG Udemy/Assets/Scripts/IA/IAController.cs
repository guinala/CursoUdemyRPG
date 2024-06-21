using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    [Header("IA States")]
    [SerializeField] private IAState initialState;
    [SerializeField] private IAState defaultState;

    public IAState currentState { get; set; }


    private void Start()
    {
        currentState = initialState;
    }

    private void Update()
    {
        currentState.ExecuteState(this);
    }

    public void changeState(IAState state)
    {
        if(state != defaultState)
        {
            currentState = state;
        }
        else
        {
            currentState = defaultState;
        }
    }
}
