using CommunityToolkit.Mvvm.Input;
using System.Configuration;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(User), nameof(User))]
public partial class UserEditViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public UserDetailModel User { get; set; } = UserDetailModel.Empty;

    public UserEditViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _userFacade.SaveAsync(User with { Projects = default!, Activities = default! });

        messengerService.Send(new UserEditMessage { UserId = User.ID });

        _navigationService.SendBackButtonPressed();
    }

    public async void Receive(UserEditMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(UserAddMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        User = await _userFacade.GetAsync(User.ID)
                 ?? UserDetailModel.Empty;
    }
}