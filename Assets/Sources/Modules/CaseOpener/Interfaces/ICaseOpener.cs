using Sources.Modules.Weapon.WeaponData;

namespace Sources.Modules.CaseOpener.Interfaces
{
    public interface ICaseOpener
    {
        public void Open(WeaponData[] weaponDatas);
    }
}