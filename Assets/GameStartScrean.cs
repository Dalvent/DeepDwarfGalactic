using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScrean : MonoBehaviour
{
    public GameObject Text;
    public Image FadeFirstImage;
    public Image FadeImage;
    public Image FadeTextImage;
    public Image FadeAllTextImage;
    public GameObject All;
    
    [Header("Times")]
    public float FirstDark = 0.5f;
    public float FirstFade = 0.5f;
    public float FirstMessage = 1.0f;
    public float UnfadeHer = 1f;
    public float StayText = 2f;
    public float FadeAllText = 1f;
    public float UnfadeAllText = 1f;
    public float WaitBlackScrean = 0.2f;

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(FirstDark);
        yield return StartCoroutine(UnFadeImageCoroutine(FadeFirstImage, FirstFade));
        yield return new WaitForSeconds(FirstMessage);

        // Просто вызываем корутину и возвращаем управление
        yield return StartCoroutine(UnFadeImageCoroutine(FadeTextImage, UnfadeHer));

        yield return new WaitForSeconds(StayText);
        yield return StartCoroutine(FadeImageCoroutine(FadeAllTextImage, FadeAllText));
        
        FadeAllTextImage.gameObject.SetActive(false);
        FadeTextImage.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
        
        All.SetActive(true);
        yield return new WaitForSeconds(WaitBlackScrean);
        yield return StartCoroutine(UnFadeImageCoroutine(FadeImage, UnfadeAllText));
        
        FadeImage.gameObject.SetActive(false);
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
        color.a = 1f;
        image.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = 1 - Mathf.Clamp01(elapsed / duration);
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}