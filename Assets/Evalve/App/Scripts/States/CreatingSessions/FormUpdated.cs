namespace Evalve.App.States.CreatingSessions
{
    public record FormUpdated
    {
        public string FieldName { get; init; }
        public object Value { get; init; }
    }
}