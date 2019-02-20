using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace ShellFreshh
{
    public class FreshShellPage : Shell, IFreshNavigationService
    {
        List<Page> _shellItem = new List<Page>();
        public IEnumerable<Page> ShellItems { get { return _shellItem; } }

        public FreshShellPage()
        {
            this.Icon = "3Bar";
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
            this.Route = "ShellFreshh";
            this.RouteHost = "microsoft.com";
            this.RouteScheme = "App";
        }

        public FreshShellPage(Page page)
            : this(page, Constants.DefaultNavigationServiceName)
        {
            this.Icon = "3Bar";
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
            this.Route = "ShellFreshh";
            this.RouteHost = "microsoft.com";
            this.RouteScheme = "App";
        }

        public FreshShellPage(Page page, string navigationPageName)

        {
            var pageModel = page.GetModel();
            if (pageModel == null)
                throw new InvalidCastException("BindingContext was not a FreshBasePageModel on this Page");

            pageModel.CurrentNavigationServiceName = navigationPageName;
            NavigationServiceName = navigationPageName;
            RegisterNavigation();

            var shellPage = page as Shell;
            if (shellPage != null)
            {
                //this._shellItem.Add(shellPage);
                foreach (ShellItem shellItem in shellPage.Items)
                {
                    var c = shellItem.Items;
                    foreach (ShellSection cc in c)
                    {
                        ShellContentCollection scc = cc.Items;
                        foreach (ShellContent sitem in scc)
                        {
                            var shellContentPage = sitem.Content as Page;
                            if (shellContentPage == null)
                                break;

                            //var vm = shellContentPage.GetModel();
                            //if (vm == null)
                            //break;

                            var name = GetTypeByPageName(shellContentPage.GetType());
                            var pageType = Type.GetType(name);
                            if (pageType == null)
                                throw new Exception(name + " not found");
                            var vm = (FreshBasePageModel)FreshIOC.Container.Resolve(pageType);



                            var bindedPage = FreshPageModelResolver.ResolvePageModel(vm.GetType(), null);
                            sitem.Content = bindedPage;
                            //sitem.BindingContext = vm;
                        }

                    }

                    //AddShell
                    this.Items.Add(shellItem);
                }
            }
        }


        public string GetTypeByPageName(Type pageModelType)
        {
            return pageModelType.AssemblyQualifiedName
                .Replace("Page", "ViewModel");
                //.Replace("View", "ViewModel");
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public string NavigationServiceName { get; private set; }

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
            _shellItem.Add(page);



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

            //if (_shellItem.Count == 1)
            //{
            //    this.CurrentItem = shellItem;
            //}
            return page;
        }
    }
}
