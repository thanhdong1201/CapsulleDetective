using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(UpdateFPS), 0f, 1f);
        Application.targetFrameRate = -1;
    }
    private void UpdateFPS()
    {
        float fps = 1/Time.deltaTime;
        fpsText.text = fps.ToString("F2");
    }
}
