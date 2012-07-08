using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Audit.Util;
using Lokad.Cqrs;
using Lokad.Cqrs.Envelope;
using Recipes.Contracts;

namespace Audit.Views
{
    public partial class DomainLogView : Form
    {
        int _maxCount = 500;

        int MaxCount
        {
            get { return _maxCount; }
            set { _maxCount = value; }
        }

        readonly LocalEventStore _client;

        readonly IEnvelopeStreamer _serializer;
        readonly ShellServices _services;

        Int64 _currentId;

        public DomainLogView(LocalEventStore eventStore, SimpleMessageSender endpoint, IEnvelopeStreamer serializer)
        {
            InitializeComponent();

            _client = eventStore;

            _services = new ShellServices(endpoint);
            _serializer = serializer;

            _log.BackColor = CommonColors.Solarized.Base03;
            _log.ForeColor = CommonColors.Solarized.Base0;

            _display.BackColor = CommonColors.Solarized.Base3;
            _display.ForeColor = CommonColors.Solarized.Base00;

            _detailsLabel.BackColor = CommonColors.Solarized.Base2;
            _detailsLabel.ForeColor = CommonColors.Solarized.Base01;

            var control = new RepopulateView(serializer, eventStore, _services) {Dock = DockStyle.Fill};
            viewsTab.Controls.Add(control);
        }

        public void Log(string text, params object[] args)
        {
            _services.Log("DLV", text, args);
        }

        void LoadEventsTill(long maxVersion, bool sync, ToolStripItem sender)
        {
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            _domainGrid.DataSource = new[] {new {Message = "Loading..."}};

            var start = sync ? _client.SyncLog(s => Log(s)) : Task.Factory.StartNew(() => { });

            start.ContinueWith(t =>
                {
                    var list = new List<DomainLogDisplayItem>();

                    long maxCount;
                    if (!Int64.TryParse(txtCount.Text, out maxCount))
                    {
                        maxCount = MaxCount;
                    }

                    MaxCount = (int) maxCount;

                    _client.ObserveTill(maxVersion, (int) maxCount, s =>
                        {
                            var envelope = _serializer.ReadAsEnvelopeData(s.Data);


                            for (int i = 0; i < envelope.Items.Length; i++)
                            {
                                var item = envelope.Items[i];
                                var session = DomainAwareAnalysis.GetCategoryNames(item);
                                // clone batch envelopes into separate ones
                                var clone = EnvelopeBuilder.CloneProperties(envelope.EnvelopeId + "-" + i, envelope);
                                clone.AddItem(item.Content);
                                list.Add(new DomainLogDisplayItem(clone.Build(), session, s.Version));
                            }
                        });
                    return list;
                }).ContinueWith(x =>
                    {
                        if (x.Exception != null)
                        {
                            Log("Exception: {0}", x.Exception.Message);
                            Trace.WriteLine(x.Exception);
                            return;
                        }
                        var items = x.Result;
                        if (items.Count > 0)
                        {
                            var min = items.Min(i => i.StoreIndex) - 1;
                            _currentId = min;
                        }

                        _domainGrid.DataSource = items;
                        tabControl1.SelectedTab = _domainLogTab;

                        // paint

                        foreach (DataGridViewRow dataGridViewRow in _domainGrid.Rows)
                        {
                            var disp = (DomainLogDisplayItem) dataGridViewRow.DataBoundItem;
                            foreach (DataGridViewCell cell in dataGridViewRow.Cells)
                            {
                                disp.Style(cell);
                            }
                        }

                        _domainGrid.Columns[0].Width = 200;
                        _domainGrid.Columns[1].Width = 80;
                        _domainGrid.Columns[2].Width = 90;
                        _domainGrid.Columns[3].Width = 40;

                        Log("Displayed {0} records starting from version {1}.", items.Count, _currentId);
                    }, context)
                .ContinueWith(_ =>
                    {
                        if (sender != null)
                        {
                            Invoke(new MethodInvoker(() => sender.Enabled = true));
                        }
                    });

            if (start.Status == TaskStatus.Created)
                start.Start();
        }

        void DataGridView1SelectionChanged(object sender, EventArgs e)
        {
            _display.Text = "";

            var builder = new StringBuilder();


            var rows = _domainGrid.SelectedRows;

            if (rows.Count < 10)
            {
                foreach (DataGridViewRow row in rows)
                {
                    var display = row.DataBoundItem as DomainLogDisplayItem;
                    if (null != display)
                    {
                        builder.AppendLine(display.ToString()).AppendLine();
                    }
                }
            }
            else
            {
                builder.AppendLine(string.Format("There are {0} messages selected", rows.Count));
            }
            _display.Text = builder.ToString();
        }

        void NextEventsClick(object sender, EventArgs e)
        {
            _nextEvents.Enabled = false;
            LoadEventsTill(_currentId, false, _nextEvents);
        }

        void BeginningEventsClick(object sender, EventArgs e)
        {
            _beginningEvents.Enabled = false;
            LoadEventsTill(int.MaxValue, _client.IsSyncAvailable, _beginningEvents);
        }

        void ResendClick(object sender, EventArgs e)
        {
            if (_domainGrid.SelectedRows.Count == 0)
                return;

            var envelopes = _domainGrid.SelectedRows.Cast<DataGridViewRow>()
                .Select(v => v.DataBoundItem)
                .Cast<DomainLogDisplayItem>()
                .Select(d => d).ToArray();
            var all = envelopes
                .SelectMany(d => d.Item.Items.Select(i => i.Content)).ToArray();


            // don`t resend set of commands
            if (_domainGrid.SelectedRows.Count > 1)
            {
                Log("Selected too many commands. Please, select only one!");

                return;
            }


            if (Debugger.IsAttached)
            {
                foreach (var o in envelopes)
                {
                    var saveEnvelopeData = _serializer.SaveEnvelopeData(o.Item);
                    Trace.WriteLine(Convert.ToBase64String(saveEnvelopeData));
                }
            }
            var items = all
                .OfType<IRecipeCommand>()
                .ToArray();

            // don`t resend set of commands
            if (items.Length > 1)
            {
                Log("Too many commands in selected envelope. Please, select only one!");

                //_services.Client.Put(all.ToArray());
                return;
            }

            if (items.Length == 0)
            {
                Log(@"No commands to send.");
                return;
            }

            var text = new StringBuilder();

            text
                .AppendFormat("Do you want to send message? You will have to live with the consequences!")
                .AppendLine()
                .Append(string.Join(";", items.Select(c => c.GetType().Name)));
            if (
                MessageBox.Show(this, text.ToString(), @"Send confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                foreach (var shelfCommand in items)
                {
                    _services.SendCommand(shelfCommand);
                }
            }
        }

        void ModifySendMenuItemClick(object sender, EventArgs e)
        {
            if (_domainGrid.SelectedRows.Count == 0)
            {
                Log(@"No commands to send.");
                return;
            }

            // don`t resend set of commands
            if (_domainGrid.SelectedRows.Count > 1)
            {
                Log("Selected too many commands. Please, select only one!");
                return;
            }

            try
            {
                var items = _domainGrid.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(i => (DomainLogDisplayItem) i.DataBoundItem)
                    .SelectMany(d => d.Item.Items)
                    .ToList();

                // don`t resend set of commands
                if (items.Count > 1)
                {
                    Log("Too many commands in selected envelope. Please, select only one!");
                    return;
                }

                var envelope = items.Single();
                using (var m = new ModifyMessage())
                {
                    m.BindMessage(envelope.MappedType, envelope.Content);
                    if (m.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    var result = m.GetMessage();

                    if (
                        MessageBox.Show(this, @"Do you really want to send this message?", @"Sending confirmation",
                            MessageBoxButtons.YesNo) ==
                                DialogResult.No)
                        return;

                    _services.SendCommand((IRecipeCommand) result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), @"Error");
            }
        }

        void DomainLogView_Load(object sender, EventArgs e)
        {
            _services.ConnectLogger(_log);
            _domainGrid.Focus();

            Log("Starting sync...");

            LoadEventsTill(int.MaxValue, _client.IsSyncAvailable, null);
        }

        void DomainLogView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _services.Disconnect();
        }
    }
}