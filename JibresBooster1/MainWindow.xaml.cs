﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Reflection;
using JibresBooster1.lib;

namespace JibresBooster1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon myNotifObj;
        private System.Windows.Forms.ContextMenu myMenu;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                // Read more about notify on https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon
                myMenu = new ContextMenu();

                // Initialize itemJibres
                MenuItem itemJibres = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemJibres });
                itemJibres.Index = 0;
                itemJibres.Text = "Web&site";
                itemJibres.Click += new EventHandler(openJibresWebsite);

                // Initialize itemAbout
                MenuItem itemAbout = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemAbout });
                itemAbout.Index = 0;
                itemAbout.Text = "A&bout";
                itemAbout.Click += new EventHandler(openAboutBox);

                // Initialize itemExit
                MenuItem itemExit = new MenuItem();
                myMenu.MenuItems.AddRange(new MenuItem[] { itemExit });
                itemExit.Index = 0;
                itemExit.Text = "E&xit";
                itemExit.Click += new EventHandler(myMenuClose);

                // Create the NotifyIcon.
                myNotifObj = new NotifyIcon();
                myNotifObj.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
                myNotifObj.Text = Title;
                myNotifObj.Visible = true;
                // Handle the DoubleClick event to activate the form.
                myNotifObj.DoubleClick += new EventHandler(myMenuDblClick);
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


        private void myMenuDblClick(object Sender, EventArgs _e)
        {
            try
            {
                if (WindowState == WindowState.Minimized)
                {
                    WindowState = WindowState.Normal;
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
            System.Diagnostics.Process.Start("https://jibres.com");
        }

        private void openAboutBox(object Sender, EventArgs e)
        {
            //Form oldAboutFrm = System.Windows.Forms.Application.OpenForms["AboutBox1"];

            //if (oldAboutFrm != null)
            //{
            //    oldAboutFrm.Close();
            //}

            var myAboutBox1 = new Forms.AboutBox1();
            
            if (myAboutBox1.Visible)
            {
                myAboutBox1.Activate();
            }
            else
            {
                myAboutBox1.Show();
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            forceCloseApp();
        }
        private void forceCloseApp()
        {
            notif.say("خداحافظ", "جیبرس بوستر بسته شد");
            // Shutdown the application.
            System.Windows.Application.Current.Shutdown();
            // OR You can Also go for below logic
            Environment.Exit(0);
        }


        private void BtnIranKishTest_Click(object sender, RoutedEventArgs e)
        {
            var kicccWindow = new Forms.Generator.Kiccc();
            kicccWindow.Show();
        }

        private void BtnFaxModem_Click(object sender, RoutedEventArgs e)
        {
            lib.faxModem.fire();
        }

        private void ImgLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.com");
        }

        private void btnAsanPardakht_Click(object sender, RoutedEventArgs e)
        {
            //lib.PcPos.Asnapardakht.fire();
        }
    }

}
