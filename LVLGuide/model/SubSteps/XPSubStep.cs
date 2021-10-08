using ExileCore;

namespace LVLGuide.model.SubSteps
{
    public class XpSubStep : IGuideSubStep
    {
        private readonly int _lvlWanted;
        public string Text { get; }

        public bool IsComplete { get; set; }

        public XpSubStep(string text, int lvlWanted)
        {
            Text = string.IsNullOrEmpty(text) ? $"Grind to LVL {lvlWanted}" : text;
            _lvlWanted = lvlWanted;
        }

        public void Update(GameController gameController)
        {
            if (!IsComplete)
            {
                IsComplete = gameController.Game.IngameState.ServerData.PlayerInformation.Level >= _lvlWanted;
            }
            DebugWindow.LogMsg($"Is complete: {IsComplete}");
        }
    }
}