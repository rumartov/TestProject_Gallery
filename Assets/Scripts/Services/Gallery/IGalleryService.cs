using System;
using Services.GameFactory;

namespace Services.Gallery
{
    public interface IGalleryService : IService
    {
        void OnScroll(float scrollValue);
        void Construct(IGameFactory gameFactory);
        void LoadImages(int amount, Action onLoaded = null);
        void CleanUp();
    }
}