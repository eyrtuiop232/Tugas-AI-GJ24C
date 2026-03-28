using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 3f;
    public float collisionDamage = 5f;
    public bool collisionDamageEnabled = true;
    public float knockbackForce = 5f;

    [Header("Weapon")]
    public Transform weapon;
    public string targetType = "Rotate"; // "Rotate" or "Follow"
    [SerializeField] protected float rotateSpeed = 90f;

    public float Health => health;
    public float Damage => damage;
    public float Speed => speed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start moving in a random diagonal direction
        float x = Random.value > 0.5f ? 1f : -1f;
        float y = Random.value > 0.5f ? 1f : -1f;
        moveDirection = new Vector2(x, y).normalized;
    }

    protected virtual void Update()
    {
        if (weapon == null) return;

        if (targetType == "Rotate")
        {
            weapon.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
        else if (targetType == "Follow")
        {
            Character nearest = FindNearestTarget();
            if (nearest != null)
            {
                Vector2 dir = nearest.transform.position - weapon.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                weapon.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    private Character FindNearestTarget()
    {
        Character[] all = FindObjectsByType<Character>(FindObjectsSortMode.None);
        Character nearest = null;
        float minDist = float.MaxValue;

        foreach (Character c in all)
        {
            if (c == this || c.CompareTag(tag)) continue;

            float dist = Vector2.Distance(transform.position, c.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = c;
            }
        }
        return nearest;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    // Moves the Rigidbody2D in the direction of the last collision reflection
    protected virtual void Move()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    // Changes movement speed — higher value = faster movement
    public void AdjustSpeed(float newSpeed)
    {
        speed = Mathf.Max(0f, newSpeed);
        Debug.Log($"{name} speed adjusted to {speed}");
    }

    // Reflect direction off the wall's collision normal (bounce)
    // Also deals collision damage and knockback when hitting another Character
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;

        if (collision.gameObject.TryGetComponent(out Character other))
        {
            rb.AddForce(normal * knockbackForce, ForceMode2D.Impulse);

            if (collisionDamageEnabled)
                other.TakeDamage(collisionDamage);
        }
    }

    public abstract void Attack(Character target);

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(0f, health);
        Debug.Log($"{name} took {amount} damage! HP remaining: {health}");

        if (health <= 0)
            Destroy(gameObject);
    }

    public bool IsAlive() => health > 0;
}
