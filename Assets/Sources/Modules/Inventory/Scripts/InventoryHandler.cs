using System;
using Sources.Modules.CaseOpener.Interfaces;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryHandler : IDisposable
    {
        private readonly InventoryFactory _inventoryFactory;
        private readonly ICaseOpenerHandler _caseOpenerHandler;

        public InventoryHandler(InventoryFactory inventoryFactory, ICaseOpenerHandler caseOpenerHandler)
        {
            _inventoryFactory = inventoryFactory;
            _caseOpenerHandler = caseOpenerHandler;
            
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }
        
        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            _inventoryFactory.Create(weapon);
        }

        public void Dispose()
        {
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }
    }
}