using System;
using System.Collections;
using Infractructure;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Services.LoadTextureFromUrl
{
    public class LoadTextureFromUrlService : ILoadTextureFromUrlService
    {
        private readonly string _storageLink = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private readonly string _extenstion = ".jpg";

        private readonly ICoroutineRunner _coroutineRunner;

        public LoadTextureFromUrlService(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

        public void LoadImage(GameObject loadImageTo, int loadedImages, Action onLoaded)
        {
            string imageUrl = _storageLink + loadedImages + _extenstion;
        
            _coroutineRunner.StartCoroutine(DownloadImage(imageUrl, loadImageTo, onLoaded));
        }

        private IEnumerator DownloadImage(string mediaUrl, GameObject loadImageTo, Action onLoaded)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
            
            yield return request.SendWebRequest();
            
            if (OnError(request))
                Debug.Log(request.error);
            else
            {
                if (loadImageTo == null)
                {
                    Debug.Log("Image GameObject destroyed before Texture2D loaded");
                    yield break;
                }

                SetDownloadedImage(loadImageTo, request);

                onLoaded?.Invoke();
            }
        }

        private static void SetDownloadedImage(GameObject loadImageTo, UnityWebRequest request)
        {
            Texture2D texture2D = ((DownloadHandlerTexture) request.downloadHandler).texture;
            Image image = loadImageTo.GetComponent<Image>();
            Sprite imageSprite = texture2D.ConvertToSprite();
            image.sprite = imageSprite;
        }

        private static bool OnError(UnityWebRequest request) =>
            request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError;
    }
}