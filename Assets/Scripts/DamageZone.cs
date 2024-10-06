using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage();
            health.gameObject.GetComponent<PlayerVisual>().PlayVisual(damageType);
        }
    }
}
