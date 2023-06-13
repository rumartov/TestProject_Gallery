namespace Infractructure.States
{
  public interface IEnterableState: IState
  {
    void Enter();
  }

  public interface IPayloadedState<TPayload> : IState
  {
    void Enter(TPayload payload);
  }

  public interface IState
  {
    void Return();
    void Exit();
  }
}