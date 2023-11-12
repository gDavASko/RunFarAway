using UnityEngine;

namespace RFW.Saves
{
    public abstract class SaveParameter
    {
        protected SaveKey key = SaveKey.None;
        protected SaveParameter additional = null;
        private string keyString = "";

        private IStorableParams _storableParams = null;

        public SaveKey Key => key;

        public string KeyString
        {
            get
            {
                if (keyString.IsNullOrEmpty())
                {
                    string additional = "";
                    if (Application.isEditor)
                    {
                        additional = "ED_";
                    }

                    keyString = string.Format("{0}_{1}", additional, FullKey);
                }
                return keyString;
            }
        }

        public string FullKey
        {
            get
            {
                if (additional == null)
                {
                    return key.ToString();
                }

                return string.Format("{0}_{1}_{2}", key, Get().ToString(), additional.FullKey);
            }
        }

        public object Value
        {
            get
            {
                if (additional == null)
                {
                    return Get();
                }
                return additional.Value;
            }

            set
            {
                if (additional == null)
                {
                    Set(value);
                }
                else
                {
                    additional.Value = value;
                }
            }
        }

        public SaveParameter(IStorableParams storableParams)
        {
            _storableParams = storableParams;
        }

        public virtual void Save()
        {
            _storableParams.Set(this);
        }

        public virtual object Load()
        {
            return _storableParams.Get(this);
        }

        public virtual int Load(int defaultValue)
        {
            //Debug.LogError(KeyString + ": " + defaultValue);
            return (int)_storableParams.Get(this);
        }

        public virtual float Load(float defaultValue)
        {
            return (float)_storableParams.Get(this);
        }

        public virtual string Load(string defaultValue)
        {
            return (string)_storableParams.Get(this);
        }

        public abstract object Get();

        public abstract void Set(object value);

        public abstract void SaveToSystem();

        public abstract void LoadFromSystem();

        public void SetDepth(SaveParameter additional)
        {
            if (this.additional != null)
            {
                this.additional.SetDepth(additional);
            }
            else
            {
                this.additional = additional;
            }
        }

        private int id = -1;
        public override string ToString()
        {
            if (id == -1)
            {
                id = Random.Range(0, 32768);
            }
            return id + " - " + KeyString + ": " + Value;
        }
    }
}