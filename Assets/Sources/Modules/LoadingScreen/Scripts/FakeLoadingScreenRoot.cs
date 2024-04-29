using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Sources.Modules.LoadingScreen.Scripts
{
    public class FakeLoadingScreenRoot : MonoBehaviour
    {
        [SerializeField] private Image _loadImage;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private string _sceneName;

        private const float MinAddValue = 0.001f;
        private const float MaxAddValue = 0.2f;
        private const float MaxValue = 100;

        private float _currentValue;

        private void Awake()
        {
            _currentValue = 0;

            StartCoroutine(Loading());
        }


        private IEnumerator Loading()
        {
            while (_currentValue < MaxValue)
            {
                _currentValue += Random.Range(MinAddValue, MaxAddValue);
                _loadImage.fillAmount = _currentValue / 100;
                _currentValue = Mathf.Clamp(_currentValue, 0, MaxValue);

                _progressText.text = $"{Math.Round(_currentValue ,2)}%";
                
                yield return null;
            }

            SceneManager.LoadScene(_sceneName);
        }
    }
}
