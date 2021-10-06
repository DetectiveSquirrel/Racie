using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExileCore;

namespace LVLGuide.model
{
    public static class GuideParser
    {
        public static Guide ParseGuideFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var currentSteps = new List<GuideSubStep>();
            var guideSteps = new List<GuideStep>();
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                if (string.IsNullOrEmpty(line) || index + 1 == lines.Length)
                {
                    var x = currentSteps.ToList();
                    guideSteps.Add(new GuideStep(x));
                    currentSteps.Clear();
                    continue;
                }
                currentSteps.Add(new GuideSubStep(line));
            }

            return new Guide(guideSteps);
        }
    }
}