using UnityEngine;

// Attach this to the WeaponModel child object (the one with BoxCollider2D trigger).
// It calls Attack() on the parent Character when another Character enters the trigger.
public class WeaponTrigger : MonoBehaviour
{
    private Character owner;

    private void Awake()
    {
        owner = GetComponentInParent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner == null) return;

        Character target = other.GetComponent<Character>();
        if (target == null || target == owner) return;

        // Only hit characters with a different tag
        if (target.CompareTag(owner.tag)) return;

        owner.Attack(target);
    }
}
