using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.User;

public partial class UserListView
{
	public UserListView(UserListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}