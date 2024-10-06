using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private void Start()
    {
        Health.OnPlayerDie += ShowGameOverUI;
    }
    private void ShowGameOverUI()
    {
        StartCoroutine("WaitASec");
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(3.5f);
        gameOverUI.SetActive(true);
        Health.OnPlayerDie -= ShowGameOverUI;
    }
}
