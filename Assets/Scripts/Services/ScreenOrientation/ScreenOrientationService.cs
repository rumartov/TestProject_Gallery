using UnityEngine;

namespace Services.ScreenOrientation
{
    class ScreenOrientationService : IScreenOrientationService
    {
        public void SetScreenOrientation(UnityEngine.ScreenOrientation screenOrientation)
        {
            Screen.orientation = screenOrientation;
        }
    }
}