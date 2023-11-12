namespace RFW.Saves
{
    public class PrefsSaves : Saves
    {
        public override void Save()
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                parameters[i].SaveToSystem();
            }
        }

        public override void Load()
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                parameters[i].LoadFromSystem();
            }
        }
    }
}