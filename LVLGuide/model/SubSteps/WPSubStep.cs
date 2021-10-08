using ExileCore;

namespace LVLGuide.model.SubSteps
{
    public class WpSubStep: IGuideSubStep
    {
        private readonly string _zoneName;

        public WpSubStep(string text, string zoneName)
        {
            _zoneName = zoneName;
            Text = string.IsNullOrEmpty(text) ? $"WP to {zoneName}" : text;
        }

        public bool IsComplete { get; set; }
        public string Text { get; }
        public void Update(GameController gameController)
        {
            if (IsComplete)
            {
                return;
            }
            IsComplete = gameController.Area.CurrentArea.Name == _zoneName;
        }
    }
}