using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExileCore;
using LVLGuide.model.SubSteps;

namespace LVLGuide.model
{
    public static class GuideParser
    {
        public static Guide ParseGuideFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var currentSteps = new List<IGuideSubStep>();
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
                currentSteps.Add(GuideStepFactory(line));
            }

            return new Guide(guideSteps);
        }

        private static IGuideSubStep GuideStepFactory(string line)
        {
            var match = line.Split('[', ']')[1];
            if (match == null)
            {
                return new DefaultSubStep(line);
            }
            // match = "QS a1q1 3"
            var commandWithArgs = match.Split(new[] {' '}, 2); // ["QS","a1q1 3"]
            var operation = commandWithArgs[0]; // QS
            var text = line;
            if (line.Contains('['))
            {
                text = line.Split('[')[0];
            }
            var commandArg = commandWithArgs[1];
            
            switch (operation)
            {
                case "QS":
                    var p = commandArg.Split(' ');
                    var stageId = Convert.ToInt32(p[1]);
                    var questId = p[0];
                    return new QsSubStep(text, stageId, questId);
                case "QT":
                    return new QsSubStep(text, 0, commandArg);
                case "G":
                    return new GSubStep(text, commandArg);
                case "P":
                    return new PSubStep(text, commandArg);
                case "WP":
                    return new WpSubStep(text, commandArg);
                case "XP":
                    DebugWindow.LogMsg(line);
                    return new XpSubStep(text, Convert.ToInt32(commandArg));
                default:
                    DebugWindow.LogMsg($"Operation: {operation}: {line}", 10.0f);
                    return new DefaultSubStep(line);
            }
        }
    }
}