using UnityEngine;

namespace RFW
{
    public class GameItemWeapon : GameItemBase
    {
        [SerializeField] private string _weaponId = "weapon";

        public override bool IsWeapon => true;
        public override float Count => 1f;
        public override string id => _weaponId;
    }
}