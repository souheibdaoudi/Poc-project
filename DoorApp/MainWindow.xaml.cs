using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace DoorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, RemoteInerfaces.DoorRequestsInterface
    {
        Boolean isGust = false;
        int house_id = 1;
        Boolean requestWaiting = false;
        DispatcherTimer timer;
        DispatcherTimer timer2;
        DispatcherTimer timer3;
        DispatcherTimer timer4;
        HaarCascade faceDetected;
        Image<Bgr, Byte> frame;
        Capture camera;
        Image<Gray, byte> result;
        Image<Gray, byte> grayFace = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        string name = null;
        Account userAccount = null;

        public MainWindow()
        {
            InitializeComponent();
            getFaceIDFromDB();

        }

        public void getFaceIDFromDB()
        {
            faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                Connection cnn = new Connection();
                List<FaceID> trainingImagesList = cnn.getAccountFacesWithIDs();
                foreach (FaceID faceID in trainingImagesList)
                {
                    Image<Gray, byte> depthImage = new Image<Gray, byte>(240, 240);
                    depthImage.Bytes = faceID.face_id;
                    trainingImages.Add(depthImage);
                    labels.Add(faceID.id.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Nothing in the Database");
            }
        }

        private void FrameProcedure()
        {
            try
            {
                if (camera.QueryFrame() != null)
                {
                    frame = camera.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    grayFace = frame.Convert<Gray, Byte>();
                    MCvAvgComp[][] facesDetectedNow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));
                    foreach (MCvAvgComp f in facesDetectedNow[0])
                    {
                        result = frame.Copy(f.rect).Convert<Gray, Byte>().Resize(240, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        if (trainingImages.ToArray().Length != 0)
                        {
                            MCvTermCriteria termCriterias = new MCvTermCriteria(trainingImages.Count, 0.001);
                            EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 1500, ref termCriterias);
                            name = recognizer.Recognize(result);

                            // face saved on database 
                            if (!name.Equals(""))
                            {
                                timer2.Stop();
                                Connection cnn = new Connection();
                                userAccount = cnn.getAccount(Convert.ToInt32(name));
                                CheckedIcon.Visibility = Visibility.Visible;
                                LoadingIcon.Visibility = Visibility.Hidden;

                                UserNameWelcoming.Text = "Welcome Mr. " + userAccount.First_Name;

                                ImageBehavior.SetRepeatBehavior(CheckedIcon, new RepeatBehavior(TimeSpan.FromMilliseconds(500)));
                                timer = new DispatcherTimer();
                                timer.Interval = TimeSpan.FromSeconds(1);
                                timer.Tick += goToPasswordSlide;
                                timer.Start();
                                camera.Dispose();
                                break;
                            }
                        }
                    }
                }
                return;
            }
            catch (Exception e)
            {
                camera.Dispose();
                return;
            }
        }

        private void FrameProcedureForGuest()
        {
            try
            {
                if (camera.QueryFrame() != null)
                {
                    frame = camera.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    grayFace = frame.Convert<Gray, Byte>();
                    MCvAvgComp[][] facesDetectedNow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));
                    foreach (MCvAvgComp f in facesDetectedNow[0])
                    {
                        result = frame.Copy(f.rect).Convert<Gray, Byte>().Resize(240, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        timer2.Stop();
                        DoorRequest request = new DoorRequest();
                        request.full_name = textLastName.Text + " " + textFirstName.Text;
                        request.request_time = DateTime.Now;
                        request.request_message = textMessage.Text;
                        request.house_id = house_id;
                        request.face_image = result.Bytes;
                        Connection cnn = new Connection();
                        cnn.saveDoorRequest(request);
                        textFirstName.Text = "";
                        textLastName.Text = "";
                        textMessage.Text = "";
                        requestWaiting = true;
                        guestButton.IsEnabled = false;
                        CheckedIcon.Visibility = Visibility.Visible;
                        LoadingIcon.Visibility = Visibility.Hidden;
                        ImageBehavior.SetRepeatBehavior(CheckedIcon, new RepeatBehavior(TimeSpan.FromMilliseconds(500)));
                        timer4 = new DispatcherTimer();
                        timer4.Interval = TimeSpan.FromSeconds(1);
                        timer4.Tick += goToWaitingRequestSlide;
                        timer4.Start();
                        camera.Dispose();
                        break;

                    }
                }
                return;
            }
            catch (Exception e)
            {
                camera.Dispose();
                return;
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingIcon.Visibility = Visibility.Visible;
            ErrorLabel.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Hidden;
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(10);
            timer2.Tick += faceDetectionResult;
            timer2.Start();
            camera = new Capture();
            camera.QueryFrame();
            if (!isGust) 
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new Action(FrameProcedure));
            }
            else
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new Action(FrameProcedureForGuest));
            }

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textFirstName.Text) && !string.IsNullOrEmpty(textLastName.Text))
            {
                ErrorLabel3.Visibility = Visibility.Hidden;
                myTransitioner.SelectedIndex = 0;
            }
            else
            {
                ErrorLabel3.Text = "Please Enter Your First And Last Name!";
                ErrorLabel3.Visibility = Visibility.Visible;
            }
        }

        private void openDoorButton_click(object sender, RoutedEventArgs e)
        {
            if(userAccount != null)
            {
                if(userAccount.Password == passwordBox.Password)
                {
                    myTransitioner.SelectedIndex = 3;
                    passwordBox.Password = "";
                    timer3 = new DispatcherTimer();
                    timer3.Interval = TimeSpan.FromSeconds(5);
                    timer3.Tick += goBackToHomeSlide;
                    timer3.Start();
                }
                else
                {
                    ErrorLabel2.Text = "Wrong Password! Who Are You?";
                    ErrorLabel2.Visibility = Visibility.Visible;
                }
            }
        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            if(isGust)
            {
                isGust = false;
                guestButton.Content = "Are You A Guest !";
                description.Text = "Get Close To The Camera For FaceID Then Enter Your Password";
                myTransitioner.SelectedIndex = 0;
            }
            else
            {
                isGust = true;
                guestButton.Content = "Family Member!";
                description.Text = "Make your request by entering your name and a helpful message then get close to the camera for face detection";
                myTransitioner.SelectedIndex = 2;
            }
        }

        private void cancelRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (requestWaiting)
            {
                requestWaiting = false;
                timer.Stop();
                goBackToHomeSlideFromGuestSlides(sender, e);
            }
        }

        private void goBackToHomeSlide(object sender, EventArgs e)
        {
            timer3.Stop();
            userAccount = null;
            name = null;
            myTransitioner.SelectedIndex = 0;
        }
  
        private void goToPasswordSlide(object sender, EventArgs e)
        {
            timer.Stop();
            CheckedIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            myTransitioner.SelectedIndex = 1;
           
        }

        private void goToWaitingRequestSlide(object sender, EventArgs e)
        {
            timer4.Stop();
            myTransitioner.SelectedIndex = 4;
            CheckedIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            requestLoadinIcon.Visibility = Visibility.Visible;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(10);
            timer.Tick += requestTimeOut;
            timer.Start();
        }

        private void goBackToHomeSlideFromGuestSlides(object sender, EventArgs e)
        {
            if (timer4.IsEnabled)
            {
                timer4.Stop();
            }
            isGust = false;
            guestButton.Content = "Are You A Guest !";
            description.Text = "Get Close To The Camera For FaceID Then Enter Your Password";
            myTransitioner.SelectedIndex = 0;
            guestButton.IsEnabled = true;
            requestResponce.Visibility = Visibility.Hidden;
        }

        private void faceDetectionResult(object sender, EventArgs e)
        {
            if (isGust)
            {
                ErrorLabel.Text = "Face Could Not Be Recognized! Please Get More Close To The Camera";
                ErrorLabel.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                ErrorLabel.Text = "Face Not Recognized! Are You A guest";
            }
            timer2.Stop();
            LoadingIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            ErrorLabel.Visibility = Visibility.Visible;
            checkButton.Content = "Try Again";
            camera.Dispose();
        }

        private void requestTimeOut(object sender, EventArgs e)
        {
            if (timer.IsEnabled) { 
                timer.Stop();
            }
            if (requestWaiting)
            {
                requestWaiting = false;
                requestLoadinIcon.Visibility = Visibility.Hidden;
                requestWaitingTitle.Text = "Request TimeOut";
                requestResponce.Visibility = Visibility.Visible;
                requestResponce.Text = "Sorry!, your request was deneyed, No one responde to your request. ";
                timer4 = new DispatcherTimer();
                timer4.Interval = TimeSpan.FromSeconds(10);
                timer4.Tick += goBackToHomeSlideFromGuestSlides;
                timer4.Start();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }
        
        private void textFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textFirstName.Focus();
        }
        
        private void textLastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textLastName.Focus();
        }
        
        private void textMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textMessage.Focus();
        }
        
        private void textFirstName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textFirstName.Text) && textFirstName.Text.Length > 0)
                firstNameValue.Visibility = Visibility.Collapsed;
            else
                firstNameValue.Visibility = Visibility.Visible;
        }
        
        private void textLastName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textLastName.Text) && textLastName.Text.Length > 0)
                lastNameValue.Visibility = Visibility.Collapsed;
            else
                lastNameValue.Visibility = Visibility.Visible;
        }
        
        private void textMessage_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textMessage.Text) && textMessage.Text.Length > 0)
                messageValue.Visibility = Visibility.Collapsed;
            else
                messageValue.Visibility = Visibility.Visible;
        }

        public void respondToRequest(int request_id, int responce_by, Boolean positiveResponce, String ResponceMessage)
        {
            if (requestWaiting) {
                requestWaiting = false;
                timer.Stop();
                requestLoadinIcon.Visibility = Visibility.Hidden;
                requestResponce.Visibility = Visibility.Visible;
                if (positiveResponce)
                {
                    Connection cnn = new Connection();
                    DoorRequest request = cnn.getRequest(request_id);
                    string splittingName = request.full_name;
                    slide4Title.Text = "Welcome Mr. " + splittingName.Split(new String[] { " " }, 1, StringSplitOptions.None)[0];
                    requestWaitingTitle.Text = "Your Request Approved With Success";
                    if (ResponceMessage.Length != 0)
                    {
                        requestResponce.Text = ResponceMessage;
                    }
                    else
                    {
                        CheckedIcon.Visibility = Visibility.Visible;   
                        ImageBehavior.SetRepeatBehavior(CheckedIcon, new RepeatBehavior(TimeSpan.FromMilliseconds(500)));
                    }
                }
                else
                {
                    requestWaitingTitle.Text = "Your Request Was Deneyed";
                    if (ResponceMessage.Length != 0)
                    {
                        requestResponce.Text = ResponceMessage;
                    }
                    else
                    {
                        // Denyed request without a cause message
                    }
                }

                timer4 = new DispatcherTimer();
                timer4.Interval = TimeSpan.FromSeconds(10);
                timer4.Tick += goBackToHomeSlideFromGuestSlides;
                timer4.Start();
            }
        }
        
        }
}

