using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Project;

public partial class ProjectListView
{
	public ProjectListView(ProjectListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}
