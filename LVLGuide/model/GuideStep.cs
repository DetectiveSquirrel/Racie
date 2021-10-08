using System.Collections.Generic;
using System.Linq;
using ExileCore;

namespace LVLGuide.model
{
    public class GuideStep
    {
        public readonly IList<IGuideSubStep> SubSteps;

        public bool IsComplete
        {
            get { return SubSteps.All(step => step.IsComplete); }
        }

        public void Update(GameController gameController)
        {
            foreach (var guideSubStep in SubSteps)
            {
                guideSubStep.Update(gameController);
            }
        }

        public GuideStep(IList<IGuideSubStep> subSteps)
        {
            SubSteps = subSteps;
        }
    }
}