﻿using System.Windows;
using System.Windows.Controls;
using TimeTracker.Models;

namespace TimeTracker.Helpers.Models
{
    public class ProjectTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EditableTemplate { get; set; }
        public DataTemplate ReadOnlyTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ProjectListBox project)
            {
                return project.IsEditable ? EditableTemplate : ReadOnlyTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}