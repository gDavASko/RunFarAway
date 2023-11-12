using System;
using System.Collections.Generic;

namespace RFW.Saves
{
    public abstract class Saves : IStorableParams
    {
        protected List<SaveParameter>   parameters = null;

        public Action<SaveKey> OnValueChanged { get; set; }

        public abstract void Save();

        public abstract void Load();

        public virtual bool Contains(string key)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].KeyString == key)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool TryGetValue(string key, out SaveParameter parameter)
        {
            parameter = null;
            if (parameters == null)
            {
                return false;
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].KeyString == key)
                {
                    parameter = parameters[i];
                    return true;
                }
            }
            return false;
        }

        public object Get(SaveParameter parameter)
        {
            return GetParameter(parameter);
        }

        public object GetParameter(SaveParameter parameter)
        {
            SaveParameter saveParameter = null;
            if (!TryGetValue(parameter.KeyString, out saveParameter))
            {
                AddParameter(parameter);
            }
            else
            {
                parameter = saveParameter;
            }
            parameter.LoadFromSystem();

            return parameter.Value;
        }

        public int Get(SaveKey key, int defaultValue)
        {
            return (int)Get(key, (object)defaultValue);
        }

        public float Get(SaveKey key, float defaultValue)
        {
            return (float)Get(key, (object)defaultValue);
        }

        public string Get(SaveKey key, string defaultValue)
        {
            return (string)Get(key, (object)defaultValue);
        }

        public object Get(SaveKey key, object defaultValue)
        {
            SaveParameter parameter = null;
            System.Type type = defaultValue.GetType();
            if (type == typeof(string))
            {
                parameter = new SaveString(key, (string)defaultValue, this);
            }
            else if (type == typeof(int))
            {
                parameter = new SaveInt(key, (int)defaultValue, this);
            }
            else if (type == typeof(float))
            {
                parameter = new SaveFloat(key, (float)defaultValue, this);
            }

            return GetParameter(parameter);
        }

        public void Set(SaveParameter parameter)
        {
            SetParameter(parameter);
        }

        public void Set(SaveKey key, object value)
        {
            SaveParameter parameter = null;
            System.Type type = value.GetType();
            if (type == typeof(string))
            {
                parameter = new SaveString(key, (string)value, this);
            }
            else if (type == typeof(int))
            {
                parameter = new SaveInt(key, (int)value, this);
            }
            else if (type == typeof(float))
            {
                parameter = new SaveFloat(key, (float)value, this);
            }

            SetParameter(parameter);
        }

        public virtual void SetParameter(SaveParameter parameter)
        {
            SaveParameter saveParameter = null;
            if (!TryGetValue(parameter.KeyString, out saveParameter))
            {
                saveParameter = parameter;
                AddParameter(parameter);
            }
            else
            {
                saveParameter.Value = parameter.Value;
            }

            saveParameter.SaveToSystem();
            OnValueChanged?.Invoke(parameter.Key);
        }

        public virtual void AddParameter(SaveParameter parameter)
        {
            if (parameters == null)
            {
                parameters = new List<SaveParameter>();
            }

            parameters.Add(parameter);
        }
    }
}