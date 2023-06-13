using Infractructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ViewImageButton : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private Button _button;
    
        public void Construct(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MoveToGalleryScene);
        }

        private void MoveToGalleryScene()
        {
            Sprite imageSprite = GetComponent<Image>().sprite;
            _gameStateMachine.Enter<LoadViewState, Sprite>(imageSprite);
        }
    }
}