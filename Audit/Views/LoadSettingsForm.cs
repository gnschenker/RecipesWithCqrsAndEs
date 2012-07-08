using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Audit.Properties;

namespace Audit.Views
{
    public partial class LoadSettingsForm : Form
    {
        IDictionary<string, string> _settings = new Dictionary<string, string>();

        public bool ConfigFileSelected { get; private set; }

        public LoadSettingsForm()
        {
            InitializeComponent();

            Shown += OnLoad;
        }


        void OnLoad(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(_file.Text))
            {
                _browse.PerformClick();
            }
        }

        public void BindArgs(string[] settings)
        {
            if (settings.Length == 1)
            {
                BindDocument(settings[1]);
            }
        }

        void BindDocument(string fileName)
        {
            ConfigFileSelected = false;
            if (!string.IsNullOrEmpty(fileName))
            {
                if (Path.GetExtension(fileName).IndexOf("cscfg") > -1)
                {
                    var x = XDocument.Load(fileName);

                    _settings = LoadSettings(x);
                    string value;
                    if (_settings.TryGetValue("DataStore", out value))
                    {
                        ConfigFileSelected = true;
                        _storage.Text = value;
                    }

                    _file.Text = fileName;
                }
                else if (Path.GetExtension(fileName).IndexOf("tmd") > -1)
                {
                    _file.Text = fileName;
                    _storage.Text = fileName;
                }
            }
        }


        public string GetAzureConfig()
        {
            return _storage.Text;
        }

        static IDictionary<string, string> LoadSettings(XDocument doc)
        {
            var settings = doc.Root
                .Descendants()
                .Where(x => x.Name.LocalName == "Setting")
                .Where(
                    x => x.Attributes().Any(a => a.Name == "name") && x.Attributes().Any(s => s.Name == "value"))
                .Select(x => Tuple.Create(x.Attribute("name").Value, x.Attribute("value").Value));

            var dict = new Dictionary<string, string>();

            foreach (var setting in settings)
            {
                if (!dict.ContainsKey(setting.Item1))
                {
                    dict.Add(setting.Item1, setting.Item2);
                }
                else
                {
                    var expected = dict[setting.Item1];
                    if (expected != setting.Item2)
                    {
                        throw new InvalidOperationException("Invalid settings file");
                    }
                }
            }

            return dict;
        }

        void BrowseClick(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = @"Pick service configuration or Domain log file to work with";
                ofd.Filter = @"Log Source Config|*.cscfg;*.tmd";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    BindDocument(ofd.FileName);
                }
            }
        }

        void _help_Click(object sender, EventArgs e)
        {
            using (var mem = new MemoryStream(Resources.ReadMe))
            using (var reader = new StreamReader(mem))
            {
                MessageBox.Show(this, reader.ReadToEnd(), "About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}