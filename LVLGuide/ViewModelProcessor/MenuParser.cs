using System;
using System.Collections;
using System.Reflection;
using ImGuiNET;
using LVLGuide.ViewModelProcessor.Nodes;

namespace LVLGuide.ViewModelProcessor
{
    public static class MenuParser
    {
        public static MenuHolder Parse(IMenu settings)
        {
            var nextAvailableKey = -2;

            return Parse(settings, new MenuConfig("", false), ref nextAvailableKey);
        }

        private static MenuHolder Parse(object menu, MenuConfig config, ref int nextAvailableKey)
        {
            if (menu == null)
            {
                throw new Exception("Cant parse null settings.");
            }

            var holder = new MenuHolder
            {
                Name = config.Name,
                ID = nextAvailableKey--,
                SameLine = config.SameLine
            };

            var props = menu.GetType().GetProperties();
            if (!HandleType(holder, menu))
            {
                foreach (var property in props)
                {
                    if (property.Name == "Enable") continue;
                    var propertyValue = property.GetValue(menu);
                    if (propertyValue == null) continue;

                    var menuConfig = new MenuConfig(property.GetCustomAttribute<HideNameAttribute>() != null ? "" : property.Name, 
                                                          property.GetCustomAttribute<SameLineAttribute>() != null);
                    ParseProperty(ref nextAvailableKey, propertyValue, menuConfig, holder);
                }
            }

            return holder;
        }

        private static void ParseProperty(ref int nextAvailableKey, object propertyValue, MenuConfig config, MenuHolder holder)
        {
            if (propertyValue is IMenu innerMenu)
            {
                holder.Children.Add(Parse(innerMenu, new MenuConfig(config.Name, false), ref nextAvailableKey));
                return;
            }

            if (propertyValue is IEnumerable enumerable)
            {
                foreach (var element in enumerable)
                {
                    ParseProperty(ref nextAvailableKey, element, new MenuConfig("", false), holder);
                }
                return;
            }
            holder.Children.Add(Parse(propertyValue, config, ref nextAvailableKey));
        }

        private static bool HandleType(MenuHolder holder, object? type)
        {
            switch (type)
            {
                case ArrowButtonNode arrowButtonNode:
                    holder.DrawDelegate = () =>
                    {
                        if (ImGui.ArrowButton(holder.Unique, arrowButtonNode.Direction))
                        {
                            arrowButtonNode.OnPressed();
                        }
                    };
                    return true;
                case ButtonNode buttonNode:
                    holder.DrawDelegate = () =>
                    {
                        if (ImGui.Button(holder.Unique))
                        {
                            buttonNode.OnPressed();
                        }
                    };
                    return true;
                case null:
                    holder.DrawDelegate = () => { };
                    return true;
                case ToggleNode toggleNode:
                    holder.DrawDelegate = () =>
                    {
                        var isChecked = toggleNode.Value;
                        ImGui.Checkbox(holder.Unique, ref isChecked);
                        toggleNode.Value = isChecked;
                    };
                    return true;
                case LabelNode labelNode:
                    holder.DrawDelegate = () =>
                    {
                        ImGui.Text(labelNode.Text);
                    };
                    return true;
                case ProgressBarNode progressBarNode:
                    holder.DrawDelegate = () =>
                    {
                        ImGui.ProgressBar(progressBarNode.Fraction);
                    };
                    return true;
                default:
                    return false;
            }
        }
    }
}