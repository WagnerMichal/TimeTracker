using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Shells
{
    public partial class AppShell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToUsersAsync()
            => await _navigationService.GoToAsync<UserListViewModel>();

        [RelayCommand]
        private async Task GoToProjectAsync()
            => await _navigationService.GoToAsync<ProjectListViewModel>();

        [RelayCommand]
        private async Task GoToActivitiesAsync()
            => await _navigationService.GoToAsync<ActivityListViewModel>();
    }
}