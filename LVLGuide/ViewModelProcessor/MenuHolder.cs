using System;
using System.Collections.Generic;
using ImGuiNET;

namespace LVLGuide.ViewModelProcessor
{
    public class MenuHolder
    {
        public string Name { get; set; } = "";
        public string Unique => $"{Name}##{ID}";
        public int ID { get; set; } = -1;
        public bool SameLine { get; set; }
        public Action? DrawDelegate { get; set; }
        public IList<MenuHolder> Children { get; } = new List<MenuHolder>();

        public void Draw()
        {
            if (Children.Count <= 0)
            {
                if (SameLine)
                {
                    ImGui.SameLine();
                }

                DrawDelegate?.Invoke();

                return;
            }

            ImGui.BeginGroup();
            foreach (var child in Children)
            {
                child.Draw();
            }

            ImGui.EndGroup();
            DrawDelegate?.Invoke();
        }
    }
}
