using Sources.Modules.Weapon.WeaponData;


namespace Sources.Modules.WeaponCaseOpener.Scripts
{
    public class WeaponCaseOpenerRoot : Common.Scripts.Weapon
    {
        
        
        public void Init(WeaponData weaponData)
        {
            View.UpdateData(weaponData);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}