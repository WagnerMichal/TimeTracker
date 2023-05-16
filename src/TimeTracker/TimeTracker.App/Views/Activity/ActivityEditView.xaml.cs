using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Activity;

public partial class ActivityEditView
{
    public ActivityEditView(ActivityEditViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}