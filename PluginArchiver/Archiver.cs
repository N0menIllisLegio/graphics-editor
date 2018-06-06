using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace PluginArchiver
{
    public class Archiver
    {
        public void CreateArchive(string path)
        {
            string zipPath = path + ".zip";
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            ZipFile.CreateFromDirectory(path, zipPath);
            Directory.Delete(path, true);
            MessageBox.Show("File archived succesfuly");
        }

        public void ReadArchive(string zipPath)
        {
            string extractPath = zipPath.Replace(".zip", "");
            ZipFile.ExtractToDirectory(zipPath, extractPath);
            File.Delete(zipPath);
            MessageBox.Show("File unarchived succesfuly");
        }
    }
}
