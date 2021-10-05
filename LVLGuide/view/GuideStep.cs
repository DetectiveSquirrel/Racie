namespace LVLGuide.view
{
    public class GuideStep
    {
        public bool IsComplete { get; set; } = false;
        public string Text { get; }

        public GuideStep(string text)
        {
            Text = text;
        }
    }
}