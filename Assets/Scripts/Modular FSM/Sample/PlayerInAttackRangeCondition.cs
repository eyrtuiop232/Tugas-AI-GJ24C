using UnityEngine;

namespace ModularFSM.Sample
{
    [CreateAssetMenu(menuName = "FSM/Sample/Conditions/PlayerInAttackRange")]
    public class PlayerInAttackRangeCondition : StateCondition
    {
        public override bool Evaluate(StateController controller)
        {
            var enemy = (EnemyController)controller;
            if (enemy.Target == null) return false;

            return Vector3.Distance(controller.transform.position, enemy.Target.position) <= enemy.attackRange;
        }
    }
}
