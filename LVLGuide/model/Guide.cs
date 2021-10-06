using System.Collections.Generic;
using System.Linq;
using ExileCore;

namespace LVLGuide.model
{
    public class Guide
    {
        private readonly IList<GuideStep> _steps;
        private int _stepIdx = 0;
        private readonly int _stepsCount;

        public Guide(IList<GuideStep> steps, int startIdx = 0)
        {
            _steps = steps;
            DebugWindow.LogMsg(steps.Count.ToString());
            _stepsCount = steps.Count;
            _stepIdx = startIdx;
        }

        public GuideStep GetCurrentStep()
        {
            return _steps[_stepIdx];
        }

        public void Previous()
        {
            EnsureBounds();

            if (_stepIdx == 0)
            {
                return;
            }

            _stepIdx--;
        }

        public void Next()
        {
            EnsureBounds();
            if (_stepIdx == _stepsCount - 1)
            {
                return;
            }

            _stepIdx++;
        }

        private void EnsureBounds()
        {
            EnsureMinBound();
            EnsureMaxBound();
        }

        private void EnsureMinBound()
        {
            if (_stepIdx < 0)
            {
                _stepIdx = 0;
            }
        }

        private void EnsureMaxBound()
        {
            if (_stepIdx >= _stepsCount)
            {
                _stepIdx = _stepsCount - 1;
            }
        }

        public float Progress()
        {
            return (float) _steps.Count(step => step.IsComplete) / _stepsCount;
        }

        public int Step()
        {
            return _stepIdx + 1;
        }

        public int Steps()
        {
            return _stepsCount;
        }

        public bool HasPrev()
        {
            return _stepIdx > 0;
        }

        public bool HasNext()
        {
            return _stepIdx < _stepsCount - 1;
        }
    }
}