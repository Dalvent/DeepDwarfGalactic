using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScrean : MonoBehaviour
{
    public Image FadeImage;
    public Image FadeTextImage;

    private void Start()
    {
        IEnumerator FadeCoroutine()
        {
            yield return new WaitForSeconds(1.5f);

            foreach (var p in FadeImageCoroutine(FadeTextImage, 1f)) 
                yield return p;

            yield return new WaitForSeconds(2f);
        }
        
        StartCoroutine()
    }

    public static IEnumerator FadeImageCoroutine(Image image, float duration)
    {
        float elapsed = 0;
        Color color = FadeTextImage.color;
        color.a = 0f;
        FadeTextImage.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            color.a = alpha;
            FadeTextImage.color = color;
            yield return null;
        }

        color.a = 1f;
        FadeTextImage.color = color;
    }
}
