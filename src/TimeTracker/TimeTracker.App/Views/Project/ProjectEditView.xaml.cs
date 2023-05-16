using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Project;

public partial class ProjectEditView
{
	public ProjectEditView(ProjectEditViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}