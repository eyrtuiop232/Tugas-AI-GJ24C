using UnityEngine;

namespace ModularFSM.Sample
{
    [CreateAssetMenu(menuName = "FSM/Sample/Actions/Idle")]
    public class IdleAction : StateAction
    {
        public override void OnEnter(StateController controller)
        {
            Debug.Log($"{controller.name}: Idle");
        }

        public override void OnUpdate(StateController controller) { }

        public override void OnExit(StateController controller) { }
    }
}
