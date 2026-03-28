using UnityEngine;

public class Archer : Character
{
    [Header("Archer")]
    public GameObject arrowPrefab;
    [SerializeField] private float fireInterval = 3f;
    private const float fireIntervalReduction = 0.2f;
    private const float minFireInterval = 0.5f;

    private float critMultiplier = 1.0f;
    private const float critChance = 0.2f;
    private const float critMultiplierIncrease = 0.5f;

    private float fireTimer;

    protected override void Awake()
    {
        base.Awake();
        health = 100f;
        damage = 6f;
        targetType = "Follow";
        collisionDamageEnabled = false;
    }

    protected override void Update()
    {
        base.Update(); // handles weapon rotation/follow

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            fireInterval = Mathf.Max(minFireInterval, fireInterval - fireIntervalReduction);
            Attack(null);
        }
    }

    public override void Attack(Character target)
    {
        if (weapon == null || arrowPrefab == null) return;

        bool isCrit = Random.value < critChance;
        float arrowDamage = damage;

        if (isCrit)
        {
            arrowDamage = damage * critMultiplier;
            critMultiplier += critMultiplierIncrease;
            Debug.Log($"[Archer] CRIT! Damage={arrowDamage}, next crit multiplier={critMultiplier}x");
        }

        // Spawn arrow at weapon position facing weapon's forward direction
        GameObject arrow = Instantiate(arrowPrefab, weapon.position, weapon.rotation);

        if (arrow.TryGetComponent(out Arrow arrowScript))
            arrowScript.Init(arrowDamage, tag);
    }
}
