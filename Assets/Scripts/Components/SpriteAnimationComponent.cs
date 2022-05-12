using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationComponent : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private int frameRate;
        [SerializeField] private bool loop;
        [SerializeField] private UnityEvent onComplete;

        private SpriteRenderer _spriteRenderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = 1F / frameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= sprites.Length)
            {
                if (loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false;
                    onComplete.Invoke();
                    return;
                }
            }

            _spriteRenderer.sprite = sprites[_currentSpriteIndex];
            _currentSpriteIndex++;
            _nextFrameTime += _secondsPerFrame;
        }
    }
}
