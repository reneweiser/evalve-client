namespace Evalve.App.States.CreatingSessions
{
    public record ViewEvent
    {
        public string Name { get; init; }
        public string Key { get; init; }
        public object Value { get; init; }

        public static ViewEvent Create(string name, string key = null, object value = null)
        {
            return new ViewEvent { Name = name, Key = key, Value = value };
        }
    }
}