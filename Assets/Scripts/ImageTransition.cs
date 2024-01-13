using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTransition : MonoBehaviour
{
    public Image[] images;
    public float transitionDuration = 2f;

    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(TransitionImages());
    }

    private IEnumerator TransitionImages()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds before transitioning

            // Crossfade from the current image to the next image
            yield return Crossfade(images[currentIndex], images[(currentIndex + 1) % images.Length]);

            currentIndex = (currentIndex + 1) % images.Length;
        }
    }

    private IEnumerator Crossfade(Image image1, Image image2)
    {
        float timer = 0f;

        Color originalColor1 = image1.color;
        Color originalColor2 = image2.color;

        while (timer < transitionDuration)
        {
            float alpha = timer / transitionDuration;

            // Use Color.Lerp to interpolate between original and target alpha values
            image1.color = Color.Lerp(originalColor1, new Color(originalColor1.r, originalColor1.g, originalColor1.b, 0f), alpha);
            image2.color = Color.Lerp(originalColor2, new Color(originalColor2.r, originalColor2.g, originalColor2.b, 1f), alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final state
        image1.color = new Color(originalColor1.r, originalColor1.g, originalColor1.b, 0f);
        image2.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, 1f);
    }
}
