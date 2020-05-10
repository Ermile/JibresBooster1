using System;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using JibresBooster1.lib;
using JibresBooster1.translation;

namespace JibresBooster1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon myNotifObj;
        private readonly System.Windows.Forms.ContextMenu myMenu;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                // Read more about notify on https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon
                myMenu = new ContextMenu();

                // Initialize itemOpenWindow
                MenuItem itemOpenWindow = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemOpenWindow });
                itemOpenWindow.Index = 0;
                itemOpenWindow.Text = T.get("OpenApp");
                itemOpenWindow.Click += new EventHandler(openMainWindow);

                // seperator
                MenuItem seperator = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { seperator });
                seperator.Index = 1;
                seperator.Text = "-";

                // Initialize itemJibres
                MenuItem itemJibres = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemJibres });
                itemJibres.Index = 2;
                itemJibres.Text = T.get("Website");
                itemJibres.Click += new EventHandler(openJibresWebsite);

                // Initialize itemAbout
                MenuItem itemAbout = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemAbout });
                itemAbout.Index = 3;
                itemAbout.Text = T.get("About");
                itemAbout.Click += new EventHandler(openAboutBox);

                // seperator
                MenuItem seperatorExit = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { seperatorExit });
                seperatorExit.Index = 4;
                seperatorExit.Text = "-";

                // Initialize itemExit
                MenuItem itemExit = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemExit });
                itemExit.Index = 5;
                itemExit.Text = T.get("ExitApp");
                itemExit.Click += new EventHandler(myMenuClose);


                // Create the NotifyIcon.
                myNotifObj = new NotifyIcon
                {
                    Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                    Text = Title,
                    Visible = true
                };
                // Handle the DoubleClick event to activate the form.
                myNotifObj.DoubleClick += new EventHandler(openMainWindow);
                // The ContextMenu property sets the menu that will
                // appear when the systray icon is right clicked.
                myNotifObj.ContextMenu = myMenu;

                notif.init(myNotifObj);
                // say ready message


                log.save("Application started.");
                listener.runListener();
            }
            catch (Exception e)
            {
                log.save("Error on running program! " + e.Message);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void openMainWindow(object Sender, EventArgs _e)
        {
            try
            {
                if (WindowState == WindowState.Minimized)
                {
                    WindowState = WindowState.Normal;
                    return;
                }

                if (IsVisible)
                {
                    Activate();
                }
                else
                {
                    Show();
                }
            }
            catch (Exception e)
            {
                log.save("Error on reopen program! " + e.Message);
            }
        }


        private void myMenuClose(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            forceCloseApp();
            Close();
        }

        private void openJibresWebsite(object Sender, EventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.ir");
        }

        private void openAboutBox(object Sender, EventArgs e)
        {
            Form oldFrm = System.Windows.Forms.Application.OpenForms["AboutBox1"];
            if (oldFrm != null)
            {
                oldFrm.Close();
            }

            Forms.AboutBox1 myAboutBox1 = new Forms.AboutBox1();
            if (myAboutBox1.Visible)
            {
                myAboutBox1.Activate();
            }
            else
            {
                myAboutBox1.Show();
            }
        }


        private void forceCloseApp()
        {
            notif.appExit();
            // Shutdown the application.
            System.Windows.Application.Current.Shutdown();
            // OR You can Also go for below logic
            Environment.Exit(0);
        }


        private void BtnIranKishTest_Click(object sender, RoutedEventArgs e)
        {
            //Form oldFrm = System.Windows.Forms.Application.OpenForms["Kiccc"];
            //if (oldFrm != null)
            //{
            //    oldFrm.Close();
            //}

            Forms.Generator.Kiccc kicccWindow = new Forms.Generator.Kiccc();
            kicccWindow.Show();
        }

        private void BtnFaxModem_Click(object sender, RoutedEventArgs e)
        {
            lib.faxModem.fire();
        }

        private void ImgLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.ir");
        }

        private void btnAsanPardakht_Click(object sender, RoutedEventArgs e)
        {
            //lib.PcPos.Asnapardakht.fire();
        }
    }

}
