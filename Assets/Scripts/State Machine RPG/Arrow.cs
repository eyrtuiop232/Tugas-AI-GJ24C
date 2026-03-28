using UnityEngine;

// Attach to the arrow prefab. Requires a BoxCollider2D set to Is Trigger.
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float damage;
    private string ownerTag;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Called by Archer when spawning this arrow
    public void Init(float damage, string ownerTag)
    {
        this.damage = damage;
        this.ownerTag = ownerTag;
    }

    private void Update()
    {
        // Move forward along the arrow's local right axis (matches weapon rotation)
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hit a character on a different team
        if (other.TryGetComponent(out Character target))
        {
            if (!target.CompareTag(ownerTag))
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
            return;
        }

        // Hit a wall (non-trigger collider)
        if (!other.isTrigger)
            Destroy(gameObject);
    }
}
