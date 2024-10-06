using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private GameObject attackEffect;
    private Health targetHealth;

    public void EnterAttack()
    {     
        attackEffect?.SetActive(true);
    }
    public void ExitAttack()
    {
        attackEffect?.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            targetHealth = health;
            health.TakeDamage();
        }
    }
}
