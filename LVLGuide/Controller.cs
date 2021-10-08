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
            _guide = GuideParser.ParseGuideFile($"{DirectoryFullName}\\guide.txt");
            Settings.ReloadButton.OnPressed = () =>
            {
                _guide = GuideParser.ParseGuideFile($"{DirectoryFullName}\\guide.txt");
                DebugWindow.LogMsg("Reloaded Guide.txt");
            };
            base.OnLoad();
        }

        public override Job Tick()
        {
            if (_guide == null)
            {
                return base.Tick();
            }

            var currentStep = _guide.GetCurrentStep();
            currentStep.Update(GameController);
            if (currentStep.IsComplete && _guide.HasNext() && _guide.AutoGoNext)
            {
                _guide.Next();
            }
            return base.Tick();
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