using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public enum DamageType
{
    Fire,
    Electric
}
public class PlayerVisual : MonoBehaviour
{
    //Fire
    [SerializeField] private GameObject fireEffect;

    //Electric
    [SerializeField] private GameObject electricEffect;

    [SerializeField] private Material dieMaterial;
    [SerializeField] private MeshRenderer[] meshRenderers;
    private Material defaultMaterial;
    [SerializeField] private AnimationClip takeDamageClip;
    [SerializeField] private Animator animator;
    private void Start()
    {
        Health.OnPlayerDie += PlayVisual;
    }
    private void PlayVisual()
    {
        fireEffect.SetActive(true);
        animator.Play(takeDamageClip.name);
        StartCoroutine("WaitASec");

    }
    public void PlayVisual(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Fire:
                fireEffect.SetActive(true);
                electricEffect.SetActive(false);
                animator.Play(takeDamageClip.name);
                StartCoroutine("WaitASec");
                break;
            case DamageType.Electric:
                fireEffect.SetActive(false);
                electricEffect.SetActive(true);
                animator.Play(takeDamageClip.name);
                StartCoroutine("WaitASec2");
                break;
        }
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(takeDamageClip.length);
        fireEffect.SetActive(false);
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = dieMaterial;
        }
        Health.OnPlayerDie -= PlayVisual;
    }
    private IEnumerator WaitASec2()
    {
        yield return new WaitForSeconds(takeDamageClip.length);
        electricEffect.SetActive(false);
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = dieMaterial;
        }
        Health.OnPlayerDie -= PlayVisual;
    }
}
