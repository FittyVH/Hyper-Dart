using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    [Header("Script refereces")]

    public Movement movement;
    public Timer timer;

    [Header("Other vars")]

    public float completionTime;
    public GameObject gameCompleteUI;
    [SerializeField] GameObject[] checkPoints;
    int i = 0;

    [SerializeField] TMP_Text checkPointCount;
    [SerializeField] int countValue = 0;

    Vector3 raycastPos = new Vector3(0f, 0f, 1.4f);

    void Start()
    {
        gameCompleteUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.movement.normalized, out RaycastHit hit, movement.movement.magnitude))
        {
            if (hit.collider.tag == "Target")
            {
                Debug.Log("chod diya");
                gameCompleteUI.SetActive(true);
                Time.timeScale = 0f;
            }

            if (hit.collider.tag == "Bubble")
            {
                if (i == checkPoints.Length - 2)
                {
                    checkPoints[i].SetActive(false);
                    // i++;
                    checkPoints[checkPoints.Length - 1].SetActive(false);
                }
                else
                {
                    checkPoints[i].SetActive(false);
                    i++;
                    checkPoints[i].SetActive(true);
                }

                // UI display
                countValue += 1;
                checkPointCount.text = countValue.ToString();
            }

            else if (hit.collider.tag == "Slow")
            {
                Time.timeScale = 0.5f;
            }
        }
    }

    void ShowGameComplete()
    {
        gameCompleteUI.SetActive(true);
    }
}
