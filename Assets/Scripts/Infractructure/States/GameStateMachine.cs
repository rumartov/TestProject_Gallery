using System;
using System.Collections.Generic;
using Services;
using Services.Gallery;
using Services.GameFactory;
using Services.ScreenOrientation;

namespace Infractructure.States
{
  public class GameStateMachine
  {
    public IState ActiveState { get; set; }

    private Dictionary<Type, IState> _states;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services, ICoroutineRunner coroutineRunner)
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(BootstrapState)] = new BootstrapState(
            this,
            sceneLoader,
            services,
            coroutineRunner,
            loadingCurtain),
        
        [typeof(LoadMenuState)] = new LoadMenuState(
            this,
            sceneLoader,
            loadingCurtain,
            services.Single<IGameFactory>(),
            services.Single<IScreenOrientationService>()),
        
        [typeof(LoadGalleryState)] = new LoadGalleryState(
          this,
          sceneLoader,
          loadingCurtain,
          services.Single<IGameFactory>(),
            services.Single<IGalleryService>(),
          services.Single<IScreenOrientationService>()),
        
        [typeof(LoadViewState)] = new LoadViewState(
          this,
          sceneLoader,
          loadingCurtain,
          services.Single<IGameFactory>(),
          services.Single<IScreenOrientationService>()),
        
      };
    }
    
    public void Enter<TState>() where TState : class, IEnterableState
    {
      IEnterableState enterableState = ChangeState<TState>();
      
      enterableState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      IPayloadedState<TPayload> state = ChangeState<TState>();
       
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IState
    {
      ActiveState?.Exit();
      
      TState state = GetState<TState>();
      ActiveState = state;
      
      return state;
    }

    public TState GetState<TState>() where TState : class, IState => 
      _states[typeof(TState)] as TState;
  }
}