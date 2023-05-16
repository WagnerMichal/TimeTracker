using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels;

public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
