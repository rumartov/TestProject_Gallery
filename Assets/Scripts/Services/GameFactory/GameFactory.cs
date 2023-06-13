using Infractructure.States;
using Services.Assets;
using Services.Gallery;
using Ui;
using UnityEngine;

namespace Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGalleryService _galleryService;
        private readonly IAssetService _assetService;

        private GalleryHudRoot _galleryGalleryHudRoot;

        public GameFactory(GameStateMachine gameStateMachine, IGalleryService galleryService, IAssetService assetService)
        {
            _gameStateMachine = gameStateMachine;
            _galleryService = galleryService;
            _assetService = assetService;
        }

        public GameObject CreateImage()
        {
            GameObject imageGameObject = _assetService.Instantiate(AssetPath.Image, _galleryGalleryHudRoot.imagesContainer);
        
            imageGameObject.GetComponent<ViewImageButton>().Construct(_gameStateMachine);
        
            return imageGameObject;
        }

        public GameObject CreateGalleryHud()
        {
            GameObject menuHud = _assetService.Instantiate(AssetPath.GalleryHud);
            
            GalleryHudRoot galleryHudRoot = menuHud.GetComponentInChildren<GalleryHudRoot>();
            galleryHudRoot.imagesScrollBar.onValueChanged.AddListener(_galleryService.OnScroll);

            _galleryGalleryHudRoot = galleryHudRoot;
        
            return menuHud;
        }

        public GameObject CreateMenuHud()
        {
            GameObject menuHud = _assetService.Instantiate(AssetPath.MenuHud);
            
            menuHud.GetComponentInChildren<GalleryButton>().Construct(_gameStateMachine);
        
            return menuHud;
        }
    
        public GameObject CreateViewHud()
        {
            GameObject menuHud = _assetService.Instantiate(AssetPath.ViewHud);
            
            menuHud.GetComponentInChildren<CloseViewAdditiveSceneButton>().Construct(_gameStateMachine);
        
            return menuHud;
        }
    }
}