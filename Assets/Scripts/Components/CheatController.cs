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

        private void OnTextInput(char c)
        {
            _estimateResetTime = _resetInputTime;
            _cheatCode += c;
        }

        private void Update()
        {
            if (_cheatCode != "")
            {
                HandleInputCheat();
            }
        }

        private void HandleInputCheat()
        {
            _estimateResetTime -= Time.deltaTime;
            if (_estimateResetTime > 0) return;
            
            foreach (var cheatCode in _codes)
            {
                if (cheatCode.Name == _cheatCode)
                {
                    cheatCode.Action.Invoke();
                }
            }
            _cheatCode = "";
            _estimateResetTime = _resetInputTime;
        }
    }

    [Serializable]
    public class CheatCode
    {
        public string Name;
        public UnityEvent Action;
    }
}
