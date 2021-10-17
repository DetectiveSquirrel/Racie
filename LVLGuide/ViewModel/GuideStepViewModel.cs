using LVLGuide.ViewModelProcessor;
using LVLGuide.ViewModelProcessor.Nodes;

namespace LVLGuide.ViewModel
{
    public class GuideStepViewModel:IMenu
    {
        [HideName]
        public ToggleNode Done { get; set; }
        [SameLine]
        public LabelNode Text { get; set; }
    }
}