using System;
using System.Reflection;
using System.Windows.Forms;

namespace BaseProject
{
    public partial class ArchivateDearchivate : Form
    {
        Object[] str = new Object[1];
        Object o;
        public ArchivateDearchivate(Object obj)
        {
            o = obj;
            InitializeComponent();
        }

        private void button_arch_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
                openFolderDialog.ShowDialog();

                MethodInfo CreateArchive = o.GetType().GetMethod("CreateArchive");
                str[0] = openFolderDialog.SelectedPath;
                CreateArchive.Invoke(o, str);
            }
            catch
            {

            }
            this.Close();
        }

        private void button_de_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "ZIP File|*.zip"
                };
                openFileDialog.ShowDialog();

                MethodInfo ReadArchive = o.GetType().GetMethod("ReadArchive");
                str[0] = openFileDialog.FileName;
                ReadArchive.Invoke(o, str);
            }
            catch
            {

            }
            this.Close();
        }
    }
}
