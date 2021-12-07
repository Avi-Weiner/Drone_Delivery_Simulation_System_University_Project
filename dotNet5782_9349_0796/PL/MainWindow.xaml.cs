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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL BLObj;
        public MainWindow()
        {
            InitializeComponent();
            BLObj = new BL.BL();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        //private void DroneList_Click(object sender, RoutedEventArgs e)
        //{
        //    DroneDisplay DroneDisplayWindow = new DroneDisplay();
        //    DroneDisplayWindow.Show();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneLIst DroneLIstWindow = new DroneLIst(BLObj);
            DroneLIstWindow.Show();
        }
    }
}