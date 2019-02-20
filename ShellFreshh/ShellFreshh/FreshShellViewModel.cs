using System;
using FreshMvvm;

namespace ShellFreshh
{
    public class FreshShellViewModel : FreshBasePageModel
    {
        public FreshShellViewModel()
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
