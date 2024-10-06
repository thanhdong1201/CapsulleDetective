using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static event Action OnEndGame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEndGame?.Invoke();
            //StartCoroutine("WaitASec");
        }
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Outro");
    }
}
