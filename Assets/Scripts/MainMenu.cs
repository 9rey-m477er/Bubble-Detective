using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuAssets, TitleCanvas;
    public GameObject CreditsAssets;
    public GameObject GameCanvas;
    public Image fadeImage;
    public float fadeDuration = .75f;

    public LocationManager locationManager;

    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI sourcesText;

    public TextMeshProUGUI creditsButtonText;

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        StartCoroutine(onStartFade());
    }

    void Update()
    {
        
    }

    public void sourcesHandler()
    {
        if(creditsButtonText.text == "SOURCES")
        {
            StartCoroutine(sourcesFadeIn());
        }
        else if(creditsButtonText.text == "CREDITS")
        {
            StartCoroutine(sourcesFadeOut());
        }
    }

    public void goToCredits()
    {
        StartCoroutine(fadeToCredits());
    }

    public void goToMain()
    {
        StartCoroutine(fadeToMainMenu());
    }

    public void goToGame()
    {
        StartCoroutine(fadeToPlay());
    }

    public IEnumerator fadeToCredits()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        CreditsAssets.SetActive(true);
        MainMenuAssets.SetActive(false);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);

    }

    public IEnumerator fadeToMainMenu()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        CreditsAssets.SetActive(false);
        MainMenuAssets.SetActive(true);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);

    }

    public IEnumerator fadeToPlay()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        yield return StartCoroutine(Fade(0));
        locationManager.gameStart();
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator onStartFade()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);

    }

    public IEnumerator sourcesFadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        creditsButtonText.text = "CREDITS";
        creditsText.gameObject.SetActive(false);
        sourcesText.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
    }


    public IEnumerator sourcesFadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        creditsButtonText.text = "SOURCES";
        creditsText.gameObject.SetActive(true);
        sourcesText.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
