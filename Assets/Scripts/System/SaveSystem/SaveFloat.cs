using UnityEngine;

namespace RFW.Saves
{
    public class SaveFloat : SaveParameter
    {
        private float value = 0f;

        public override object Get()
        {
            return value;
        }

        public override void Set(object value)
        {
            this.value = (float)value;
        }

        public override void SaveToSystem()
        {
            PlayerPrefs.SetFloat(KeyString, (float)Value);
        }

        public override void LoadFromSystem()
        {
            Value = PlayerPrefs.GetFloat(KeyString, value);
        }

        public SaveFloat(SaveKey key, float value, IStorableParams _storableParams) : base(_storableParams)
        {
            this.key = key;
            this.value = value;
        }
    }
}