using System;

namespace RFW
{
    public class HitPointSystem : IHitPointSystem, IInitializable
    {
        private float _maxHitpoints = 10f;

        public Type SystemType => typeof(HitPointSystem);

        public System.Action OnDeath
        {
            get;
            set;
        }

        public float HitPoints
        {
            get;
            private set;
        } = 0f;

        public float HitPointPercent => HitPoints / _maxHitpoints;

        public Action<float> OnHPChanged
        {
            get;
            set;
        }

        public void Init(params object[] parameters)
        {
            _maxHitpoints = parameters.Get<float>();
            HitPoints = _maxHitpoints;
        }

        public void ChangeHitPointsTo(float value)
        {
            if (HitPoints <= 0)
                return;

            HitPoints -= value;

            OnHPChanged?.Invoke(value);

            if (HitPoints <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Kill()
        {
            ChangeHitPointsTo(HitPoints + 1);
        }

        public void Dispose()
        {
            OnDeath = null;
            OnHPChanged = null;
        }
    }
}