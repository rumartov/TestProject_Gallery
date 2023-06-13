using Services.Gallery;
using Services.GameFactory;
using Services.ScreenOrientation;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infractructure.States
{
  public class LoadViewState : IPayloadedState<Sprite>
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IScreenOrientationService _screenOrientationService;

    public LoadViewState(GameStateMachine gameGameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IScreenOrientationService screenOrientationService)
    {
      _gameStateMachine = gameGameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _screenOrientationService = screenOrientationService;
    }

    public void Enter(Sprite image)
    {
      _loadingCurtain.Show();
      
      _screenOrientationService.SetScreenOrientation(ScreenOrientation.AutoRotation);
      
      LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
      
      _sceneLoader.Load(Constants.View, _loadingCurtain, () => OnLoaded(image), loadSceneMode);
    }

    public void Return()
    {
      Exit();
    }

    public void Exit()
    {
      _gameStateMachine.ActiveState = _gameStateMachine.GetState<LoadGalleryState>();
      _sceneLoader.Unload(Constants.View);
    }

    private void OnLoaded(Sprite image)
    {
      _loadingCurtain.Hide();
      
      InitViewHud(image);
    }

    private void InitViewHud(Sprite image)
    {
      GameObject viewHud = _gameFactory.CreateViewHud();
      viewHud.GetComponent<ViewHudRoot>().viewImage.sprite = image;
      
      viewHud.GetComponentInChildren<CloseViewAdditiveSceneButton>().Construct(_gameStateMachine);
    }
  }
}