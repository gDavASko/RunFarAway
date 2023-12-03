using System;

namespace RFW.Saves
{
    public interface IStorableParams: IDisposable
    {
        System.Action<SaveKey>  OnValueChanged { get; set; }

        void Save();
        void Load();
        bool Contains(string key);
        bool TryGetValue(string key, out SaveParameter parameter);
        object Get(SaveParameter parameter);
        object GetParameter(SaveParameter parameter);
        int Get(SaveKey key, int defaultValue);
        float Get(SaveKey key, float defaultValue);
        string Get(SaveKey key, string defaultValue);
        object Get(SaveKey key, object defaultValue);
        void Set(SaveParameter parameter);
        void Set(SaveKey key, object value);
        void SetParameter(SaveParameter parameter);
        void AddParameter(SaveParameter parameter);
    }
}