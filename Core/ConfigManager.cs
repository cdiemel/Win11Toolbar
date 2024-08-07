using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win11Toolbar.Core
{
    internal class ConfigManager
    {
        private string[] _toolbarPaths;

        //
        // Start Singleton Magice
        //
        private static ConfigManager instance;
        public static ConfigManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigManager();
                return instance;
            }
        }
        //
        // End Singleton Magic
        //
        private ConfigManager()
        {
            Debug.WriteLine("ConfigManager");
            this._toolbarPaths = new string[5];
            this._GetConfig();
        }

        public string[] ToolbarPaths
        {
            get { return this._toolbarPaths; }
            set
            {
                Debug.WriteLine("ToolbarPaths:Set");
                this._toolbarPaths = value;
                this._SetConfig();
                this.UpdateConfig();
            }
        }

        private void _GetConfig()
        {
            Debug.WriteLine("_GetConfig");
            //List<string> paths = new List<string>();
            //int i = paths.FindIndex(a => a.ToString() == "");
            //paths.Insert(i, this._toolbarPaths[i]);

            this._toolbarPaths = new string[10];
            Console.WriteLine(this._toolbarPaths[1]);
            string lines = File.ReadAllText($@"{System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Desktop\Win11Toolbar.wtb11c");
            Console.WriteLine(lines);
            foreach (string line in lines.Trim().Split('\r'))
            {
                string[] parts = line.Trim().Split('|');
                if (!int.TryParse(parts[0], out int index)) continue;
                if (index > this._toolbarPaths.Length - 1) { throw new ArgumentOutOfRangeException("index"); }
                Debug.WriteLine($"_GetConfig: {parts[1]}");
                if (Directory.Exists(parts[1]))
                {
                    this._toolbarPaths[index] = parts[1];
                    TabManager.Instance.AddTab(index+1, parts[1]);
                }
            }
            Console.WriteLine(this._toolbarPaths[0]);
        }

        private void _SetConfig()
        {
            Console.WriteLine(this._toolbarPaths[1]);
            string[] _tmpArray = new string[this._toolbarPaths.Length];
            int index = 0;
            foreach (string path in this._toolbarPaths)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    _tmpArray[index] = $"{index}|{path}";
                    index++;
                }
            }
            File.WriteAllLines(@"C:\Users\casdiem2\Desktop\Win11Toolbar.wtb11c", _tmpArray);
        }

        public void UpdateConfig()
        {
            this._GetConfig();
            int index = 0;
            string[] _tmpArray = new string[this._toolbarPaths.Length];
            foreach (string path in this._toolbarPaths)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    TabManager.Instance.UpdateTab(index, path);
                    _tmpArray[index] = path;
                    index++;
                }
            }
            if (this._toolbarPaths.Length != _tmpArray.Length)
            {
                this._toolbarPaths = _tmpArray;
                this._SetConfig();
            }
        }
    }
}
