﻿using MaterialDesignThemes.Wpf.Transitions;
using ResidentApp.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteInerfaces.Services.AdminServices;
using RemoteInerfaces.Entities;
using System.Runtime.CompilerServices;

namespace ResidentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static IAdminRoomService adminRoomService;
        public static IAdminDeviceService adminDeviceService = (IAdminDeviceService)Activator.GetObject(typeof(IAdminDeviceService), "tcp://localhost:8080/AdminDeviceService");
        SlideWipe SlideWipeLeft = new SlideWipe() { Direction = SlideDirection.Left, Duration = TimeSpan.FromSeconds(0.5) };
        SlideWipe SlideWipeRight = new SlideWipe() { Direction = SlideDirection.Right, Duration = TimeSpan.FromSeconds(0.5) };
        public List<RoomDevice> roomDevices { get; set; }
        public ObservableCollection<UserControls.Slider> Rooms { get; set; }
        public List<RoomWithDevieces> rooms;
        public ObservableCollection<ComboBoxItem> ComboBoxRoomsList { get; set; }

        public MainWindow()
        {
            adminRoomService = (IAdminRoomService)Activator.GetObject(typeof(IAdminRoomService), "tcp://localhost:8080/AdminRoomService");
            //adminDeviceService = 
            initializeRoomSliders();



            InitializeComponent();
            //DataContext = this;
            Loaded += MainWindow_Loaded;
            RoomCombobox.SelectedIndex = 0;
            if (Rooms.Count() == 0)
            {
                noRoomAvailable.Visibility = Visibility.Visible;
                RoomsAvailable.Visibility = Visibility.Collapsed;
            }
            else
            {
                noRoomAvailable.Visibility = Visibility.Collapsed;
                RoomsAvailable.Visibility = Visibility.Visible;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;

        }

        private void initializeRoomSliders()
        {

            rooms = adminRoomService.getResidentRooms(2);
            Rooms = new ObservableCollection<UserControls.Slider>();
            ComboBoxRoomsList = new ObservableCollection<ComboBoxItem>();

            foreach (RoomWithDevieces room in rooms)
            {
                UserControls.Slider slider = new UserControls.Slider();
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                ObservableCollection<RoomDevice> roomDevices = new ObservableCollection<RoomDevice>();
                int deviceIndex = 0;
                foreach (Device device in room.room_devices)
                {
                    RoomDevice roomDevice = new RoomDevice();
                    roomDevice.Title = device.descriptive_name;
                    roomDevice.Device_ID = device.id_device;
                    roomDevice.ColumnGrid = deviceIndex % 4;
                    roomDevice.RowGrid = deviceIndex < 4 ? 0 : 1;
                    roomDevice.ImageOffSource = $"/Images/{device.type}_off.png";
                    roomDevice.ImageOnSource = $"/Images/{device.type}_on.png";
                    deviceIndex++;
                    roomDevices.Add(roomDevice);
                }
                slider.Title = room.room.descriptive_name;
                slider.ImageSourceAsBytes = room.room.background_image;
                //roomDevices.Add(new RoomDevice() { Title = "Temperature And Humidity", ColumnGrid = 0, RowGrid = 0, ImageOffSource = "/Images/Temperature_Humidity_off.png", ImageOnSource= "/Images/Temperature_Humidity_on.png" });
                //roomDevices.Add(new RoomDevice() { Title = "Camera", ColumnGrid = 1, RowGrid = 0, ImageOffSource = "/Images/Camera_off.png", ImageOnSource = "/Images/Camera_on.png" });
                //roomDevices.Add(new RoomDevice() { Title = "TV", ColumnGrid = 2, RowGrid = 0, ImageOffSource = "/Images/TV_off.png", ImageOnSource = "/Images/TV_on.png" });

                slider.RoomDevicesList = roomDevices;
                slider.BackwardWipe = SlideWipeLeft;
                slider.ForwardWipe = SlideWipeRight;
                slider.RoomID = "Room" + room.room.id_room.ToString();

                comboBoxItem.Content = room.room.descriptive_name;
                comboBoxItem.Uid = Rooms.Count().ToString();
                comboBoxItem.Padding = new Thickness(20);
                Rooms.Add(slider);
                ComboBoxRoomsList.Add(comboBoxItem);
            }


        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private byte[] roomBackgroundImage;
        private string fullPathImage;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (myTransitioner.SelectedIndex < Rooms.Count - 1)
                myTransitioner.SelectedIndex += 1;
            //page1.Visibility = Visibility.Hidden;
            RoomCombobox.SelectedValue = myTransitioner.SelectedIndex.ToString();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (myTransitioner.SelectedIndex > 0)
                myTransitioner.SelectedIndex -= 1;
            RoomCombobox.SelectedValue = myTransitioner.SelectedIndex.ToString();
        }

        private void RoomCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)RoomCombobox.SelectedItem;
            int value = Convert.ToInt32(typeItem.Uid.ToString());
            if (value != myTransitioner.SelectedIndex)
                myTransitioner.SelectedIndex = value;

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }


    }
    public class ComboBoxRoomItem
    {
        public string Uid { get; set; }
        public string RoomTitle { get; set; }
    }
}
