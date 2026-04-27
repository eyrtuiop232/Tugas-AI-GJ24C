using UnityEngine;

namespace ModularFSM
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void OnEnter(StateController controller);
        public abstract void OnUpdate(StateController controller);
        public abstract void OnExit(StateController controller);
    }
}
