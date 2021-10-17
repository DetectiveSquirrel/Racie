namespace LVLGuide.ViewModelProcessor
{
    public class MenuConfig
    {
        public MenuConfig(string name, bool sameLine)
        {
            Name = name;
            SameLine = sameLine;
        }

        public string Name { get; }
        public bool SameLine { get; }
    }
}
