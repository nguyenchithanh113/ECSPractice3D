using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textFPS;
    private float updateInterval = 0.5f;  // Update interval for displaying FPS

    private void Update()
    {

        float fps = 1.0f / Time.deltaTime;
        _textFPS.text = fps.ToString();
    }
}
