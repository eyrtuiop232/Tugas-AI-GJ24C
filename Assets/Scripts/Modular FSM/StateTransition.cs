using System;
using UnityEngine;

namespace ModularFSM
{
    [Serializable]
    public class StateTransition
    {
        public StateCondition condition;
        [Tooltip("State to enter when condition is true. Leave null to remain in current state.")]
        public State trueState;
        [Tooltip("State to enter when condition is false. Leave null to remain in current state.")]
        public State falseState;
    }
}
