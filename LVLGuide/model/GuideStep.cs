using System.Collections.Generic;
using System.Linq;

namespace LVLGuide.model
{
    public class GuideStep
    {
        public readonly IList<GuideSubStep> SubSteps;

        public bool IsComplete
        {
            get { return SubSteps.All(step => step.IsComplete); }
        }

        public GuideStep(IList<GuideSubStep> subSteps)
        {
            SubSteps = subSteps;
        }
    }
}