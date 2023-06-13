using System;
using System.Runtime.CompilerServices;
using Services.GameFactory;
using Services.LoadTextureFromUrl;
using UnityEngine.UI;

namespace Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly int _imagesAmount;
        private int _loadedImages;

        private readonly ILoadTextureFromUrlService _loadImageFromUrlService;
        private IGameFactory _gameFactory;
        public GalleryService(ILoadTextureFromUrlService loadImageFromUrlService)
        {
            _loadImageFromUrlService = loadImageFromUrlService;

            _loadedImages = Constants.FirstImageIndexInStorage;
            _imagesAmount = Constants.ImagesAmount;
        }

        public void Construct(IGameFactory gameFactory) => _gameFactory = gameFactory;

        public void CleanUp()
        {
            _loadedImages = Constants.FirstImageIndexInStorage;
        }
        
        public void OnScroll(float scrollValue)
        {
            if (scrollValue < 0 && HasImagesOnServer())
                LoadImage();
        }

        public void LoadImages(int amount, Action onLoaded)
        {
            for (int i = 0; i < amount; i++)
            {
                if (i == amount - 1)
                {
                    LoadImage(onLoaded);
                } 
            
                LoadImage();
            }
        }

        public void LoadImage(Action onLoaded = null)
        {
            LoadFromServer(onLoaded);
        }
    
        private bool HasImagesOnServer() => _loadedImages < _imagesAmount;

        private void LoadFromServer(Action onLoaded)
        {
            Image image = _gameFactory.CreateImage().GetComponent<Image>();

            _loadImageFromUrlService.LoadImage(image.gameObject, _loadedImages, onLoaded);
        
            _loadedImages++;
        }
    }
}