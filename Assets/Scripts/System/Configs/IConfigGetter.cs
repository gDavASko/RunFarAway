using System;

public interface IConfigGetter: IDisposable
{
    T GetConfig<T>() where T: class;
}