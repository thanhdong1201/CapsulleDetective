using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action OnPlayerDie;
    [SerializeField] private int health = 1;
    public bool IsPlayerDead { get; private set; }
    private void Start()
    {
        IsPlayerDead = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            OnPlayerDie?.Invoke();
        }
    }
    public void TakeDamage()
    {
        if (IsPlayerDead) return;

        health -= 1;
        if (health <= 0)
        {
            IsPlayerDead = true;
            OnPlayerDie?.Invoke();
        }
    }
}
