using Infractructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class CloseViewAdditiveSceneButton : MonoBehaviour
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
            _button.onClick.AddListener(CloseViewAdditiveScene);
        }

        private void CloseViewAdditiveScene()
        {
            _gameStateMachine.ActiveState.Return();
        }
    }
}