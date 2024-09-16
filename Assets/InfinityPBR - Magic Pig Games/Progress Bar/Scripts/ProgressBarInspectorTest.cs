using System;
using UnityEngine;

namespace MagicPigGames
{
    public class ProgressBarInspectorTest : MonoBehaviour
    {
        [Header("Test Zone")]
        [Tooltip("Toggle on to test the progress bar in the editor, during play mode.")]
        public bool enableTesting = false;
        [Range(0f, 1f)]
        [Tooltip("Note, if testing in the editor and invertProgress is true, the progress value will be inverted.")]
        public float progress = 0f; // This is the Inspector test value for progress!

        private float _lastProgress = 0f;
        private ProgressBar _progressBar;

        protected virtual void Update()
        {
            if (!enableTesting)
            {
                Debug.Log("enableTesting i�aretlenmedi.");
                return;
            }

            if (Math.Abs(_lastProgress - progress) < 0.001) return;

            _lastProgress = progress;
            Debug.Log("�lerleme de�eri g�ncelleniyor: " + progress);
            _progressBar.SetProgress(progress);
        }


        private void OnValidate()
        {
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();
        }
    }
}

