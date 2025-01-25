using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCharacterFades : MonoBehaviour
{
    public Image leftSprite;
    public Image rightSprite;
    public float fadeDuration = 0.75f;
    public List<Sprite> leftImages = new List<Sprite>();
    public List<Sprite> rightImages = new List<Sprite>();

    private int leftIndex = 0;
    private int rightIndex = 0;

    void Start()
    {
        StartCoroutine(ImageFadeSequence());
    }

    private IEnumerator ImageFadeSequence()
    {
        while (true)
        {
            // Left sprite fade out, change, and fade in
            yield return StartCoroutine(FadeImage(leftSprite, 0)); // Fade out
            leftIndex = (leftIndex + 1) % leftImages.Count; // Update to next image
            leftSprite.sprite = leftImages[leftIndex];
            yield return StartCoroutine(FadeImage(leftSprite, 1)); // Fade in

            // Right sprite fade out, change, and fade in
            yield return StartCoroutine(FadeImage(rightSprite, 0)); // Fade out
            rightIndex = (rightIndex + 1) % rightImages.Count; // Update to next image
            rightSprite.sprite = rightImages[rightIndex];
            yield return StartCoroutine(FadeImage(rightSprite, 1)); // Fade in
        }
    }

    private IEnumerator FadeImage(Image image, float targetAlpha)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
    }
}
