using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Activity;

public partial class ActivityDetailView
{
    public ActivityDetailView(ActivityDetailViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}