using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.ButtonsPanel.Scripts
{
    public class ButtonsPanelRoot : MonoBehaviour
    {
        [SerializeField] private List<ButtonsPanel> _buttonsPanels;
        
        private ButtonsPanelHandler _buttonsPanelHandler;
        private void Awake()
        {
            _buttonsPanelHandler = new ButtonsPanelHandler(_buttonsPanels);

            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.Init();
            
            _buttonsPanelHandler.Enable();
        }

        private void OnEnable()
        {
            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.Enable();
        }

        private void OnDisable()
        {
            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.Disable();
            
            _buttonsPanelHandler.Disable();
        }
    }
}
