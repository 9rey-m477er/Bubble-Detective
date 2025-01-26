using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndingButtons : MonoBehaviour
{
    public LocationManager LocationManager;
    public LocationObject goodEnding, badEnding;
    private LocationObject chosenEnding;
    public Image background, textbox;
    public Sprite theChair, nothing;
    public GameObject buttons;
    public Image fadeImage;
    public float fadeDuration = .75f;

    public bool executionDone = false;
    public bool continue1 = false;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Click!");
            if (executionDone == true)
            {
                executionDone = false;
                LocationManager.selectingCulprit = false;
                LocationManager.advanceLocation();
            }
        }

    }

    public void addButtons() //pop up ending buttons
    {
        buttons.SetActive(true);
    }

    public void badOption()
    {
        Debug.Log("Clicked Bad");
        chosenEnding = badEnding;
        LocationManager.addEndgameToQueue(chosenEnding);
        StartCoroutine(fadeBad());
    }

    public void goodOption()
    {
        Debug.Log("Clicked Good");
        chosenEnding = goodEnding;
        LocationManager.addEndgameToQueue(chosenEnding);
        StartCoroutine(fadeGood());
    }


    public IEnumerator fadeGood()
    {
        Debug.Log("good");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));

        background.sprite = theChair;
        textbox.sprite = nothing;
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

        background.sprite = theChair;
        textbox.sprite = nothing;
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
