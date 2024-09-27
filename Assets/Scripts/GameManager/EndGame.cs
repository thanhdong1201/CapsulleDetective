using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.UIManager.EndGame();
            StartCoroutine("WaitASec");
        }
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Outro");
    }
}
