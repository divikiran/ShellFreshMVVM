using System;
using FreshMvvm;
using Xamarin.Forms;

namespace ShellFreshh
{
    public class MyShellViewModel : FreshBasePageModel
    {
        public MyShellViewModel()
        {
           
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
    }
}

