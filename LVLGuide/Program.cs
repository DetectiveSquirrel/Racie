using System.Collections.Generic;
using System.IO;
using ExileCore;
using LVLGuide.view;

namespace LVLGuide
{
    public class Program : BaseSettingsPlugin<Settings>
    {
        private readonly GuideWindow _guideWindow = new();
        private List<GuideStep> _steps = new();
        private int _stepIdx = 0;
        private float Progress => (float) (_stepIdx) / (_steps.Count - 1);

        public Program()
        {
            Name = "LVLGuide";
            var path = $"{DirectoryFullName}\\Guide.txt";
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                DebugWindow.LogMsg(line);
                _steps.Add(new GuideStep(line));
            }
        }

        public override void Render()
        {
            var currentStep = _steps[_stepIdx];
            if (currentStep.IsComplete && _stepIdx < (_steps.Count - 1))
            {
                _stepIdx++;
            }

            _stepIdx += _guideWindow.Draw(Settings, _steps[_stepIdx], _stepIdx + 1, Progress);
            if (_stepIdx >= _steps.Count)
            {
                _stepIdx--;
            }
        }
    }
}