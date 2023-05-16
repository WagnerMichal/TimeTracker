using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.User;

public partial class UserDetailView
{
    public UserDetailView(UserDetailViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}