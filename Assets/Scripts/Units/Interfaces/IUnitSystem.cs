using System;

namespace RFW
{
    public interface IUnitSystem: IDisposable
    {
        Type SystemType
        {
            get;
        }
    }
}