using UnityEngine;

namespace Services.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreateMenuHud();
        GameObject CreateImage();
        GameObject CreateGalleryHud();
        GameObject CreateViewHud();
    }
}