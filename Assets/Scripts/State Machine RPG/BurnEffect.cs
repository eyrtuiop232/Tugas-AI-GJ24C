using System.Collections;
using UnityEngine;

// Added to a Character when ignited by the Mage.
// Deals 1 damage per second until the target dies.
// Multiple instances can exist on the same target (stacking burns).
public class BurnEffect : MonoBehaviour
{
    private Character target;

    public void Init(Character target)
    {
        this.target = target;
        StartCoroutine(Burn());
    }

    private IEnumerator Burn()
    {
        while (target != null && target.IsAlive())
        {
            yield return new WaitForSeconds(1f);
            if (target != null && target.IsAlive())
                target.TakeDamage(1f);
        }
        Destroy(this);
    }
}
