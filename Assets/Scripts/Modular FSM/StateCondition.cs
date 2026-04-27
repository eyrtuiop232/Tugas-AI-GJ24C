using UnityEngine;

namespace ModularFSM
{
    public abstract class StateCondition : ScriptableObject
    {
        public abstract bool Evaluate(StateController controller);
    }
}
