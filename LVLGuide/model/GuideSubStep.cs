namespace LVLGuide.model
{
    public class GuideSubStep
    {
        public bool IsComplete { get; set; }
        public string Text { get; }

        public GuideSubStep(string text)
        {
            Text = text;
        }
    }
}