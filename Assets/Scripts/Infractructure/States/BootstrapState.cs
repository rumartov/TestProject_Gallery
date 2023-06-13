using Services;
using Services.Assets;
using Services.Gallery;
using Services.GameFactory;
using Services.LoadTextureFromUrl;
using Services.ScreenOrientation;
using Ui;
using UnityEngine;

namespace Infractructure.States
{
  public class BootstrapState : IEnterableState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    private readonly GalleryHudRoot _galleryHudRoot;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGalleryService _galleryService;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services,
      ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      _coroutineRunner = coroutineRunner;
      _loadingCurtain = loadingCurtain;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Constants.Initial, _loadingCurtain, onLoaded: EnterLoadLevel);

    public void Return() => Application.Quit();

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IInputService>(new InputService(
        _gameStateMachine,
        _coroutineRunner,
        _sceneLoader));
      
      _services.RegisterSingle<IAssetService>(new AssetService());
      
      _services.RegisterSingle<IScreenOrientationService>(new ScreenOrientationService());

      _services.RegisterSingle<ILoadTextureFromUrlService>(new LoadTextureFromUrlService(
        _coroutineRunner));
      
      _services.RegisterSingle<IGalleryService>(new GalleryService(
        _services.Single<ILoadTextureFromUrlService>()));

      _services.RegisterSingle<IGameFactory>(new GameFactory(
        _gameStateMachine, 
        _services.Single<IGalleryService>(),
        _services.Single<IAssetService>()));

      ConstructGalleryService();
    }

    private void ConstructGalleryService()
    {
      _services.Single<IGalleryService>().Construct(_services.Single<IGameFactory>());
    }

    private void EnterLoadLevel()
    {
      _gameStateMachine.Enter<LoadMenuState>();
    }
  }
}