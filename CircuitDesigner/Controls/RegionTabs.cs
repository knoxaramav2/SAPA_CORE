using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class RegionTabs : UserControl
    {
        public RegionTabs()
        {
            InitializeComponent();
        }

        private void UpdateConnectionsInfo(string[] connList)
        {
            //TODO Dropdown does not retain selected state
            var currSelect = ConnectionsDropDown.SelectedIndex == -1 ? "" : 
                ConnectionsDropDown.SelectedItem as string;
            ConnectionsDropDown.Items.Clear();
            ConnectionsDropDown.Items.AddRange(connList);
            if (!connList.Contains(currSelect))
            {
                if (ConnectionsDropDown.Items.Count > 0)
                {
                    ConnectionsDropDown.SelectedIndex = 0;
                }
                else
                {
                    ConnectionsDropDown.SelectedIndex = -1;
                    ConnectionsDropDown.Text = string.Empty;
                }
            }
        }

        public void UpdateInfo(RegionModel model)
        {
            IDInput.Text = model.ID;
            UpdateConnectionsInfo(model.Connections.Select(x => x.ID).ToArray());
        }
    }
}
