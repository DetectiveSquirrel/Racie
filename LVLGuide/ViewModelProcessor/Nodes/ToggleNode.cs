using System;
using ExileCore;

namespace LVLGuide.ViewModelProcessor.Nodes
{
    public sealed class ToggleNode
    {
        private bool value;

        public ToggleNode(bool value) => this.Value = value;

        public bool Value
        {
            get => value;
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                try
                {
                    OnValueChanged?.Invoke(this, value);
                }
                catch (Exception ex)
                {
                    DebugWindow.LogError($"Error in function that subscribed for: ToggleNode.OnValueChanged. {Environment.NewLine} {ex}", 10f);
                }
            }
        }

        public event EventHandler<bool>? OnValueChanged;

        public static implicit operator bool(ToggleNode node) => node.Value;
    }
}