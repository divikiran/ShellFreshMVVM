using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace ShellFreshh
{
    public class FreshShellPage : Shell, IFreshNavigationService
    {
        List<Page> _tabs = new List<Page>();
        public IEnumerable<Page> TabbedPages { get { return _tabs; } }

        public FreshShellPage()
        {
            this.Icon = "3Bar";
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
            this.Route = "ShellFreshh";
            this.RouteHost = "microsoft.com";
            this.RouteScheme = "App";
        }

        public string NavigationServiceName => "ShellNavigation";

        public void NotifyChildrenPageWasPopped()
        {
            //foreach (var page in this.Items)
            //{
            //    if (page is ShellItem)
            //        ((ShellItem)page).NotifyAllChildrenPopped();
            //}
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                return this.CurrentItem.Navigation.PopModalAsync(animate);
            return this.CurrentItem.Navigation.PopAsync(animate);
        }

        public Task PopToRoot(bool animate = true)
        {
            return this.CurrentItem.Navigation.PopToRootAsync(animate);
        }

        public Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                return this.Navigation.PushModalAsync(CreateContainerPageSafe(page));
            return this.CurrentItem.Navigation.PushAsync(page);
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            //var page = _tabs.FindIndex(o => o.GetModel().GetType().FullName == typeof(T).FullName);

            //if (page > -1)
            //{
            //    CurrentPage = this.Children[page];
            //    var topOfStack = CurrentPage.Navigation.NavigationStack.LastOrDefault();
            //    if (topOfStack != null)
            //        return Task.FromResult(topOfStack.GetModel());

            //}
            return null;
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
            return new NavigationPage(page)
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.DarkGray
            };
        }

        public virtual Page AddShell<T>(string title, string route, string icon = null, object data = null) where T : FreshBasePageModel
        {
            Page page = FreshPageModelResolver.ResolvePageModel<T>(data);

            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            _tabs.Add(page);
            page.Title = title;
            if (icon != null)
            {
                page.Icon = icon;
            }
            var shellItem = new ShellItem
            {
                Title = title,
                Route = route,
                
            };
            var section = new ShellSection();
            var shellContent = new ShellContent
            {
                Content = page
            };
            section.Items.Add(shellContent);
            shellItem.Items.Add(section);
            this.Items.Add(shellItem);

            return page;
        }
    }
}
