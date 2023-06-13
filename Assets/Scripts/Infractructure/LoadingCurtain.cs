using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infractructure
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;
    public Scrollbar ProgressBar;
    public TextMeshProUGUI LoadingPercent;
    
    private void Awake() => DontDestroyOnLoad(this);
    
    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }

    public void Hide()
    {
      if (gameObject.activeInHierarchy) 
        StartCoroutine(DoFadeIn());
    }
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      gameObject.SetActive(false);
    }
  }
}