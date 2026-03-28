using System.Collections;
using UnityEngine;

public class Mage : Character
{
    [Header("Mage")]
    [SerializeField] private Vector2 arenaMin = new Vector2(-8f, -4.5f);
    [SerializeField] private Vector2 arenaMax = new Vector2(8f, 4.5f);

    private const float telekinesisSpeedMultiplier = 4f;
    private const float telekinesisExtraDamageMultiplier = 2f;
    private const float telekinesisDuration = 6f;
    private const float teleportInterval = 10f;
    private const float cooldown = 0.5f;

    private float lastAttackTime = float.MinValue;

    protected override void Awake()
    {
        base.Awake();
        health = 80f;
        damage = 5f;
        targetType = "Rotate";
        rotateSpeed = 150f;
        collisionDamageEnabled = true;
        base.AdjustSpeed(4f);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(teleportInterval);
            Teleport();
        }
    }

    private void Teleport()
    {
        float x = Random.Range(arenaMin.x, arenaMax.x);
        float y = Random.Range(arenaMin.y, arenaMax.y);
        transform.position = new Vector3(x, y, transform.position.z);
        Debug.Log($"[Mage] Teleported to ({x:F1}, {y:F1})");
    }

    public override void Attack(Character target)
    {
        if (Time.time < lastAttackTime + cooldown) return;
        lastAttackTime = Time.time;

        // Effect 1: Telekinesis
        TelekinesisEffect tk = target.GetComponent<TelekinesisEffect>();
        if (tk != null)
        {
            // Already under telekinesis: deal bonus damage, knockback, then cleanse
            target.TakeDamage(damage * telekinesisExtraDamageMultiplier);
            if (target.TryGetComponent(out Rigidbody2D targetRb))
            {
                Vector2 dir = (target.transform.position - transform.position).normalized;
                targetRb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
            }
            tk.Cleanse();
            Debug.Log($"[Mage] Telekinesis cleansed on {target.name}! Dealt {damage * telekinesisExtraDamageMultiplier} damage.");
        }
        else
        {
            // Apply telekinesis — target moves very fast for 6s
            target.TakeDamage(damage);
            TelekinesisEffect newTk = target.gameObject.AddComponent<TelekinesisEffect>();
            newTk.Init(target, telekinesisSpeedMultiplier, telekinesisDuration);
            Debug.Log($"[Mage] Telekinesis applied to {target.name}!");
        }

        // Effect 3: Ignite — add a new independent burn stack
        BurnEffect burn = target.gameObject.AddComponent<BurnEffect>();
        burn.Init(target);
    }
}
