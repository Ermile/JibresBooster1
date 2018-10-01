using System;
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
using System.Net;
using System.Threading;

namespace JibresBooster1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            runListener();
        }

        private void btnJibresWebsite_Click(object sender, RoutedEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.com");
        }


        static HttpListener _httpListener = new HttpListener();
        static void runListener()
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.logText.Document.Blocks.Clear();
            mw.logText.Document.Blocks.Add(new Paragraph(new Run(DateTime.Now + "\tStarting server...")));

            // add prefix "http://localhost:4200/"
            _httpListener.Prefixes.Add("http://localhost:4200/");
            // start server (Run application as Administrator!)
            _httpListener.Start();
            mw.logText.Document.Blocks.Add(new Paragraph(new Run(DateTime.Now + "\tServer started.")));
            Thread _responseThread = new Thread(ResponseThread);
            // start the response thread
            _responseThread.Start(); 
        }

        static void ResponseThread()
        {
            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext(); // get a context
                // Now, you'll find the request URL in context.Request.Url
                // get the bytes to response
                byte[] _responseArray = Encoding.UTF8.GetBytes("<html><head><title>Localhost server -- port 4200</title></head>" +
                    "<body>Welcome to the <strong>Localhost server</strong> -- <em>port 4200!</em></body></html>");
                // write bytes to the output stream
                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length);
                // set the KeepAlive bool to false
                context.Response.KeepAlive = false;
                // close the connection
                context.Response.Close();

                // ((MainWindow)Application.Current.MainWindow).logText.Document.Blocks.Add(new Paragraph(new Run(DateTime.Now + "\tRespone given to a request." + Environment.NewLine)));
                MessageBox.Show("Respone given to a request.");
                // Console.WriteLine("Respone given to a request.");
            }
        }
    }

}
