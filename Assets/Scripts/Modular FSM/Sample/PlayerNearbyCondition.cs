using UnityEngine;

namespace ModularFSM.Sample
{
    [CreateAssetMenu(menuName = "FSM/Sample/Conditions/PlayerNearby")]
    public class PlayerNearbyCondition : StateCondition
    {
        public override bool Evaluate(StateController controller)
        {
            var enemy = (EnemyController)controller;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null) return false;

            float dist = Vector3.Distance(controller.transform.position, player.transform.position);
            if (dist <= enemy.detectionRange)
            {
                enemy.Target = player.transform;
                return true;
            }

            enemy.Target = null;
            return false;
        }
    }
}
