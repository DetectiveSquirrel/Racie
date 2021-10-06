using ExileCore;
using LVLGuide.model;
using LVLGuide.view;

namespace LVLGuide
{
    public class Controller : BaseSettingsPlugin<Settings>
    {
        private readonly GuideWindow _guideWindow = new();
        private Guide? _guide;

        public Controller()
        {
            Name = "LVLGuide";
        }

        public override void OnLoad()
        {
            var path = $"{DirectoryFullName}\\Guide.txt";
            _guide = GuideParser.ParseGuideFile(path);
            Settings.ReloadButton.OnPressed = () =>
            {
                GuideParser.ParseGuideFile(path);
                DebugWindow.LogMsg("Reloaded Guide.txt");
            };
            base.OnLoad();
        }

        public override void Render()
        {
            if (_guide == null)
            {
                return;
            }
            _guideWindow.Draw(Settings, _guide);
        }
    }
}