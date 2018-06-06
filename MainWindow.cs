using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FigureBase;
using static BaseProject.Verification;

namespace BaseProject
{
    public partial class Pencil : Form
    {
        Collection collection = new Collection();
        List<AbstractShape> plugins;
        Object archiver; // Archiver
        private readonly CustomSerializer formatter = CustomSerializer.GetInstance;
        
        private void LoadPlugins(string path)
        {
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");
            plugins = new List<AbstractShape>();

            foreach (string pluginPath in pluginFiles)
            {       
                try
                {
                    Assembly assembly = Assembly.LoadFrom(pluginPath);
                    Type[] objType = assembly.GetTypes();

                    foreach (var type in objType)
                    {
                        if (type.IsSubclassOf(typeof(AbstractShape)))
                        {
                            var plugin = (AbstractShape)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                            pluginsList.Items.Add(plugin.ToString()).SubItems.Add(isSignedPlugin(assembly).ToString());
                        }
                    }

                }
                catch
                {
                    continue;
                }
            }
        }

        private void button_Reload_Click(object sender, EventArgs e)
        {
            plugins.Clear();
            pluginsList.Items.Clear();
            archiver = null;
            LoadPlugins(Application.StartupPath);
        }

        public Pencil()
        {
            InitializeComponent();
            ThisAppStrongName = GetStrongName(Assembly.GetExecutingAssembly());
            this.LoadPlugins(Application.StartupPath);
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button_Draw_Click(object sender, EventArgs e)
        {
            collection.DrawAll(pictureBox1);
        }

        private void button_clear_list_Click(object sender, EventArgs e)
        {
            collection.collection.Clear();
            FiguresList.Items.Clear();
        }

        private void FiguresList_DoubleClick(object sender, EventArgs e)
        {
            if (FiguresList.SelectedIndex >= 0)
            {
                dynamic figure = FiguresList.SelectedItem;
                FigureParametrs form = new FigureParametrs(figure);
                form.ShowDialog();
                FiguresList.Refresh();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (FiguresList.SelectedIndex >= 0)
            {
                collection.collection.RemoveAt(FiguresList.SelectedIndex);
                FiguresList.Items.RemoveAt(FiguresList.SelectedIndex);
            }
        }

        private void pluginsList_DoubleClick_1(object sender, EventArgs e)
        {
            if (pluginsList.SelectedIndices.Count > 0)
            {
                foreach (var plugin in plugins)
                {
                    if (plugin.ToString() == pluginsList.SelectedItems[0].Text)
                    {
                        dynamic figure = plugin.Clone();
                        FigureParametrs form = new FigureParametrs(figure);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            collection.collection.Add(figure);
                            FiguresList.Items.Add(figure);
                        }
                    }

                    if (pluginsList.SelectedItems[0].Text == "Archiver")
                    {
                        ArchivateDearchivate form = new ArchivateDearchivate(archiver);
                        form.Show();
                        break;
                    }
                }
            }
        }

        private void button_archive_Click(object sender, EventArgs e)
        {
            if (archiver == null)
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog()
                    {
                        Filter = "DLL File|*.dll"
                    };
                    openFileDialog.ShowDialog();
                    Assembly assembly = Assembly.LoadFrom(openFileDialog.FileName);
                    pluginsList.Items.Add("Archiver").SubItems.Add(isSignedPlugin(assembly).ToString());
                    archiver = assembly.CreateInstance("PluginArchiver.Archiver");
                }
                catch
                {
                    archiver = null;
                }
            }
            else
            {
                MessageBox.Show("File archiver already loaded!");
            }
        }

        private void button_serialize_Click(object sender, EventArgs e)
        {
            formatter.Serialize(collection);
        }

        private void button_deserialize_Click(object sender, EventArgs e)
        {
            FiguresList.Items.Clear();
            collection.collection.Clear();

            collection = formatter.Deserialize(plugins);
            foreach (var name in collection.collection)
            {
                FiguresList.Items.Add(name);
            }
        }

        private void button_adapter_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "DLL File|*.dll"
                };
                openFileDialog.ShowDialog();
                Assembly assembly = Assembly.LoadFrom(openFileDialog.FileName);
                Type[] types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(AbstractShape)))
                    {
                        var plugin = (AbstractShape)Activator.CreateInstance(type);
                        plugins.Add(plugin);
                        pluginsList.Items.Add(plugin.ToString()).SubItems.Add(isSignedPlugin(assembly).ToString());
                    }
                }
            }
            catch
            {
            }
        }
    }
}
