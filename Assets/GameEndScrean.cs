using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScreen : MonoBehaviour
{
   public GameObject All;
   
   [Header("Images")]
   public Image WhiteBox;
   public Image Frame1;
   public Image Frame2;
   public Image Frame3;

   [Header("Settings")] 
   public float FadeWhiteTime;
   public float WhiteScreanTime;
   public float FadeFirstImageTime;
   public float Frame1Time;
   public float Frame2Time;

   private void Start()
   {
      WhiteBox.gameObject.SetActive(false);
      Frame1.gameObject.SetActive(false);
      Frame2.gameObject.SetActive(false);
      Frame3.gameObject.SetActive(false);
   }

   public void Show()
   {
      StartCoroutine(FadeCoroutine());
   }

   private IEnumerator FadeCoroutine()
   {
      WhiteBox.gameObject.SetActive(true);
      Frame1.gameObject.SetActive(false);
      Frame2.gameObject.SetActive(false);
      Frame3.gameObject.SetActive(false);
      
      yield return StartCoroutine(FadeImageCoroutine(WhiteBox, FadeWhiteTime));
      All.SetActive(false);
      yield return new WaitForSeconds(WhiteScreanTime);
      
      Frame1.gameObject.SetActive(true);

      // Просто вызываем корутину и возвращаем управление
      yield return StartCoroutine(UnFadeImageCoroutine(WhiteBox, FadeFirstImageTime));

      yield return new WaitForSeconds(Frame1Time);
      
      Frame1.gameObject.SetActive(false);
      Frame2.gameObject.SetActive(true);
        
      yield return new WaitForSeconds(Frame2Time);
      
      Frame2.gameObject.SetActive(false);
      Frame3.gameObject.SetActive(true);
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
