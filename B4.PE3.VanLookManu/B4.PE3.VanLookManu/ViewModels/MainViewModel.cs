using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private readonly IUserDataService userData;
        public MainViewModel(IUserDataService userDataService)
        {
            this.userData = userDataService;
        }

        #region - Properties -
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged(nameof(User));
            }
        }
        #endregion

        #region - Commands -
        public ICommand OpenUserLocationsCommand => new Command<ItemTappedEventArgs>(
          async (ItemTappedEventArgs args) =>
          {
              await CoreMethods.PushPageModel<LocationsViewModel>((args.Item as User), false, true);
          });

        public ICommand RegisterUserCommand => new Command(
           async () =>
           {
               await CoreMethods.PushPageModel<RegisterUserViewModel>();

           });
        #endregion

        #region - Overrides -
        public async override void Init(object initData)
        {
            base.Init(initData);
            await RefreshUsers();
        }
        public async override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            await RefreshUsers();
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshUsers();
        }
        #endregion

        private async Task RefreshUsers()
        {
            try
            {
                var users = await userData.GetUsersAsync();
                if (users.Count == 0)
                {
                    await EnsureData();
                }
                else
                {
                    Users = null;
                    Users = new ObservableCollection<User>(users.OrderBy(u => u.Id));
                }
            }
            catch (Exception ex)
            {

                await CoreMethods.DisplayAlert("ow ****", ex.InnerException.Message, "error");
            }

        }

        private async Task EnsureData()
        {
            if (Users == null)
            {
                var user = new User
                {
                    Name = "cig",
                    Id = 0
                };
                var users = new List<User>();
                users.Add(user);
                Users = null;
                Users = new ObservableCollection<User>(users.OrderBy(u => u.Name));
                await userData.CreateUser(user);
            }
        }

    }
}
