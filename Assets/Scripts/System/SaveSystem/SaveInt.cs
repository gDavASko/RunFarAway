using UnityEngine;

namespace RFW.Saves
{
    public class SaveInt : SaveParameter
    {
        private int value = 0;

        public override object Get()
        {
            return value;
        }

        public override void Set(object value)
        {
            this.value = (int)value;
        }

        public override void SaveToSystem()
        {
            PlayerPrefs.SetInt(KeyString, (int)Value);
        }

        public override void LoadFromSystem()
        {
            Value = PlayerPrefs.GetInt(KeyString, (int)Value);
        }

        public SaveInt(SaveKey key, int value, IStorableParams _storableParams) : base(_storableParams)
        {
            this.key = key;
            this.value = value;
        }
    }
}