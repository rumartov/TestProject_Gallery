using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infractructure
{
  public class SceneLoader
  {
    public bool CanEscape { get; private set; }
    
    private readonly ICoroutineRunner _coroutineRunner;
    private float _loadingProgress;
    private float _loadingTolerance = 0.01f;
    private float _loadingSpeed = 5;
    private float _targetLoadingPercent;
    private float _loadingPercent;
    private Coroutine _lastCoroutine;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
      CanEscape = true;
    }

    public void Load(string name,
      LoadingCurtain loadingCurtain,
      Action onLoaded = null,
      LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
      CleanUp();
      
      _coroutineRunner.StartCoroutine(LoadScene(name, loadingCurtain, onLoaded, loadSceneMode));
    }

    private void CleanUp()
    {
      _loadingProgress = 0;
      _loadingPercent = 0;
      CanEscape = false;
    }

    public void Unload(string name, Action onLoaded = null)
    {
      _coroutineRunner.StartCoroutine(UnloadScene(name, onLoaded));
    }

    private IEnumerator LoadScene(string nextScene, LoadingCurtain loadingCurtain, Action onLoaded,
       LoadSceneMode loadSceneMode)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        ActivateSceneOnLoaded(nextScene, onLoaded, loadSceneMode);
        
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene, loadSceneMode);
      waitNextScene.allowSceneActivation = false;

      while (!waitNextScene.isDone)
      {
        yield return new WaitForSeconds(0.05f);
        
        FormatLoadingProgress(waitNextScene);
        
        LoadLoadingBar(loadingCurtain);

        LoadLoadingPercent(loadingCurtain);

        if (InLoadingBarLoaded(loadingCurtain) && IsLoadingPercentLoaded(loadingCurtain))
          waitNextScene.allowSceneActivation = true;

        yield return null;
      }

      ActivateSceneOnLoaded(nextScene, onLoaded, loadSceneMode);
    }

    private void ActivateSceneOnLoaded(string nextScene, Action onLoaded, LoadSceneMode loadSceneMode)
    {
      CanEscape = true;
      SetAdditiveSceneActive(nextScene, loadSceneMode);
      onLoaded?.Invoke();
    }

    private void FormatLoadingProgress(AsyncOperation waitNextScene)
    {
      _loadingProgress = Mathf.Clamp01(waitNextScene.progress / 0.9f);
    }

    private static bool IsLoadingPercentLoaded(LoadingCurtain loadingCurtain)
    {
      return int.Parse(loadingCurtain.LoadingPercent.text) == 100;
    }

    private bool InLoadingBarLoaded(LoadingCurtain loadingCurtain)
    {
      return Math.Abs(loadingCurtain.ProgressBar.size - 1) < _loadingTolerance;
    }

    private void LoadLoadingPercent(LoadingCurtain loadingCurtain)
    {
      _loadingPercent = Mathf.MoveTowards(
        _loadingPercent,
        _loadingProgress,
        _loadingSpeed * Time.deltaTime);
      loadingCurtain.LoadingPercent.text = String.Format("{0:0}", _loadingPercent * 100);
    }

    private void LoadLoadingBar(LoadingCurtain loadingCurtain)
    {
      loadingCurtain.ProgressBar.size = Mathf.MoveTowards(
        loadingCurtain.ProgressBar.size,
        _loadingProgress,
        _loadingSpeed * Time.deltaTime);
    }

    private static void SetAdditiveSceneActive(string nextScene, LoadSceneMode loadSceneMode)
    {
      if (loadSceneMode == LoadSceneMode.Additive)
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
    }

    private IEnumerator UnloadScene(string unloadScene, Action onLoaded = null)
    {
      /*if (SceneManager.GetActiveScene().name != unloadScene)
      {
        onLoaded?.Invoke();
        yield break;
      }*/
      
      AsyncOperation waitUnloadScene = SceneManager.UnloadSceneAsync(unloadScene);

      while (waitUnloadScene == null && !waitUnloadScene.isDone)
        yield return null;

      onLoaded?.Invoke();
    }
  }
}