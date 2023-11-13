public interface IConfigGetter
{
    T GetConfig<T>() where T: class;
}