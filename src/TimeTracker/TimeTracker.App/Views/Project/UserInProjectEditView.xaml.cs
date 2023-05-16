using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views.Project;

public partial class UserInProjectEditView 
{
    public UserInProjectEditView(UserInProjectEditViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }
}