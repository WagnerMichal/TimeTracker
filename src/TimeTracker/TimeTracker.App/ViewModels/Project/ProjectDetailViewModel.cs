using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectUserDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserInProjectFacade _userInProjectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid Id { get; set; }
    public ProjectDetailModel? Project { get; set; }
    
    public IEnumerable<UserInProjectListModel> Users { get; set; }

    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IUserInProjectFacade userInProjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _userInProjectFacade = userInProjectFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(Id);
        if (Project is not null)
        {
            Users = await _userInProjectFacade.GetAsyncByProjectId(Project.ID);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            await _projectFacade.DeleteAsync(Project.ID);

            messengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Project is not null)
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(ProjectEditViewModel.Project)] = Project with { } });
        }
    }
    
    [RelayCommand]
    private async Task GoToAddUsersAsync()
    {
        if (Project is not null)
        {
            await _navigationService.GoToAsync("//projects/detail/users",
                new Dictionary<string, object?> { [nameof(ProjectEditViewModel.Project)] = Project with { } });
        }
    }

    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.ID)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(ProjectUserAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectUserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}