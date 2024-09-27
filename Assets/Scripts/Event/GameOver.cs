using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private GameObject playerDieVisual;
    [SerializeField] private GameObject playerVisual;

    private bool alreadyDie = false;
    public void PlayDieVisual()
    {
        if (!alreadyDie)
        {
            alreadyDie = true;
            playerVisual.SetActive(false);

            GameObject g = Instantiate(playerDieVisual, transform);
            input.SetUIInput();
        }

    }
}
