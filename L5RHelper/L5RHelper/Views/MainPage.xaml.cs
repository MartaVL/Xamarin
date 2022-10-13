using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace L5RHelper
{
    public partial class MainPage : ContentPage
    {       
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(checkMaster.IsChecked)
            {
                Navigation.PushAsync(new PageMaster());
            }
            else
            {
                Navigation.PushAsync(new PagePlayer());
            }                        
        }

    }
}
