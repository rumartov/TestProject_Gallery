using System;
using UnityEngine;

namespace Services.LoadTextureFromUrl
{
    public interface ILoadTextureFromUrlService : IService
    {
        void LoadImage(GameObject loadImageTo, int loadedImages, Action onLoaded = null);
    }
}