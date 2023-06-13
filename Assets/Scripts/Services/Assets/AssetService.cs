using UnityEngine;

namespace Services.Assets
{
    class AssetService : IAssetService
    {
        public GameObject Instantiate(string prefabPath)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab);
        }
        
        public GameObject Instantiate(string prefabPath, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab, parent);
        }
    }
}