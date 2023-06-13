using Infractructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GalleryButton : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private Button _button;

        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MoveToGalleryScene);
        }

        private void MoveToGalleryScene()
        {
            _gameStateMachine.Enter<LoadGalleryState>();
        }
    }
}