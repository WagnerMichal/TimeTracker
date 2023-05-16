using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
//using TimeTracker.App.Resources.Texts;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.App.ViewModels;
using System.Diagnostics;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityDetailViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IUserFacade _userFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid Id { get; set; }
    public ActivityDetailModel Activity { get; private set; }
    
    public string ProjectName { get; private set; }
    
    public string UserName { get; private set; }

    public ActivityDetailViewModel(
        IActivityFacade activityFacade,
        IUserFacade userFacade,
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _userFacade = userFacade;
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);
        
        if (Activity is not null)
        {
            ProjectDetailModel Project = await _projectFacade.GetAsync(Activity.ProjectID);
            if (Project is not null)
            {
                ProjectName = Project.Name;
            }
            UserDetailModel User = await _userFacade.GetAsync(Activity.UserID);
            if (User is not null)
            {
                UserName = User.Name + " " + User.LastName;
            }
        } 
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            try
            {
                await _activityFacade.DeleteAsync(Activity.ID);
                messengerService.Send(new ActivityDeleteMessage());
                _navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await _alertService.DisplayAsync("alert title"/*ActivityDetailViewModelTexts.DeleteError_Alert_Title*/, "error message"/*ActivityDetailViewModelTexts.DeleteError_Alert_Message*/);
            }
        }
    }
    
    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object> { [nameof(ActivityEditViewModel.Activity)] = Activity });
    }

    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Activity?.ID)
        {
            await LoadDataAsync();
        }
    }
}
