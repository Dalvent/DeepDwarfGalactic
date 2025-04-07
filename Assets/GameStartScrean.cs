using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScrean : MonoBehaviour
{
    public Image FadeImage;
    public Image FadeTextImage;
    public Image FadeAllTextImage;

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        // Просто вызываем корутину и возвращаем управление
        yield return StartCoroutine(FadeImageCoroutine(FadeTextImage, 1f));

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeImageCoroutine(FadeAllTextImage, 1f));
        yield return StartCoroutine(FadeImageCoroutine(FadeImage, 1f));
    }

    public static IEnumerator FadeImageCoroutine(Image image, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }
    
    public static IEnumerator UnFadeImageCoroutine(Image image, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }
}