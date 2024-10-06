using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private GameObject attackEffect;

    [SerializeField] private Material material;
    [SerializeField] private MeshRenderer meshRenderer;
    private Material defaultMaterial;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        defaultMaterial = meshRenderer.material;
    }
    public void DetectTarget()
    {
        meshRenderer.material = material;
        audioSource.PlayOneShot(clip);
    }
    public void NoTarget()
    {
        meshRenderer.material = defaultMaterial;
    }
    public void EnterAttack()
    {
        attackEffect?.SetActive(true);
        audioSource.PlayOneShot(clip);
    }
    public void ExitAttack()
    {
        attackEffect?.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage();
            health.gameObject.GetComponent<PlayerVisual>().PlayVisual(damageType);
        }
    }
}
