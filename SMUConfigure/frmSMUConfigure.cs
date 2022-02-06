using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SMUConfigure
{
    public partial class frmSMUConfigure : Form
    {
        static string path = "C:\\SiteMapUpdater\\settings.config";

        AESEncryption AES = new AESEncryption();

        public frmSMUConfigure()
        {
            InitializeComponent();
        }

        private void frmSMUConfigure_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                foreach (string line in File.ReadAllLines(path))
                {
                    lbPathStrings.Items.Add(AES.Decrypt(line));
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            File.Delete(path);

            StreamWriter sw = File.CreateText(path);
            {
                if (lbPathStrings.Items.Count != 0)
                {
                    foreach (string item in lbPathStrings.Items)
                    {
                        sw.WriteLine(AES.Encrypt(item));
                    }
                    sw.Close();
                }
                else
                {
                    sw.Close();
                    File.Delete(path);
                }
            }
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            DialogResult result = FBD.ShowDialog();
            {
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(FBD.SelectedPath))
                {
                    lbPathStrings.Items.Add(FBD.SelectedPath.ToString());
                }
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbPathStrings.Items.Count > 0)
            {
                if (lbPathStrings.SelectedIndex > -1)
                {
                    lbPathStrings.Items.RemoveAt(lbPathStrings.SelectedIndex);
                }
            }
        }


    }
}
