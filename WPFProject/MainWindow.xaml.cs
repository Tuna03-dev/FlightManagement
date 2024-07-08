﻿using BussinessObjects;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFProject2;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AccountMember accountMember;
        public MainWindow(AccountMember account)
        {   
            this.accountMember = account;
            InitializeComponent();
            AuthorizationUser(account);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to logout?", "Logout!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Windows[0].Show();
            
            
        }

        private void btnManageAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AccountWindow accountWindow = new AccountWindow(accountMember);
            accountWindow.Show();
            
        }

        private void AuthorizationUser(AccountMember account)
        {
            if(account.Role.Equals("Admin"))
            {
                ((Button)btnManageAccount).Visibility = Visibility.Visible;
                ((Button)btnCreateFlight).Visibility = Visibility.Visible;
                ((Button)btnManageAirLine).Visibility = Visibility.Visible;
                ((Button)btnManageAirport).Visibility = Visibility.Visible;
                ((Button)btnManagePlatform).Visibility = Visibility.Visible;
            }
            else
            {
                ((Button)btnManageAccount).Visibility = Visibility.Collapsed;
                ((Button)btnCreateFlight).Visibility = Visibility.Collapsed;
                ((Button)btnManageAirLine).Visibility = Visibility.Collapsed;
                ((Button)btnManageAirport).Visibility = Visibility.Collapsed;
                ((Button)btnManagePlatform).Visibility = Visibility.Collapsed;

            }
        }

        private void btnManageBooking_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
        }

        private void btnCreateFlight_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateFlightWindow createFlightWindow = new CreateFlightWindow();
            createFlightWindow.Show();
        }

        private void btnManagePassenger_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManagePassengerWindow managePassengerWindow = new ManagePassengerWindow();
            managePassengerWindow.Show();
        }

        private void btnManageAirport_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageAirportWindow manageAirportWindow = new ManageAirportWindow();
            manageAirportWindow.Show();
        }

        private void btnManageAirLine_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageAirlineWindow manageAirlineWindow = new ManageAirlineWindow();
            manageAirlineWindow.Show();
        }

        private void btnManagePlatform_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManagePlatformWindow managePlatformWindow = new ManagePlatformWindow();
            managePlatformWindow.Show();
        }
    }
}