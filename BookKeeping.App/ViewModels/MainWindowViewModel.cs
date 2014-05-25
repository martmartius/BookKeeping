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

namespace BookKeeping.App.ViewModels
{
    public class MainWindowViewModel : MainWindowViewModelBase
    {
        private bool _quitConfirmationEnabled;

        public MainWindowViewModel()
        {
            this.DisplayName = BookKeeping.App.Properties.Resources.Application_Name;
            this.QuitConfirmationEnabled = true;

            Workspaces.Add(new WorkspaceViewModel
            {
                DisplayName = "Empty workspace"
            });

            CollectionViewSource.GetDefaultView(Workspaces).MoveCurrentToFirst();
            Exit = ApplicationCommands.Close;
        }

        public ICommand AddProduct { get; set; }

        public ICommand CloseTabItem { get; set; }

        public ICommand Exit { get; set; }

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

            var fileNode = new MenuTreeNode(T("File"), null);
            fileNode.AddChild(new MenuTreeNode(T("ProductList"), new DelegateCommand(_ =>
            {
                var viewModel = CreateOrExistWorkspace<ProductListViewModel>();
                SetActiveWorkspace(viewModel);
            })));

            var editNode = new MenuTreeNode(T("Edit"), null);
            editNode.AddChild(new MenuTreeNode(T("Undo"), ApplicationCommands.Undo));
            editNode.AddChild(new MenuTreeNode(T("Redo"), ApplicationCommands.Redo));
            editNode.AddChild(new MenuTreeNode(T("Cut"), ApplicationCommands.Cut));
            editNode.AddChild(new MenuTreeNode(T("Copy"), ApplicationCommands.Copy));
            editNode.AddChild(new MenuTreeNode(T("Paste"), ApplicationCommands.Paste));
            editNode.AddChild(new MenuTreeNode(T("Find"), ApplicationCommands.Find));

            MainMenu.Add(fileNode);
            MainMenu.Add(editNode);
        }

    }
}