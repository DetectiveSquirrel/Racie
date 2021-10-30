using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using LVLGuide.model;
using LVLGuide.ViewModelProcessor;
using LVLGuide.ViewModelProcessor.Nodes;

namespace LVLGuide.ViewModel
{
    public class GuideViewModel:IMenu
    {
        public GuideViewModel(Guide guide)
        {
            GuideSteps = guide.GetCurrentStep().SubSteps.Select(x =>
            {
                var node = new GuideStepViewModel
                {
                    Done = new ToggleNode(x.IsComplete),
                    Text = new LabelNode(x.Text)
                };
                node.Done.OnValueChanged += (_, newValue) =>
                {
                    x.IsComplete = newValue;
                    guide.AutoGoNext = true;
                };
                return node;
            }).ToList();
            ButtonLeft.OnPressed += () =>
            {
                guide.AutoGoNext = false;
                guide.Previous();
            };
            ButtonRight.OnPressed += guide.Next;
            ProgressBar = new ProgressBarNode(guide.Progress());
            Label = new LabelNode($"Step {guide.Step()} of {guide.Steps()}");
        }

        public ArrowButtonNode ButtonLeft { get; } = new ArrowButtonNode { Direction = ImGuiDir.Left };
        [HideName]
        [SameLine]
        public LabelNode Label { get; }
        [SameLine]
        public ArrowButtonNode ButtonRight { get; } = new ArrowButtonNode { Direction = ImGuiDir.Right };
        public List<GuideStepViewModel> GuideSteps { get; }
        public ProgressBarNode ProgressBar { get; }
    }
}