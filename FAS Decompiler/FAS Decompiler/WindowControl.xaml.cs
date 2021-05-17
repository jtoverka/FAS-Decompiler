using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Basic
{
    /// <summary>
    /// Interaction logic for WindowControl.xaml
    /// </summary>
    public partial class WindowControl : UserControl, INotifyPropertyChanged
    {
        #region Fields
        
        private bool mRestoreForDragMove = false;

        #endregion
        
        #region Properties
        #region DP - Window : Window

        /// <summary>
        /// Get or Set the Window Property.
        /// </summary>
        public Window Window
        {
            get { return (Window)GetValue(WindowProperty); }
            set 
            {
                if (value == Window)
                    return;

                SetValue(WindowProperty, value);

                PropertyChanged?.Invoke(this, new("Window"));
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Window.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register(nameof(Window), typeof(Window), typeof(WindowControl));

        #endregion
        #region DP - MinVisibility : bool

        /// <summary>
        /// Gets or Sets the minimize button visibility
        /// </summary>
        public Visibility MinVisibility
        {
            get { return (Visibility)GetValue(MinVisibilityProperty); }
            set { SetValue(MinVisibilityProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for MinVisibility.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MinVisibilityProperty =
            DependencyProperty.Register(nameof(MinVisibility), typeof(Visibility), typeof(WindowControl));

        #endregion
        #region DP - MaxVisibility : bool

        /// <summary>
        /// Gets or Sets the maximize button visibility
        /// </summary>
        public Visibility MaxVisibility
        {
            get { return (Visibility)GetValue(MaxVisibilityProperty); }
            set { SetValue(MaxVisibilityProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for MaxVisibility.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MaxVisibilityProperty =
            DependencyProperty.Register(nameof(MaxVisibility), typeof(Visibility), typeof(WindowControl));

        #endregion
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public WindowControl()
        {
            this.DataContext = this;
            this.MinVisibility = Visibility.Visible;
            this.MaxVisibility = Visibility.Visible;
            PropertyChanged += WindowControl_PropertyChanged;
            InitializeComponent();
        }

        #endregion

        #region Delegates, Events, Handlers

        /// <summary>
        /// Invoked when the Window_Exit_Button is clicked
        /// </summary>
        public event EventHandler WindowExit;

        /// <summary>
        /// Invoked when the Window Property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Subscribe to Window SizeChanged event upon new assignment to Window Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Window.SizeChanged += Window_SizeChanged;
        }

        /// <summary>
        /// Controls the state of the window when the title bar is being moved
        /// </summary>
        public void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (this.mRestoreForDragMove)
            {
                this.mRestoreForDragMove = false;

                var point = Window.PointToScreen(e.MouseDevice.GetPosition(Window));

                Window.Left = point.X - (Window.RestoreBounds.Width * 0.5);
                Window.Top = point.Y;

                Window.WindowState = WindowState.Normal;
            }

            Window.DragMove();
        }

        /// <summary>
        /// Re-adjust borders if window size is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.WindowState == WindowState.Normal)
            {
                this.mRestoreForDragMove = false;
                if (Window.ResizeMode != ResizeMode.NoResize)
                    Window.BorderThickness = new Thickness(0);
            }
            else if (Window.WindowState == WindowState.Maximized)
            {
                this.mRestoreForDragMove = true;
                if (Window.ResizeMode != ResizeMode.NoResize)
                    Window.BorderThickness = new Thickness(7);
            }
        }

        /// <summary>
        /// Window minimize button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Window resize button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_Resize_Click(object sender, RoutedEventArgs e)
        {
            if (Window.WindowState == WindowState.Maximized)
                Window.WindowState = WindowState.Normal;
            else
                Window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Delegate the cancel button to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Exit_Click(object sender, RoutedEventArgs e)
        {
            WindowExit.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
