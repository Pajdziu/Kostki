﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kostki.ViewModels
{
    public class MainMenuItemModel
    {

        public ObservableCollection<ItemViewModel> Items1 { get; private set; }

        public MainMenuItemModel()
        {
            this.Items1 = new ObservableCollection<ItemViewModel>();
        }

        public void LoadData()
        {
            this.Items1.Add(new ItemViewModel() { firstItem = "Zagraj w gre", secondItem = "Prosty Poziom", color = "Red" });
            this.Items1.Add(new ItemViewModel() { firstItem = "Zagraj w gre", secondItem = "Trudny Poziom", color = "Green" });
        }
    }
}