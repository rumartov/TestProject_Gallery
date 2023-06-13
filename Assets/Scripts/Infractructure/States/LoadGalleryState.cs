using Services.Gallery;
using Services.GameFactory;
using Services.ScreenOrientation;
using UnityEngine;

namespace Infractructure.States
{
  public class LoadGalleryState : IEnterableState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IGalleryService _galleryService;
    private readonly IScreenOrientationService _screenOrientationService;
    private readonly IScreenOrientationService _screen;
    private bool _galleryIsLoaded;

    public LoadGalleryState(GameStateMachine gameGameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IGalleryService galleryService, IScreenOrientationService screenOrientationService)
    {
      _gameStateMachine = gameGameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _galleryService = galleryService;
      _screenOrientationService = screenOrientationService;
    }

    public void Return()
    { 
      _loadingCurtain.Show();
      _gameStateMachine.Enter<LoadMenuState>();
      _galleryIsLoaded = false;
      
      Exit();
    }

    public void Enter()
    {
      _screenOrientationService.SetScreenOrientation(ScreenOrientation.Portrait);
      _loadingCurtain.Show();
      
      _sceneLoader.Load(Constants.Gallery, _loadingCurtain, OnGalleryLoaded);
    }

    public void Exit()
    {
      _galleryService.CleanUp();
    }

    private void OnGalleryLoaded()
    {
      if (_galleryIsLoaded == false)
      {
        InitGalleryHud();
        _galleryIsLoaded = true; 
        
        _galleryService.LoadImages(Constants.InitialGalleryImagesAmount);
      }
      
      _loadingCurtain.Hide();
    }

    private void InitGalleryHud() => _gameFactory.CreateGalleryHud();
  }
}