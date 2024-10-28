﻿using System.Windows;
using System.Windows.Controls;
using TimerTracker.Models;

namespace TimerTracker.Helpers.Models
{
    public class SubModuleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EditableTemplate { get; set; }
        public DataTemplate ReadOnlyTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SubModuleListBox project)
            {
                return project.IsEditable ? EditableTemplate : ReadOnlyTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}