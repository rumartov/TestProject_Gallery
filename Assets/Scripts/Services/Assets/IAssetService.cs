using UnityEngine;

namespace Services.Assets
{
    public interface IAssetService : IService
    {
        GameObject Instantiate(string prefabPath);
        GameObject Instantiate(string prefabPath, Transform parent);
    }
}