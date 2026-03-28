using System.Collections;
using UnityEngine;

// Added to a Character when hit by the Mage's first hit.
// Boosts the target's move speed for a duration.
// A second Mage hit while active will call Cleanse() — knockback + restore speed immediately.
public class TelekinesisEffect : MonoBehaviour
{
    private Character target;
    private float originalSpeed;
    private Coroutine expireCoroutine;

    public void Init(Character target, float speedMultiplier, float duration)
    {
        this.target = target;
        originalSpeed = target.Speed;
        target.AdjustSpeed(originalSpeed * speedMultiplier);
        expireCoroutine = StartCoroutine(ExpireAfter(duration));
    }

    // Called by Mage when the target is hit a second time while this is active
    public void Cleanse()
    {
        if (expireCoroutine != null) StopCoroutine(expireCoroutine);
        RestoreSpeed();
        Destroy(this);
    }

    private IEnumerator ExpireAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        RestoreSpeed();
        Destroy(this);
    }

    private void RestoreSpeed()
    {
        if (target != null)
            target.AdjustSpeed(originalSpeed);
    }
}
