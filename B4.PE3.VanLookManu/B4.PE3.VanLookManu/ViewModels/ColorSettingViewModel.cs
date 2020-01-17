using B4.PE3.VanLookManu.Domain.Services;
using FreshMvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class ColorSettingViewModel : FreshBasePageModel
    {

        #region - Properties -
        private ObservableCollection<Color> colors;
        public ObservableCollection<Color> Colors
        {
            get { return colors; }
            set
            {
                colors = value;
                RaisePropertyChanged(nameof(Colors));
            }
        }
        #endregion

        #region - Overrides -
        public override void Init(object initData)
        {
            base.Init(initData);
            LoadColors();
        }
        #endregion

        #region - Commands -
        public ICommand SelectedColor => new Command<ItemTappedEventArgs>(
          async (ItemTappedEventArgs args) =>
          {
              await CoreMethods.PopPageModel((args.Item as Color?), false, true);
          });


        #endregion


        private void LoadColors()
        {
            ColorList colorlist = new ColorList();
            Colors = new ObservableCollection<Color>(colorlist.OrderBy(c => c.Name));

        }

    }
}
