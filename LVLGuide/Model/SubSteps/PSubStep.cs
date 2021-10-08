using ExileCore;

namespace LVLGuide.model.SubSteps
{
    public class PSubStep : IGuideSubStep
    {
        private readonly string _waypointName;
        private int _wayPointIndex = -1;

        public PSubStep(string text, string waypointName)
        {
            _waypointName = waypointName;
            Text = text;
        }

        public bool IsComplete { get; set; }
        public string Text { get; }

        public void Update(GameController gameController)
        {
            var waypoints = gameController.Game.IngameState.ServerData.WaypointsUnlockState;
            if (_wayPointIndex == -1)
            {
                var areas = gameController.Files.WorldAreas.EntriesList;
                var index = 0;
                foreach (var worldArea in areas)
                {
                    if (!worldArea.HasWaypoint) continue;
                    if (worldArea.Name == _waypointName)
                    {
                        _wayPointIndex = index;
                        IsComplete = waypoints[_wayPointIndex];
                        return;
                    }
                    index++;
                }
            }
            else
            {
                IsComplete = waypoints[_wayPointIndex];
            }
        }
    }
}