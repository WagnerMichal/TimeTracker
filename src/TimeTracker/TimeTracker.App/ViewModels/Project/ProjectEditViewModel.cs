using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectUserEditMessage>, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectUserDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _projectFacade.SaveAsync(Project with { Users = default! });

        messengerService.Send(new ProjectEditMessage { ProjectId = Project.ID });

        _navigationService.SendBackButtonPressed();
    }

    public async void Receive(ProjectUserEditMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectUserAddMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectUserDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        Project = await _projectFacade.GetAsync(Project.ID)
                 ?? ProjectDetailModel.Empty;
    }
}