using Services.GameFactory;
using Services.ScreenOrientation;
using Unity.VisualScripting;
using UnityEngine;

namespace Infractructure.States
{
  public class LoadMenuState : IEnterableState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IScreenOrientationService _screenOrientationService;
    
    public LoadMenuState(GameStateMachine gameGameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, 
      IGameFactory gameFactory, IScreenOrientationService screenOrientationService)
    {
      _gameStateMachine = gameGameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _screenOrientationService = screenOrientationService;
    }

    public void Enter()
    {
      _screenOrientationService.SetScreenOrientation(ScreenOrientation.Portrait);
      _loadingCurtain.Show();
      
      _sceneLoader.Load(Constants.Menu, _loadingCurtain, OnLoaded);
    }

    public void Return() => Application.Quit();

    public void Exit()
    {
      //_loadingCurtain.Hide();
    }

    private void OnLoaded()
    {
      InitMenuHud();
      _loadingCurtain.Hide();
    }

    private void InitMenuHud() => _gameFactory.CreateMenuHud();
  }
}