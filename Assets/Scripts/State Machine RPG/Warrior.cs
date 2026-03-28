using UnityEngine;

public class Warrior : Character
{
    private float cooldown = 0.5f;
    private const float cooldownReduction = 0.05f;
    private const float minCooldown = 0.1f;
    private const float rotateSpeedIncrease = 120f;
    private const float knockbackIncrease = 1f;

    private float lastAttackTime = float.MinValue;

    protected override void Awake()
    {
        base.Awake();
        health = 140f;
        damage = 2f;
        targetType = "Rotate";
        rotateSpeed = 200f;
        collisionDamageEnabled = true;
        base.AdjustSpeed(8f);
    }

    public override void Attack(Character target)
    {
        if (Time.time < lastAttackTime + cooldown) return;

        lastAttackTime = Time.time;

        // On hit: shorten cooldown, hit harder
        cooldown = Mathf.Max(minCooldown, cooldown - cooldownReduction);
        knockbackForce += knockbackIncrease;

        // Invert spin direction, then increase magnitude so the sign change
        // never accidentally reduces the absolute speed
        rotateSpeed = -rotateSpeed;
        rotateSpeed += Mathf.Sign(rotateSpeed) * rotateSpeedIncrease;
        if (Mathf.Abs(rotateSpeed) > 550f)
            rotateSpeed = Mathf.Sign(rotateSpeed) * 550f;

        // Deal damage
        target.TakeDamage(damage);
        damage += 2f;

        // Knockback target away from warrior
        if (target.TryGetComponent(out Rigidbody2D targetRb))
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            targetRb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }

        Debug.Log($"[Warrior] Hit {target.name}! Cooldown:{cooldown:F2}s RotateSpeed:{rotateSpeed} Knockback:{knockbackForce}");
    }
}
