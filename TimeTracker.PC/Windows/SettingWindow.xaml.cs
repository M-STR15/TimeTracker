using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.PC.Helpers;
using TimeTracker.PC.Models;
using TimeTracker.PC.Services;
using TimeTracker.PC.Stories;

namespace TimeTracker.PC.Windows
{
	[ObservableObject]
	public partial class SettingWindow : Window
	{
		private readonly MainStory _mainStory;
		private readonly ProjectRepository _projectProvider;

		private EventLogService _eventLogService;
		private ProjectListBox _selectProjectListBox;
		private List<string> PositionList = new();

		public SettingWindow(MainStory mainStory)
		{
			_eventLogService = new EventLogService();
			try
			{
				_mainStory = mainStory;
				_projectProvider = _mainStory.ContainerStore.GetProjectProvider();

				InitializeComponent();

				ProjectListBox = new ObservableCollection<ProjectListBox>();
				SubModuleListBox = new ObservableCollection<SubModuleListBox>();

				loadProjects();
				setProjectItemsView();
				setSubModulesItemsView();

				CmdProjectSave = new RelayCommand(onSaveProject_Click);
				CmdSubModuleSave = new RelayCommand(onSaveSubModule_Click);

				DataContext = this;

				PositionList.Add("H:Left-V:Top");
				PositionList.Add("H:Left-V:Center");
				PositionList.Add("H:Left-V:Bottom");

				PositionList.Add("H:Center-V:Top");
				PositionList.Add("H:Center-V:Center");
				PositionList.Add("H:Center-V:Bottom");

				PositionList.Add("H:Rght-V:Top");
				PositionList.Add("H:Right-V:Center");
				PositionList.Add("H:Right-V:Bottom");

				cmbPositionList.ItemsSource = PositionList;
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("333f942b-9655-4e66-a8ca-7b06bc5db7c4"), ex, "Problém při otvírání setting okna.");
			}
		}

		public ICollection<SubModuleListBox> SubModuleListBox { get; set; }

		private async void loadProjects()
		{
			var list = await _projectProvider.GetProjectsAsync();
			ProjectListBox = new ObservableCollection<ProjectListBox>(list.Select(x => new ProjectListBox(x)).ToList());
		}

		private async void onActionAfterChangeItemInListProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				var selected = (ProjectListBox)ProjectItemsView.CurrentItem;

				if (selected != null)
				{
					var list = await _projectProvider.GetSubModulesAsync(selected.Id);

					if (list != null)
					{
						SubModuleListBox.Clear();
						foreach (var item in list.Select(x => new SubModuleListBox(x)).ToList())
						{
							SubModuleListBox.Add(item);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("0cdee520-445c-42a9-8039-d1fe128bae1d"), ex, "Problém při přepnutí projectu.");
			}
		}

		private void onAddProject_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var name = $"New Project({ProjectListBox.Count + 1})";
				var item = new ProjectListBox(name, "");
				item.IsEditable = true;

				ProjectListBox.Add(item);
				ProjectItemsView.Refresh();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("83bfd991-0549-4094-ab5d-625688ded5d9"), ex, "Problém s přidáním projektu.");
			}
		}

		private void onAddSubModule_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var name = $"New SubModule{(SubModuleListBox.Count + 1)}";
				var selectProjectId = ((ProjectListBox)ProjectItemsView.CurrentItem).Id;
				var item = new SubModuleListBox(0, name, "", selectProjectId);
				item.IsEditable = true;

				SubModuleListBox.Add(item);
				SubModuleItemsView.Refresh();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("6828bf3f-dd9b-453c-a9e9-9709ce87f523"), ex, "Problém s vytvořením sub modulu.");
			}
		}

		private void onDeleteProject_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selected = (ProjectListBox)ProjectItemsView.CurrentItem;
				if (selected != null)
				{
					var result = _projectProvider.DeleteProjectAsync(selected);

					if (result != null)
					{
						var item = ProjectListBox.First(x => x.GuidId == selected.GuidId);
						ProjectListBox.Remove(selected);
						ProjectItemsView.Refresh();
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("c4e38573-382d-4b9f-bf04-e11c13e33c63"), ex, "Problém s smazání projektu.");
			}
		}

		private void onDeleteSubModule_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
				if (selected != null)
				{
					var result = _projectProvider.DeleteSubModuleAsync(selected);
					if (result != null)
					{
						var item = ProjectListBox.FirstOrDefault(x => x.GuidId == selected.GuidId);
						SubModuleListBox.Remove(selected);
						SubModuleItemsView.Refresh();
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("bbb177d5-b2d0-41fc-aa15-8bc3154eda9c"), ex, "Problém s smazání sub modulu.");
			}
		}

		private void onEditProject_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selected = (ProjectListBox)ProjectItemsView.CurrentItem;

				if (selected != null)
				{
					selected.IsEditable = true;
					ProjectItemsView.Refresh();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("5609f4e2-7502-4473-9ecb-b5636356a1c3"), ex, "Problém s editací projektu.");
			}
		}

		private void onEditSubModule_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
				if (selected != null)
				{
					selected.IsEditable = true;
					SubModuleItemsView.Refresh();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("4064c4c9-2326-43c6-bfa0-a855e9e7c4c0"), ex, "Problém s editací sub modulu.");
			}
		}

		private void onProjectItemsViewChangeHandler(object sender, EventArgs args) => setLblProjectInfo();

		private void onSaveProject_Click(object parameter)
		{
			try
			{
				var item = (ProjectListBox)parameter;
				item.IsEditable = false;

				var result = _projectProvider.SaveProjectAsync(item);
				if (result != null)
				{
					var updateItem = ProjectListBox.FirstOrDefault(x => x.GuidId == item.GuidId);
					if (updateItem != null)
					{
						updateItem.Id = result.Id;
						ProjectItemsView.Refresh();
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("f6e161c0-6406-4c08-b6d7-266f165788ef"), ex, "Problém s uložením projektu.");
			}
		}

		private void onSaveSubModule_Click(object parameter)
		{
			try
			{
				var item = (SubModuleListBox)parameter;
				item.IsEditable = false;

				var result = _projectProvider.SaveSubModuleAsync(item);
				if (result != null)
				{
					var updateItem = SubModuleListBox.FirstOrDefault(x => x.GuidId == item.GuidId);
					if (updateItem != null)
					{
						updateItem.Id = result.Id;
						ProjectItemsView.Refresh();
					}
				}

				SubModuleItemsView.Refresh();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("1043e657-45cd-4b06-8aec-41ce850fdca5"), ex, "Problém s uložením sub modulu.");
			}
		}

		private void onSubModuleItemsViewChangeHandler(object sender, EventArgs args) => setLblSubModuleInfo();

		private void setLblProjectInfo() => lblProjectInfo.Content = ProjectListBox.Count();

		private void setLblSubModuleInfo() => lblSubModuleInfo.Content = SubModuleListBox.Count();

		private void setProjectItemsView()
		{
			ProjectItemsView = CollectionViewSource.GetDefaultView(ProjectListBox);
			//ProjectItemsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
			//ProjectItemsView.CustomSort = new IdSorter();
			ProjectItemsView.Refresh();

			ProjectItemsView.CollectionChanged += onProjectItemsViewChangeHandler;

			setLblProjectInfo();
		}

		private void setSubModulesItemsView()
		{
			SubModuleItemsView = CollectionViewSource.GetDefaultView(SubModuleListBox);
			//SubModuleItemsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
			SubModuleItemsView.Refresh();

			SubModuleItemsView.CollectionChanged += onSubModuleItemsViewChangeHandler;

			setLblSubModuleInfo();
		}

		#region MVVM

		public SubModuleListBox _selectSubModuleListBox;
		public ICommand CmdProjectSave { get; }
		public ICommand CmdSubModuleSave { get; }
		public ICollectionView ProjectItemsView { get; set; }

		public ICollection<ProjectListBox> ProjectListBox { get; set; }

		public ProjectListBox SelectProjectListBox
		{
			get => _selectProjectListBox;
			set
			{
				if (_selectProjectListBox != value)
				{
					_selectProjectListBox = value;
					OnPropertyChanged();

					var isSelected = (value != null);
					btnProjectEdit.IsEnabled = isSelected;
					btnProjectDelete.IsEnabled = isSelected;
					btnSubModuleAdd.IsEnabled = isSelected && !SelectProjectListBox.IsEditable;
				}
			}
		}

		public SubModuleListBox SelectSubModuleListBox
		{
			get => _selectSubModuleListBox;
			set
			{
				if (_selectSubModuleListBox != value)
				{
					_selectSubModuleListBox = value;
					OnPropertyChanged();

					var isSelected = (value != null);
					btnSubModuleDelete.IsEnabled = isSelected;
					btnSubModuleEdit.IsEnabled = isSelected;
				}
			}
		}

		public ICollectionView SubModuleItemsView { get; set; }

		#endregion MVVM
	}
}