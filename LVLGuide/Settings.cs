
using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;

namespace LVLGuide
{
    public class Settings : ISettings
    {
        public Settings()
        {
        }
        
        [Menu("Reload Button")]
        public ExileCore.Shared.Nodes.ButtonNode ReloadButton { get; } = new();
        public ExileCore.Shared.Nodes.ToggleNode Enable { get; set; } = new(false);
        public float PosX { get; set; } = 40;
        public float PosY { get; set; } = 40;
        public float Width { get; set; } = 350;

        public float Height { get; set; } = 180;
    }
}