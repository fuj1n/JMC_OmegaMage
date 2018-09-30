using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WorldEdit
{
    public partial class WorldPropertiesEditor : Form
    {
        public Dictionary<char, string> CustomPortals
        {
            get
            {
                Dictionary<char, string> dict = new Dictionary<char, string>();
                foreach (DictionaryElement el in customPortals.Controls.OfType<DictionaryElement>())
                {
                    if (string.IsNullOrWhiteSpace(el.Key) || string.IsNullOrWhiteSpace(el.Value))
                        continue;
                    dict[el.Key[0]] = el.Value;
                }

                return dict;
            }
            set
            {
                foreach (Control c in customPortals.Controls)
                    c.Dispose();

                customPortals.Controls.Clear();

                foreach (KeyValuePair<char, string> kvp in value)
                {
                    AddValueToDict(kvp.Key, kvp.Value);
                }
            }
        }

        public WorldPropertiesEditor()
        {
            InitializeComponent();

            btnAddPortal.Click += (o, e) =>
            {
                using (RoomSelectorBox sel = new RoomSelectorBox())
                {
                    if (sel.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(sel.text.Text))
                        AddValueToDict(sel.text.Text[0], "");
                }
            };
        }

        private void AddValueToDict(char key, string value)
        {
            if (customPortals.Controls.OfType<DictionaryElement>().Any(d => d.Key == key.ToString()))
                return;

            DictionaryElement el = new DictionaryElement
            {
                Key = key.ToString(),
                Value = value,
                Parent = customPortals
            };
            el.onClickRemove += e =>
            {
                customPortals.Controls.Remove(e);
                e.Dispose();
            };
        }

    }
}
