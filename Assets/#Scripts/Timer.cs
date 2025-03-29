using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    public float currentTime;

    void Start()
    {
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        timer.text = currentTime.ToString("F2"); // Display time up to 2 decimal places
    }
}