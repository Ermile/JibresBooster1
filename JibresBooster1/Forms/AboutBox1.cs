using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace JibresBooster1.Forms
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            var version = fieVersionInfo.FileVersion;


            Text = String.Format("About {0}", fieVersionInfo.ProductName);
            labelProductName.Text = fieVersionInfo.ProductName;
            labelVersion.Text = String.Format("Version {0}", fieVersionInfo.ProductVersion);
            labelCopyright.Text = fieVersionInfo.LegalCopyright;
            labelCompanyName.Text = fieVersionInfo.CompanyName;
            // this.textBoxDescription.Text = fieVersionInfo.FileDescription;
        }
    }
}
