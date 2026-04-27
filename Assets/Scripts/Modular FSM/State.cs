using UnityEngine;

namespace ModularFSM
{
    [CreateAssetMenu(menuName = "FSM/State", fileName = "New State")]
    public class State : ScriptableObject
    {
        public StateAction[] actions;
        public StateTransition[] transitions;

        public void OnEnter(StateController controller)
        {
            foreach (var action in actions)
                action.OnEnter(controller);
        }

        public void OnUpdate(StateController controller)
        {
            foreach (var action in actions)
                action.OnUpdate(controller);

            CheckTransitions(controller);
        }

        public void OnExit(StateController controller)
        {
            foreach (var action in actions)
                action.OnExit(controller);
        }

        void CheckTransitions(StateController controller)
        {
            foreach (var t in transitions)
            {
                if (t.condition == null) continue;

                State next = t.condition.Evaluate(controller) ? t.trueState : t.falseState;

                if (next != null && next != controller.CurrentState)
                {
                    controller.TransitionToState(next);
                    return;
                }
            }
        }
    }
}
