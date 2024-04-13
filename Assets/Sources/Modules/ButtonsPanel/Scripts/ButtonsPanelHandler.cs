using System.Collections.Generic;
using System.Linq;

namespace Sources.Modules.ButtonsPanel.Scripts
{
    public class ButtonsPanelHandler
    {
        private readonly List<ButtonsPanel> _buttonsPanels;

        public ButtonsPanelHandler(List<ButtonsPanel> buttonsPanels)
        {
            _buttonsPanels = buttonsPanels;
            SwitchPanel(buttonsPanels.First());
        }

        public void Enable()
        {
            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.Clicked += SwitchPanel;
        }

        public void Disable()
        {
            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.Clicked -= SwitchPanel;
        }

        private void SwitchPanel(ButtonsPanel buttonsPanel)
        {
            DisableAll();
            
            buttonsPanel.EnableUI();
        }

        private void DisableAll()
        {
            foreach (var buttonsPanel in _buttonsPanels)
                buttonsPanel.DisableUI();
        }
    }
}
