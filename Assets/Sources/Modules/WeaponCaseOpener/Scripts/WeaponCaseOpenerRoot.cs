
namespace Sources.Modules.WeaponCaseOpener.Scripts
{
    public class WeaponCaseOpenerRoot : Common.Scripts.Weapon
    {
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