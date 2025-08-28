namespace Evalve.Contracts
{
    public enum InteractionType
    {
        Idle,
        Selected,
        Dragging,
    }
    
    public interface IInteractable
    {
        string GetId();
        void SetInteraction(InteractionType interaction);
    }
}