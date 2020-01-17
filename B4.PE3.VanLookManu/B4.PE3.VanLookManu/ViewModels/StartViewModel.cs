using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class StartViewModel : FreshBasePageModel
    {
        public ICommand OpenUsersCommand => new Command<ItemTappedEventArgs>(
         async (ItemTappedEventArgs args) =>
         {
             await CoreMethods.PushPageModel<MainViewModel>( false);
         });
    }
}
