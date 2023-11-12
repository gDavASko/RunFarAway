namespace RFW.Pool
{
    public interface IReleaser<T> where T: IPoolable<T>
    {
        void Release(T unitView);
    }
}