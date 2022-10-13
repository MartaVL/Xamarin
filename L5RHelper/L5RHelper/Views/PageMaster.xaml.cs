
using L5RHelper.ViewsModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace L5RHelper
{
    public partial class PageMaster : ContentPage
    {
        public PageMaster()
        {
            InitializeComponent();


            BindingContext = new PageMasterViewModel(); 


        }
    }
}