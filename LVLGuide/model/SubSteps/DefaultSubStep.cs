using ExileCore;

namespace LVLGuide.model.SubSteps
{
    public class DefaultSubStep : IGuideSubStep
    {
        public bool IsComplete { get; set; }
        public string Text { get; }

        public void Update(GameController gameController)
        {
        }

        public DefaultSubStep(string text)
        {
            Text = text;
        }
    }
}