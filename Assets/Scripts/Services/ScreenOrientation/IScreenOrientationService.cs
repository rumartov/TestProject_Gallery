namespace Services.ScreenOrientation
{
    public interface IScreenOrientationService : IService
    {
        void SetScreenOrientation(UnityEngine.ScreenOrientation screenOrientation);
    }
}