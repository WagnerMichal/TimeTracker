using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Activity;

public partial class ActivityListView
{
    public ActivityListView(ActivityListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}