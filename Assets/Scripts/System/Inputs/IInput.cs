namespace RFW
{
    public interface IInput
    {
        System.Action<ActionType> OnAction { get; set; }
    }
}