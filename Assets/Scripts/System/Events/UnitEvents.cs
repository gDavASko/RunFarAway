namespace RFW.Events
{
    public class UnitEvents
    {
        public System.Action<IUnitDeathContext> OnUnitDeath {get; set; }
        public System.Action<IUnit> OnUnitCreated { get; set; }
    }
}