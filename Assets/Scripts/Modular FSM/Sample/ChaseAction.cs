using UnityEngine;

namespace ModularFSM.Sample
{
    [CreateAssetMenu(menuName = "FSM/Sample/Actions/Chase")]
    public class ChaseAction : StateAction
    {
        public override void OnEnter(StateController controller)
        {
            Debug.Log($"{controller.name}: Chasing");
        }

        public override void OnUpdate(StateController controller)
        {
            var enemy = (EnemyController)controller;
            if (enemy.Target == null) return;

            Vector2 direction = ((Vector2)enemy.Target.position - (Vector2)controller.transform.position).normalized;
            controller.transform.position += (Vector3)(direction * (enemy.moveSpeed * Time.deltaTime));

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public override void OnExit(StateController controller) { }
    }
}
