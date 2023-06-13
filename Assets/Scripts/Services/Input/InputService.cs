using System.Collections;
using UnityEngine;

namespace Infractructure.States
{
    public class InputService : IInputService
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;

        public InputService(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;

            _coroutineRunner.StartCoroutine(HandleInput());
        }
        
        private IEnumerator HandleInput()
        {
            while (Application.isPlaying)
            {
                Debug.Log("HandleInput");

                if (CanEscape())
                {
                    Debug.Log("EscapeTest");
                    Escape();
                }
                
                yield return null;
            }
            
            yield return null;
        }

        private void Escape()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Escape");
                Debug.Log(_gameStateMachine.ActiveState + "On Escape ");
                _gameStateMachine.ActiveState.Return();
            }
        }

        private bool CanEscape()
        {
            return _sceneLoader.CanEscape;
        }
    }
}