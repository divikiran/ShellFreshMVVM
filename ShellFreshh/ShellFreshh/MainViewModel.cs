using System;
using FreshMvvm;

namespace ShellFreshh
{
    public class MainViewModel : FreshBasePageModel
    {
        public MainViewModel()
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
