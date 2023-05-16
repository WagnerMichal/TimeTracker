using TimeTracker.App.Models;
using TimeTracker.App.Services;
using TimeTracker.App.ViewModels;
using TimeTracker.App.Views.Activity;
using TimeTracker.App.Views.Project;
using TimeTracker.App.Views.User;
using TimeTracker.App.Services.Interfaces;

namespace TimeTracker.App.Services
{
    public class NavigationService : INavigationService
    {
        public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
        {
            new("//users", typeof(UserListView), typeof(UserListViewModel)),
            new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),
            new("//users/detail", typeof(UserDetailView), typeof(UserDetailViewModel)),

            new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
            new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
            new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),
            new("//projects/detail/users", typeof(UserInProjectEditView), typeof(UserInProjectEditViewModel)),

            new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
            new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
            new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        };

        public async Task GoToAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            var route = GetRouteByViewModel<TViewModel>();
            await Shell.Current.GoToAsync(route);
        }
        public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
            where TViewModel : IViewModel
        {
            var route = GetRouteByViewModel<TViewModel>();
            await Shell.Current.GoToAsync(route, parameters);
        }

        public async Task GoToAsync(string route)
            => await Shell.Current.GoToAsync(route);

        public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
            => await Shell.Current.GoToAsync(route, parameters);

        public bool SendBackButtonPressed()
            => Shell.Current.SendBackButtonPressed();

        private string GetRouteByViewModel<TViewModel>()
            where TViewModel : IViewModel
            => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
    }
}
