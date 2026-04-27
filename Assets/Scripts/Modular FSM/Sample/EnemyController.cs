using UnityEngine;

namespace ModularFSM.Sample
{
    public class EnemyController : StateController
    {
        public float detectionRange = 10f;
        public float attackRange = 2f;
        public float moveSpeed = 3.5f;
        public float attackDamage = 10f;
        public float attackCooldown = 1f;

        public Transform Target { get; set; }
        public float NextAttackTime { get; set; }
    }
}
