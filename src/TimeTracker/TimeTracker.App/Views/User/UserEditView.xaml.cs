using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.User;

public partial class UserEditView
{
    public UserEditView(UserEditViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}