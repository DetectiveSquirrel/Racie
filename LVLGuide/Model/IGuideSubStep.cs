using ExileCore;

namespace LVLGuide.model
{
    public interface IGuideSubStep
    {
        public bool IsComplete { get; set; }
        public string Text { get; }
        public void Update(GameController gameController);
    }
}