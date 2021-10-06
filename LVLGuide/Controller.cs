using ExileCore;
using LVLGuide.model;
using LVLGuide.view;

namespace LVLGuide
{
    public class Controller : BaseSettingsPlugin<Settings>
    {
        private readonly GuideWindow _guideWindow = new();
        private readonly Guide _guide;

        public Controller()
        {
            Name = "LVLGuide";
            var path = $"{DirectoryFullName}\\Guide.txt";
            _guide = GuideParser.ParseGuideFile(path);
        }

        public override void Render()
        {
            _guideWindow.Draw(Settings, _guide);
        }
    }
}