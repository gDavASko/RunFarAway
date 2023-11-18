namespace RFW
{
    public interface IInput
    {
        System.Action<ActionType, bool> OnAction { get; set; }
    }
}