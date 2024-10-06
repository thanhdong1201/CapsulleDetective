using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private GameObject startCamera;
    private float time = 1.0f;
    private void Start()
    {
        input.DisableAllInput(); 
        startCamera.SetActive(true);
        StartCoroutine("WaitToStart");
    }
    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(time);
        startCamera.SetActive(false);
        yield return new WaitForSeconds(time);
        input.SetGamePlayInput();
    }
}
