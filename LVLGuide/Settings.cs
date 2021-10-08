
using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace LVLGuide
{
    public class Settings : ISettings
    {
        public Settings()
        {
            Enable = new ToggleNode(false);
        }
        
        [Menu("Reload Button")]
        public ButtonNode ReloadButton { get; }= new();
        public ToggleNode Enable { get; set; }
        public float PosX { get; set; } = 40;
        public float PosY { get; set; } = 40;
        public float Width { get; set; } = 350;

        public float Height { get; set; } = 180;
    }
}