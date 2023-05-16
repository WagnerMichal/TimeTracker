using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
//using TimeTracker.App.Resources.Texts;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.App.ViewModels;
using System.Diagnostics;
using TimeTracker.App.Services.Interfaces;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserDetailViewModel : ViewModelBase, IRecipient<UserEditMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; private set; }

    public UserDetailViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = await _userFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            try
            {
                await _userFacade.DeleteAsync(User.ID);
                messengerService.Send(new UserDeleteMessage());
                _navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await _alertService.DisplayAsync("Delete error"/*UserDetailViewModelTexts.DeleteError_Alert_Title*/, "error message"/*UserDetailViewModelTexts.DeleteError_Alert_Message*/);
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (User is not null)
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(UserEditViewModel.User)] = User });
        }
    }
    
    [RelayCommand]
    private async Task GoToCreateTaskAsync()
    {
        if (User is not null)
        {

            await _navigationService.GoToAsync("//activities/edit",
                new Dictionary<string, object?> { [nameof(ActivityEditViewModel.UserId)] = User.ID });
        }
    }

    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.ID)
        {
            await LoadDataAsync();
        }
    }
}
