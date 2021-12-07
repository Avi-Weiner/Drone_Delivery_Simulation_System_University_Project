﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneLIst.xaml
    /// </summary>
    public partial class DroneLIst : Window
    {
        public DroneLIst(IBL.IBL BLObj)
        {
            
            InitializeComponent();
            DroneLIstView.ItemsSource = BL.BL.BLObject.BLDroneList;
        }
        
        private void DroneLIstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeComponent();
            DroneLIstView.ItemsSource = BL.BL.BLObject.BLDroneList;
        }
    }
}
