using System;
using System.Runtime.InteropServices;
using ExileCore;
using ImGuiNET;
using LVLGuide.model;
using LVLGuide.ViewModel;
using LVLGuide.ViewModelProcessor;
using SharpDX;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace LVLGuide.view
{
    public class GuideWindow
    {
        public void Draw(Settings settings, Guide guide)
        {
            var oldStyle = SaveStyle();
            SetupStyle();
            var rect = CreateWindowRect(settings);
            var flags = SetupWindowFlags(rect);
            var opened = true;

            if (ImGui.Begin($"LVLGuide", ref opened, flags))
            {
                var model = new GuideViewModel(guide);
                MenuParser.Parse(model).Draw();
                UpdateSizeSettings(settings);
            }

            LoadStyle(oldStyle);
            ImGui.End();
        }

        private static void UpdateSizeSettings(Settings settings)
        {
            var pos = ImGui.GetWindowPos();
            settings.PosX = pos.X;
            settings.PosY = pos.Y;

            var size = ImGui.GetWindowSize();
            settings.Width = size.X;
            settings.Height = size.Y;
        }

        private static RectangleF CreateWindowRect(Settings settings)
        {
            ImGui.SetNextWindowPos(new Vector2(settings.PosX, settings.PosY), ImGuiCond.Once, Vector2.Zero);
            var windowSize = new Vector2(settings.Width, settings.Height);
            ImGui.SetNextWindowSize(windowSize, ImGuiCond.Always);

            var rect = new RectangleF(settings.PosX, settings.PosY, windowSize.X, windowSize.Y);
            return rect;
        }

        private static ImGuiWindowFlags SetupWindowFlags(RectangleF rect)
        {
            var flags = ImGuiWindowFlags.NoScrollbar |
                        ImGuiWindowFlags.NoBringToFrontOnFocus |
                        ImGuiWindowFlags.NoFocusOnAppearing |
                        ImGuiWindowFlags.NoSavedSettings;

            if (!rect.Contains(Input.MousePosition))
                flags ^= ImGuiWindowFlags.NoMove;
            return flags;
        }

        private static unsafe void LoadStyle(ImGuiStyle style)
        {
            var stylePtr = ImGui.GetStyle();
            Marshal.StructureToPtr(style, new IntPtr(stylePtr.NativePtr), false);
        }

        private static unsafe ImGuiStyle SaveStyle()
        {
            var style = ImGui.GetStyle();
            return Marshal.PtrToStructure<ImGuiStyle>(new IntPtr(style.NativePtr));
        }

        private void SetupStyle()
        {
            var style = ImGui.GetStyle();
            style.Alpha = 1.0f;
            style.WindowPadding = new Vector2(15, 15);
            style.WindowRounding = 0.0f;
            style.FramePadding = new Vector2(5, 5);
            style.FrameRounding = 4.0f;
            style.ItemSpacing = new Vector2(12, 8);
            style.ItemInnerSpacing = new Vector2(8, 6);
            style.IndentSpacing = 25.0f;
            style.ScrollbarSize = 15.0f;
            style.ScrollbarRounding = 9.0f;
            style.GrabMinSize = 5.0f;
            style.GrabRounding = 3.0f;

            //style.Colors[(int) ImGuiCol.Bg] = new Vector4(0.19f, 0.18f, 0.21f, 1.00f);
            //style.Colors[(int) ImGuiCol.CloseButtonActive] = new Vector4(0.40f, 0.39f, 0.38f, 1.00f);
            //style.Colors[(int) ImGuiCol.CloseButtonHovered] = new Vector4(0.40f, 0.39f, 0.38f, 0.39f);
            //style.Colors[(int) ImGuiCol.CloseButton] = new Vector4(0.40f, 0.39f, 0.38f, 0.16f);
            //style.Colors[(int) ImGuiCol.ColumnActive] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            //style.Colors[(int) ImGuiCol.ColumnHovered] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            //style.Colors[(int) ImGuiCol.Column] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.ModalWindowDimBg] = new Vector4(1.00f, 0.98f, 0.95f, 0.73f);
            style.Colors[(int) ImGuiCol.BorderShadow] = new Vector4(0.92f, 0.91f, 0.88f, 0.00f);
            style.Colors[(int) ImGuiCol.Border] = new Vector4(0.10f, 0.10f, 0.10f, 0.88f);
            style.Colors[(int) ImGuiCol.ButtonActive] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.ButtonHovered] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            style.Colors[(int) ImGuiCol.Button] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.CheckMark] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            style.Colors[(int) ImGuiCol.ChildBg] = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            style.Colors[(int) ImGuiCol.FrameBgActive] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.FrameBgHovered] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            style.Colors[(int) ImGuiCol.FrameBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.HeaderActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            style.Colors[(int) ImGuiCol.HeaderHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.Header] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.PlotHistogramHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
            style.Colors[(int) ImGuiCol.PlotHistogram] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            style.Colors[(int) ImGuiCol.PlotLinesHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
            style.Colors[(int) ImGuiCol.PlotLines] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            style.Colors[(int) ImGuiCol.PopupBg] = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            style.Colors[(int) ImGuiCol.ResizeGripActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            style.Colors[(int) ImGuiCol.ResizeGripHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.ResizeGrip] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int) ImGuiCol.ScrollbarBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.ScrollbarGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            style.Colors[(int) ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            style.Colors[(int) ImGuiCol.ScrollbarGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            style.Colors[(int) ImGuiCol.SliderGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            style.Colors[(int) ImGuiCol.SliderGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            style.Colors[(int) ImGuiCol.TextDisabled] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            style.Colors[(int) ImGuiCol.TextSelectedBg] = new Vector4(0.25f, 1.00f, 0.00f, 0.43f);
            style.Colors[(int) ImGuiCol.TitleBgActive] = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            style.Colors[(int) ImGuiCol.TitleBgCollapsed] = new Vector4(1.00f, 0.98f, 0.95f, 0.75f);
            style.Colors[(int) ImGuiCol.TitleBg] = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            style.Colors[(int) ImGuiCol.WindowBg] = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            style.Colors[(int) ImGuiCol.Text] = new Vector4(0.80f, 0.80f, 0.83f, 1.00f);
        }
    }
}