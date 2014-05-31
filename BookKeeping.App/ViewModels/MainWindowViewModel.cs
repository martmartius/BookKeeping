﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using ICommand = System.Windows.Input.ICommand;
using BookKeeping.UI.ViewModels;
using BookKeeping.UI;
using BookKeeping.Auth;
using BookKeeping.Domain.Repositories;
using BookKeeping.Domain.Aggregates;

namespace BookKeeping.App.ViewModels
{
    public class MainWindowViewModel : MainWindowViewModelBase
    {
        private bool _quitConfirmationEnabled;
        private IContextUserProvider _contextUserProvider;
        private readonly IRepository<User> _repostory;
        private long? _previousUserId;
        private bool _isWorkspacesVisible;

        public MainWindowViewModel()
        {
            this.QuitConfirmationEnabled = true;

            var userRepository = new UserRepository();
            _repostory = userRepository;

            var authService = new AuthenticationService(userRepository);
            SignIn = new SignInViewModel(authService);
            SignIn.AuthenticationSuccessful += AuthorizationSuccessful;

            Profile = new ProfileViewModel(authService, _contextUserProvider);
            Bind(Profile, t => t.IsAuthorization, IsAuthorization_PropertyChanged);

            MainMenu.Clear();
            BuildMainMenu();
        }

        void AuthorizationSuccessful(object sender, EventArgs e)
        {
            if (_contextUserProvider == null)
                _contextUserProvider = new ContextUserProvider((UserRepository)_repostory);

            Profile.Username = _contextUserProvider.ContextUser().Name;
            Profile.IsAuthorization = true;
            _previousUserId = _contextUserProvider.ContextUser().Id;
        }

        void IsAuthorization_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Profile.IsAuthorization)
            {
                Profile.ChangePassword = new ChangePasswordViewModel(_repostory);
                if (_contextUserProvider.ContextUser() != null
                    && _previousUserId != _contextUserProvider.ContextUser().Id)
                {
                    Workspaces.Clear();
                }
            }
        }

        public SignInViewModel SignIn { get; private set; }

        public ProfileViewModel Profile { get; private set; }

        public ICommand CloseLoginCmd { get; private set; }

        public ICommand CloseTabItem { get; set; }

        public ICommand Exit { get; set; }

        public ICommand ListOfProductsCmd { get; private set; }

        public ICommand ListOfCustomersCmd { get; private set; }

        public ICommand VendorListCmd { get; private set; }

        public ICommand SaleOfGoodsCmd { get; private set; }

        public ICommand PurchaseOfGoodsCmd { get; private set; }

        public ICommand SettingsCmd { get; private set; }

        public ICommand RemainsOfGoodsReportCmd { get; private set; }

        public ICommand HistoryOfGoodsReportCmd { get; private set; }

        public ICommand ChartsCmd { get; private set; }

        public ICommand SaveCmd { get; private set; }

        public ICommand PrintCmd { get; private set; }

        public bool IsWorkspacesVisible
        {
            get { return _isWorkspacesVisible; }
            set
            {
                _isWorkspacesVisible = value;
                OnPropertyChanged(() => IsWorkspacesVisible);
            }
        }

        public bool QuitConfirmationEnabled
        {
            get { return _quitConfirmationEnabled; }
            set
            {
                if (value.Equals(_quitConfirmationEnabled)) return;
                _quitConfirmationEnabled = value;
                OnPropertyChanged(() => QuitConfirmationEnabled);
            }
        }

        protected override void BuildMainMenu()
        {
            base.BuildMainMenu();

            //General
            PrintCmd = new DelegateCommand(_ => ((IPrintable)CurrentWorkspace).Print(), _ => CurrentWorkspace is IPrintable);
            Exit = ApplicationCommands.Close;
            SaveCmd = new DelegateCommand(_ => ((ISaveable)CurrentWorkspace).SaveChanges(), _ => CurrentWorkspace is ISaveable && ((ISaveable)CurrentWorkspace).CanSave);

            CloseLoginCmd = new DelegateCommand(t => { }, t => Profile.IsAuthorization);

            ListOfProductsCmd = new DelegateCommand(_ => SetActiveWorkspace(CreateOrRetrieveWorkspace<ProductListViewModel>()));
            SaleOfGoodsCmd = new DelegateCommand(_ => SetActiveWorkspace(CreateOrRetrieveWorkspace<OrderViewModel>()));

            // Reports
            ChartsCmd = new DelegateCommand(_ => SetActiveWorkspace(CreateOrRetrieveWorkspace<ChartsViewModel>()));
            RemainsOfGoodsReportCmd = new DelegateCommand(_ => SetActiveWorkspace(CreateOrRetrieveWorkspace<RemainsOfGoodsViewModel>()));

        }

        protected override void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnWorkspacesChanged(sender, e);
            IsWorkspacesVisible = Workspaces.Count > 0;
        }
    }
}