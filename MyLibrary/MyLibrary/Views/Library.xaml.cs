using MyLibrary.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Library : ContentPage
    {
        public Library()
        {
            BindingContext = new LibraryViewModel();
            InitializeComponent();
        }
    }
}