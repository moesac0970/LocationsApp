using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using B4.PE3.VanLookManu.Domain.Services.Local;
using B4.PE3.VanLookManu.Domain.Services.Validator;
using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class RegisterUserViewModel : FreshBasePageModel
    {
        private IUserDataService UserDataService;
        private Validator validator;
        public RegisterUserViewModel()
        {
            UserDataService = new UserMockDataJson();
            this.validator = new Validator() ;

    }

    #region - Properties -
    private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region - Commands -
        public ICommand SaveUserCommand => new Command(
          async () =>
          {
              try
              {
                  var user = new User { Name = Name };

                  if(validator.ValidatorUser(user).Count == 0)
                  {
                      await UserDataService.CreateUser(user);
                      await CoreMethods.PopToRoot(true);
                      CoreMethods.RemoveFromNavigation();

                  }
                  else
                  {
                      await CoreMethods.DisplayAlert("Alert", "not everything filled in", "ok");
                  }

              }
              catch
              {
                  await CoreMethods.DisplayAlert("Error", "something went wrong", "Cancel");
              }
          });
        #endregion




    }
}
