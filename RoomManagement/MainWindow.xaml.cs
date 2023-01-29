using MaterialDesignThemes.Wpf.Transitions;
using RoomManagement.UserControls;
using RoomManagement.Models;
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

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LanguageItems model { get; set; }
        static IAdminRoomService adminRoomService;
        public static IAdminDeviceService adminDeviceService = (IAdminDeviceService) Activator.GetObject(typeof(IAdminDeviceService), "tcp://localhost:8080/AdminDeviceService");
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
            model = new LanguageItems();

            

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
             
            rooms = adminRoomService.getAdminRooms(1);
            Rooms = new ObservableCollection<UserControls.Slider>();
            ComboBoxRoomsList = new ObservableCollection<ComboBoxItem>();

            foreach (RoomWithDevieces room in rooms)
            {
                UserControls.Slider slider = new UserControls.Slider();
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                ObservableCollection<RoomDevice> roomDevices = new ObservableCollection<RoomDevice>();
                int deviceIndex = 0;
                foreach(Device device in room.room_devices)
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
                slider.RoomID = "Room"+room.room.id_room.ToString();

                comboBoxItem.Content = room.room.descriptive_name;
                comboBoxItem.Uid = Rooms.Count().ToString();
                comboBoxItem.Padding = new Thickness(20);
                Rooms.Add(slider);
                ComboBoxRoomsList.Add(comboBoxItem);
            }

            
        }
        
        private void reInitializeMultiSelectomboBox()
        {
            var GridChild = HelperClass.GetChildOfType<Grid>(CustomMultiSelectComboBox);
            Console.WriteLine("started");
            if (GridChild == null) return;
            Console.WriteLine("grid not null");
            var toggleButtonChild = HelperClass.GetChildOfType<ToggleButton>(GridChild);
            if (toggleButtonChild == null) return;
            Console.WriteLine("toggleButton not null");
            var contentControlChild = HelperClass.GetChildOfType<ContentControl>(toggleButtonChild);
            if (contentControlChild == null) return;
            Console.WriteLine("contentControl not null");
            var contentPresenterlChild = HelperClass.GetChildOfType<ContentPresenter>(contentControlChild);
            if (contentPresenterlChild == null) return;
            Console.WriteLine("contentPresenter not null");
            var borderChild = HelperClass.GetChildOfType<Border>(contentPresenterlChild);
            if (borderChild == null) return;
            Console.WriteLine("border not null");

            borderChild.BorderBrush = (Brush) new BrushConverter().ConvertFrom("#E1E6EB");
            borderChild.Background = Brushes.Transparent;
            borderChild.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borderChild.BorderThickness = new Thickness(1);
            Console.WriteLine("all is good");

        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.LineCount > 0)
            {
                textBox.ScrollToLine(textBox.LineCount - 1);
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

        public void newDeviceFormButton_click(object sender, RoutedEventArgs e)
        {
            newDeviceForm.Visibility = Visibility.Visible;
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

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;
            var tb = HelperClass.GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null) return;
            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;
            wm.Content = "Select Your BirthDate";
        }

        private void chooseProfileImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Please select your profile image";
            bool? responce = openFileDialog.ShowDialog();
            if (responce == true)
            {
                var fullPath = openFileDialog.FileName;
                profileImagePath.Text = fullPath;
                ImageBrush image = new ImageBrush();
                var fileSource = System.IO.Path.Combine(fullPath);
                BitmapImage bitmapImage = new BitmapImage(new Uri(fileSource));

                roomBackgroundImage = System.IO.File.ReadAllBytes(fullPath);


                image.ImageSource = bitmapImage;
                //ProfileImage.Fill = image;
                fullPathImage = fullPath;
            }
        }

        private void MultiSelectComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            reInitializeMultiSelectomboBox();
        }

        private void AddNewRoom_Click(object sender, RoutedEventArgs e)
        {
            newRoomForm.Visibility = Visibility.Visible;
        }
        
        private void newRoomCancel_Click(object sender, RoutedEventArgs e)
        {
            newRoomForm.Visibility = Visibility.Collapsed;
        }
        
        private void newRoomSave_Click(object sender, RoutedEventArgs e)
        {
            //Add to db
            Room newRoom = new Room();
            newRoom.id_house = 1;
            newRoom.descriptive_name = descriptiveNameTextBox.Text;
            newRoom.background_image = roomBackgroundImage;
            ObservableCollection<LanguageItem> items = (ObservableCollection<LanguageItem>)CustomMultiSelectComboBox.SelectedItems;
            List<RoomAccessors> roomAccessors = new List<RoomAccessors>();
            foreach(LanguageItem item in items)
            {
                RoomAccessors roomAccessor = new RoomAccessors();
                roomAccessor.id_account = item.Id;
                roomAccessors.Add(roomAccessor);
            }
            RoomWithDevieces newRoomWithDevieces = new RoomWithDevieces();
            newRoomWithDevieces.room = newRoom;
            rooms.Add(newRoomWithDevieces);
            adminRoomService.createNewRoom(newRoom, roomAccessors);


            Rooms.Add(
                new UserControls.Slider() { 
                    Title = descriptiveNameTextBox.Text,
                    ImageSourceAsBytes = roomBackgroundImage,
                    RoomDevicesList = new ObservableCollection<RoomDevice>(),
                    BackwardWipe = SlideWipeLeft,
                    ForwardWipe = SlideWipeRight 
                }
            );
            ComboBoxRoomsList.Add( 
                new ComboBoxItem() { 
                    Content = descriptiveNameTextBox.Text,
                    Uid = ComboBoxRoomsList.Count().ToString(),
                    Padding = new Thickness(20)
                }
            );
            newRoomForm.Visibility = Visibility.Collapsed;
            noRoomAvailable.Visibility = Visibility.Collapsed;
            RoomsAvailable.Visibility = Visibility.Visible;

        }

        private void newDeviceSave_Click(object sender, RoutedEventArgs e)
        {
            if (rooms[myTransitioner.SelectedIndex].room_devices.Count < 8)
            {
                RoomWithDevieces targetedRoom = rooms[myTransitioner.SelectedIndex];
                Device newDevice = new Device();
                newDevice.descriptive_name = descriptiveDeviceNameTextBox.Text;
                newDevice.id_room = targetedRoom.room.id_room;
                ComboBoxItem comboBoxItem = (ComboBoxItem) deviceTypeComboBox.SelectedItem;
                Console.WriteLine(comboBoxItem.Content.ToString());
                newDevice.type = comboBoxItem.Content.ToString();
                newDevice.url = DeviceUrlTextBox.Text;
                rooms[myTransitioner.SelectedIndex].room_devices.Add(newDevice);
                adminDeviceService.addNewDevice(newDevice);
                RoomDevice newRoomDevice = new RoomDevice();
                newRoomDevice.ColumnGrid = targetedRoom.room_devices.Count() % 4;
                newRoomDevice.RowGrid = targetedRoom.room_devices.Count() < 4 ? 0 : 1;
                newRoomDevice.Title = newDevice.descriptive_name;
                newRoomDevice.ImageOffSource = $"/Images/{newDevice.type}_off.png";
                newRoomDevice.ImageOnSource = $"/Images/{newDevice.type}_on.png";
                Rooms[myTransitioner.SelectedIndex].RoomDevicesList.Add(newRoomDevice);
            }
            newDeviceForm.Visibility = Visibility.Collapsed;

        }

        private void newDeviceCancel_Click(object sender, RoutedEventArgs e)
        {
            newDeviceForm.Visibility = Visibility.Collapsed;
        }
    }

    public class ComboBoxRoomItem
    {
        public string Uid { get; set; }
        public string RoomTitle { get; set; }
    }

    ////public class RoomDevice 
    //{
    //    public string Title { get; set; }
    //    public string ImageOffSource { get; set; }
    //    public string ImageOnSource { get; set; }
    //    public int ColumnGrid { get; set; }
    //    public int RowGrid { get; set; }


    //}
}
