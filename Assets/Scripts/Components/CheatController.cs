using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components
{
    /// <summary>
    /// Добавляет возможность использовать читы
    /// </summary>
    public class CheatController : MonoBehaviour
    {
        [SerializeField] private CheatCode[] _codes;
        [SerializeField] private float _resetInputTime;

        private string _cheatCode;
        private float _estimateResetTime;

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput;
        }

        private void OnDestroy()
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }

        private void OnTextInput(char c)
        {
            _estimateResetTime = _resetInputTime;
            _cheatCode += c;
            HandleInputCheat();
        }

        private void FixedUpdate()
        {
            if (_estimateResetTime < 0)
            {
                _cheatCode = "";
            }
            else
            {
                _estimateResetTime -= Time.deltaTime;
            }
        }

        private void HandleInputCheat()
        {
            foreach (var cheatCode in _codes)
            {
                if (cheatCode.Name != _cheatCode) continue;
                cheatCode.Action.Invoke();
                _cheatCode = "";
            }
        }
    }

    [Serializable]
    public class CheatCode
    {
        public string Name;
        public UnityEvent Action;
    }
}
