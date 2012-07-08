using System;
using System.Linq;
using System.Windows.Forms;
using Lokad.Cqrs;
using Lokad.Cqrs.Envelope;
using Recipes.Contracts;
using ServiceStack.Text;

namespace Audit.Util
{
    public sealed class DomainLogDisplayItem
    {
        public readonly ImmutableEnvelope Item;
        public readonly string RecordId;
        public readonly long StoreIndex;
        public Action<DataGridViewCell> Style = style => { };

        public DomainLogDisplayItem(ImmutableEnvelope item, string session, long storeIndex)
        {
            Class = string.Join(",", item.Items.Select(s => s.MappedType.Name).ToArray());
            Recorded = FormatUtil.TimeOffsetUtc(DateTime.SpecifyKind(item.CreatedOnUtc, DateTimeKind.Utc));
            RecordId = item.EnvelopeId;
            Item = item;
            Session = session;
            AssignStyle(item, session);
            StoreIndex = storeIndex;

            Type = ' ';
        }

        public string Class { get; set; }
        public string Recorded { get; set; }
        public string Session { get; set; }
        public char Type { get; set; }

        void AssignStyle(ImmutableEnvelope item, string session)
        {
            Style += cell =>
                {
                    if (session == (cell.Value as string))
                    {
                        cell.Style.BackColor = DomainAwareAnalysis.GetBackgroundColorForCategory(session);
                    }
                    else if (cell.Value is char)
                    {
                        cell.Style.BackColor = DomainAwareAnalysis.GetBackgroundColorForCategory(Class);
                    }
                    else
                    {
                        cell.Style.BackColor = DomainAwareAnalysis.GetBackgroundColorForContract(item.Items[0]);
                    }
                };
        }

        string Indent(string source)
        {
            return string.Join("\r\n",
                source.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(s => ">  " + s));
        }

        public override string ToString()
        {
            return Item.PrintToString(o =>
                {
                    string details = JsvFormatter.Format(JsonSerializer.SerializeToString(o));
                    string description;
                    if (Describe.TryDescribe(o, out description))
                    {
                        return "\r\n" + Indent(description) + "\r\n\r\n" + details;
                    }
                    return details;
                });
        }
    }
}