using System.Linq;
using ExileCore;

namespace LVLGuide.model.SubSteps
{
    public class QsSubStep : IGuideSubStep
    {
        private readonly int _wantedStageId;
        private readonly string _questId;
        private readonly bool _updateText;
        public string Text { get; private set; }

        public bool IsComplete { get; set; }

        public QsSubStep(string text, int wantedStageId, string questId)
        {
            _wantedStageId = wantedStageId;
            _questId = questId;
            Text = text;
            if (string.IsNullOrEmpty(Text))
            {
                _updateText = true;
            }
            
        }

        public void Update(GameController gameController)
        {
            var questState = gameController.IngameState.IngameUi.GetQuestStates
                .Where(x => x.Value.Key.Id == _questId)
                .Select(x => x.Value.Value)
                .First();
            if (questState == null)
            {
                return;
            }
            IsComplete = questState.QuestStateId <= _wantedStageId;
            if (_updateText)
            {
                Text = questState.QuestStateText;
            }
        }
    }
}