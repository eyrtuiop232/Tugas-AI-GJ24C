using UnityEngine;

namespace ModularFSM
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] State initialState;

        public State CurrentState { get; private set; }

        void Start()
        {
            TransitionToState(initialState);
        }

        void Update()
        {
            CurrentState?.OnUpdate(this);
        }

        public void TransitionToState(State next)
        {
            if (next == null) return;

            CurrentState?.OnExit(this);
            CurrentState = next;
            CurrentState.OnEnter(this);
        }
    }
}
