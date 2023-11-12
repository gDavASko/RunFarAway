namespace RFW
{
    public interface IAnimationController: IUnitSystem
    {
        void Animate(string animationID, bool loop = false, System.Action<string> OnAnimationComplete = null);
    }
}