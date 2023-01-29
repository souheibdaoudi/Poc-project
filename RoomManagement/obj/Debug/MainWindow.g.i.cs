// Updated by XamlIntelliSenseFileGenerator 29/01/2023 20:22:49
#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "74CBFA509B18028B7422EE306A2732AB0EDA675A03522153567C1BA8F7598153"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
using RoomManagement;
using Sdl.MultiSelectComboBox.API;
using Sdl.MultiSelectComboBox.Behaviours;
using Sdl.MultiSelectComboBox.Controls;
using Sdl.MultiSelectComboBox.EventArgs;
using Sdl.MultiSelectComboBox.Services;
using Sdl.MultiSelectComboBox.Themes.Generic;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RoomManagement
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 160 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border newRoomForm;

#line default
#line hidden


#line 224 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptiveNameTextBox;

#line default
#line hidden


#line 235 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Sdl.MultiSelectComboBox.Themes.Generic.MultiSelectComboBox CustomMultiSelectComboBox;

#line default
#line hidden


#line 263 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock profileImagePath;

#line default
#line hidden


#line 289 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border noRoomAvailable;

#line default
#line hidden


#line 352 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RoomsAvailable;

#line default
#line hidden


#line 366 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border newDeviceForm;

#line default
#line hidden


#line 430 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptiveDeviceNameTextBox;

#line default
#line hidden


#line 451 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DeviceUrlTextBox;

#line default
#line hidden


#line 459 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox deviceTypeComboBox;

#line default
#line hidden


#line 559 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoomCombobox;

#line default
#line hidden


#line 628 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Transitions.Transitioner myTransitioner;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RoomManagement;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.window = ((RoomManagement.MainWindow)(target));
                    return;
                case 2:

#line 28 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);

#line default
#line hidden
                    return;
                case 3:
                    this.newRoomForm = ((System.Windows.Controls.Border)(target));
                    return;
                case 4:
                    this.descriptiveNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.CustomMultiSelectComboBox = ((Sdl.MultiSelectComboBox.Themes.Generic.MultiSelectComboBox)(target));

#line 236 "..\..\MainWindow.xaml"
                    this.CustomMultiSelectComboBox.Loaded += new System.Windows.RoutedEventHandler(this.MultiSelectComboBox_Loaded);

#line default
#line hidden
                    return;
                case 6:

#line 259 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.chooseProfileImageButtonClick);

#line default
#line hidden
                    return;
                case 7:
                    this.profileImagePath = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 8:

#line 275 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newRoomSave_Click);

#line default
#line hidden
                    return;
                case 9:

#line 282 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newRoomCancel_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.noRoomAvailable = ((System.Windows.Controls.Border)(target));
                    return;
                case 11:

#line 335 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewRoom_Click);

#line default
#line hidden
                    return;
                case 12:
                    this.RoomsAvailable = ((System.Windows.Controls.Grid)(target));
                    return;
                case 13:
                    this.newDeviceForm = ((System.Windows.Controls.Border)(target));
                    return;
                case 14:
                    this.descriptiveDeviceNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 15:
                    this.DeviceUrlTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 16:
                    this.deviceTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 17:

#line 481 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newDeviceSave_Click);

#line default
#line hidden
                    return;
                case 18:

#line 488 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newDeviceCancel_Click);

#line default
#line hidden
                    return;
                case 19:

#line 500 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newDeviceFormButton_click);

#line default
#line hidden
                    return;
                case 20:

#line 549 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PreviousButton_Click);

#line default
#line hidden
                    return;
                case 21:
                    this.RoomCombobox = ((System.Windows.Controls.ComboBox)(target));

#line 569 "..\..\MainWindow.xaml"
                    this.RoomCombobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RoomCombobox_SelectionChanged);

#line default
#line hidden
                    return;
                case 22:

#line 592 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextButton_Click);

#line default
#line hidden
                    return;
                case 23:

#line 609 "..\..\MainWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewRoom_Click);

#line default
#line hidden
                    return;
                case 24:
                    this.myTransitioner = ((MaterialDesignThemes.Wpf.Transitions.Transitioner)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window window;
    }
}
