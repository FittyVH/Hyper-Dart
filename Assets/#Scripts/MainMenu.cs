using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int currentIndex;

    [SerializeField] GameObject howToPLayUI;
    [SerializeField] GameObject creditsUI;

    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnPlayClicked()
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
    public void OnHowTOPlayClicked()
    {
        howToPLayUI.SetActive(true);
    }
    public void OnCreditsClicked()
    {
        creditsUI.SetActive(true);
    }
    public void HowToPlayBack()
    {
        howToPLayUI.SetActive(false);
    }
    public void CreditsBack()
    {
        creditsUI.SetActive(false);
    }
}
