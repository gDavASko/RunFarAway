using UnityEngine;

namespace RFW.Saves
{
    public class SaveString : SaveParameter
    {
        private string value = "";

        public override object Get()
        {
            return value;
        }

        public override void Set(object value)
        {
            this.value = (string)value;
        }

        public override void SaveToSystem()
        {
            PlayerPrefs.SetString(KeyString, (string)Value);
        }

        public override void LoadFromSystem()
        {
            Value = PlayerPrefs.GetString(KeyString, value);
        }

        public SaveString(SaveKey key, string value, IStorableParams _storableParams) : base(_storableParams)
        {
            this.key = key;
            this.value = value;
        }
    }
}