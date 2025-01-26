using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndingButtons : MonoBehaviour
{
    public LocationManager LocationManager;
    public GameObject goodAssets;
    public GameObject badAssets;
    public GameObject buttons;
    public Image fadeImage;
    public float fadeDuration = .75f;

    public bool executionDone = false;
    public bool continue1 = false;
    // Start is called before the first frame update
    void Start()
    {
        badAssets.SetActive(false);
        goodAssets.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(executionDone == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                continue1 = true;
            }
        }

        if(continue1 == true)
        {
            LocationManager.advanceDialogue();
        }
    }

    public void badOption()
    {
        StartCoroutine(fadeBad());
    }

    public void goodOption()
    {
        StartCoroutine(fadeGood());
    }


    public IEnumerator fadeGood()
    {
        Debug.Log("good");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));

        goodAssets.SetActive(true);
        badAssets.SetActive(false);
        buttons.SetActive(false);

        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
        executionDone = true;
    }

    public IEnumerator fadeBad()
    {
        Debug.Log("bad");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));

        badAssets.SetActive(true);
        goodAssets.SetActive(false);
        buttons.SetActive(false);

        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
        executionDone = true;
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
}
