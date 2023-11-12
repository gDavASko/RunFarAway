using System.Collections.Generic;

namespace RFW
{
    public class DeathZoneChecker : IInitializable
    {
        private List<string> _deathTags = null;
        private IHitPointSystem _hpSystem = null;

        private void CheckDeath(string tag)
        {
            if (_deathTags.Contains(tag))
                _hpSystem.Kill();
        }

        public void Init(params object[] parameters)
        {
            _deathTags = parameters.Get<List<string>>();
            _hpSystem = parameters.Get<IHitPointSystem>();

            System.Action<string> OnTriggerContact = parameters.Get<System.Action<string>>();
            OnTriggerContact += CheckDeath;
        }
    }
}