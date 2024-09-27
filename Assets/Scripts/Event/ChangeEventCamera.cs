using System.Collections;
using UnityEngine;
using Cinemachine;


public class ChangeEventCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera eventCamera;
    [SerializeField] private GameObject eventCameraUI;
    [SerializeField] private InputReaderSO input;

    public void SetEventCamera(Transform target)
    {
        eventCamera.m_LookAt = target;
        eventCamera.m_Follow = target;
    }
    public void EnableCamera()
    {
        eventCamera.gameObject.SetActive(true);
        eventCameraUI.SetActive(true);
        StartCoroutine("WaitASecs");
    }
    private IEnumerator WaitASecs()
    {
        yield return new WaitForSeconds(1f);
        input.DisableAllInput();
    }
    public void DisableCamera()
    {
        eventCamera.gameObject.SetActive(false);      
        StartCoroutine("WaitASec");
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
        eventCameraUI.SetActive(false);
        input.SetGamePlayInput();
    }
}
