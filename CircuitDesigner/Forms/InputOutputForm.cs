using CircuitDesigner.Models;
using System.ComponentModel;
using System.Data;

namespace CircuitDesigner.Forms
{
    internal partial class InputOutputForm : Form
    {
        private readonly string[] UsedNames = [];

        public string ConnectorName
        {
            get { return NameInput.Text; } 
            set { NameInput.Text = value; }
        }

        public new bool Enabled
        {
            get { return EnabledCheckbox.Checked; }
            set { EnabledCheckbox.Checked = value; }
        }

        public void Init(ConnectorModel model)
        {
            InitializeComponent();

            if (model is InputModel) { Text = "Input Properties"; }
            else { Text = "Output Properties"; }

            EnabledCheckbox.Checked = model.Enabled;
            NameInput.Text = model.Name;

            //ConnectionsLabel.Text = $"Connections ({model.Connections.Count})";
            //foreach (var cnn in model.Connections)
            //{
            //    ConnectionList.Items.Add(cnn.Name);
            //}
        }

        public InputOutputForm(ConnectorModel model, List<InputModel> connectors)
        {
            UsedNames = connectors.Select(x => x.Name)
                .Where(x => !x.Equals(model.Name, StringComparison.OrdinalIgnoreCase)).ToArray();
            Init(model);
        }

        private bool IsValidName(string name)
        {
            return !(string.IsNullOrEmpty(name) || 
                UsedNames.Any(x => x.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        private void NameInput_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValidName(NameInput.Text))
            {
                NameInput.BackColor = Color.Firebrick;
                e.Cancel = true;
                AcceptBtn.Enabled = false;
            }
            else
            {
                NameInput.BackColor = default;
                AcceptBtn.Enabled = true;
            }
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
