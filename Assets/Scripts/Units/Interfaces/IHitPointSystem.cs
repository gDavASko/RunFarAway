namespace RFW
{
    public interface IHitPointSystem: IUnitSystem
    {
        float HitPoints { get; }
        float HitPointPercent { get; }

        System.Action<float> OnHPChanged { get; set; }
        System.Action OnDeath { get; set; }

        void ChangeHitPointsTo(float value);

        void Kill();
    }
}