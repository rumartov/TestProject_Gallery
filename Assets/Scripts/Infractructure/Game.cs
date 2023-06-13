using Infractructure.States;
using Services;

namespace Infractructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain) => 
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, coroutineRunner);
  }
}