using UnityEngine;

namespace ModularFSM.Sample
{
    [CreateAssetMenu(menuName = "FSM/Sample/Actions/Attack")]
    public class AttackAction : StateAction
    {
        public override void OnEnter(StateController controller)
        {
            var enemy = (EnemyController)controller;
            enemy.NextAttackTime = 0f;
            Debug.Log($"{controller.name}: Attacking");
        }

        public override void OnUpdate(StateController controller)
        {
            var enemy = (EnemyController)controller;
            if (enemy.Target == null) return;

            Vector2 direction = ((Vector2)enemy.Target.position - (Vector2)controller.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Time.time >= enemy.NextAttackTime)
            {
                enemy.NextAttackTime = Time.time + enemy.attackCooldown;
                Debug.Log($"{controller.name} hits {enemy.Target.name} for {enemy.attackDamage} damage!");
            }
        }

        public override void OnExit(StateController controller) { }
    }
}
