using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuAssets;
    public GameObject CreditsAssets;
    public Image fadeImage;
    public float fadeDuration = .75f;

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        StartCoroutine(onStartFade());
    }

    void Update()
    {
        
    }

    public void goToCredits()
    {
        StartCoroutine(fadeToCredits());
    }

    public void goToMain()
    {
        StartCoroutine(fadeToMainMenu());
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

    public IEnumerator onStartFade()
    {
        fadeImage.gameObject.SetActive(true);
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
