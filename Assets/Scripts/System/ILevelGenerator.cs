namespace RFW
{
    public interface ILevelGenerator: IConstructable, IInitializable
    {
        void CreateNext();
        void RegenerateCurrent();
    }
}