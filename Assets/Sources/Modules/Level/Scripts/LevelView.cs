using System;
using Sources.Modules.Level.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Level.Scripts
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _currentExperienceText;
        [SerializeField] private Slider _slider;
        
        private ILevelHandlerEvent _levelHandlerEvent;

        [Inject]
        public void Construct(ILevelHandlerEvent levelHandlerEvent)
        {
            _levelHandlerEvent = levelHandlerEvent;
        }

        private void OnEnable()
        {
            _levelHandlerEvent.ExperienceUpdated += OnExperienceUpdated;
            _levelHandlerEvent.LevelLimitUpdated += OnLevelLimitUpdate;
        }

        private void OnDisable()
        {
            _levelHandlerEvent.ExperienceUpdated -= OnExperienceUpdated;
            _levelHandlerEvent.LevelLimitUpdated -= OnLevelLimitUpdate;
        }

        private void OnLevelLimitUpdate(int level, uint limit)
        {
            _levelText.text = $"Level: {level}";
            _slider.maxValue = limit;
        }

        private void OnExperienceUpdated(uint exp)
        {
            _slider.value = exp;
            _currentExperienceText.text = $"{exp}/{_slider.maxValue}";
        }
    }
}