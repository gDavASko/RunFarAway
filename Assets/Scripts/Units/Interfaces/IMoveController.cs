namespace RFW
{
    public interface IMoveController: IUnitSystem, IInitializable
    {
        bool CanMove
        {
            get;
            set;
        }
    }
}