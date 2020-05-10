using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace JibresBooster1.Forms
{
    internal partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            string version = fieVersionInfo.FileVersion;


            Text = string.Format("About {0}", fieVersionInfo.ProductName);
            labelProductName.Text = fieVersionInfo.ProductName;
            labelVersion.Text = string.Format("Version {0}", fieVersionInfo.ProductVersion);
            labelCopyright.Text = fieVersionInfo.LegalCopyright;
            labelCompanyName.Text = fieVersionInfo.CompanyName;
            // this.textBoxDescription.Text = fieVersionInfo.FileDescription;
        }
    }
}
