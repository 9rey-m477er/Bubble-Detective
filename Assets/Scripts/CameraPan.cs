using UnityEngine;

public class CanvasPan : MonoBehaviour
{
    public RectTransform image; // The RectTransform of the image inside the canvas
    public RectTransform canvas; // The RectTransform of the canvas
    public float panSpeed = 300f; // Speed of the panning
    public float panThreshold = 50f; // Distance from the edge of the screen to start panning

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Calculate the visible width of the canvas in pixels
        float canvasWidth = canvas.rect.width;
        float imageWidth = image.rect.width;

        // Calculate the minimum and maximum X positions for the image
        float minX = -(imageWidth - canvasWidth) / 2f;
        float maxX = (imageWidth - canvasWidth) / 2f;

        // Pan right if the mouse is near the right edge of the screen
        if (mousePosition.x >= Screen.width - panThreshold)
        {
            image.anchoredPosition -= new Vector2(panSpeed * Time.deltaTime, 0);
        }
        // Pan left if the mouse is near the left edge of the screen
        else if (mousePosition.x <= panThreshold)
        {
            image.anchoredPosition += new Vector2(panSpeed * Time.deltaTime, 0);
        }

        // Clamp the X position of the image to prevent empty space from showing
        image.anchoredPosition = new Vector2(
            Mathf.Clamp(image.anchoredPosition.x, minX, maxX),
            image.anchoredPosition.y
        );
    }
}
