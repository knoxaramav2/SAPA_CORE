using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public class OutputModel : INodeModel
    {
        #region Model definitions
        public OutputModel(NodeControl host, string name)
        {
            Host = host;
            Name = name;
        }

        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeTypes Type { get; set; } = NodeTypes.OUTPUT;

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Host.NodeLabel.Text = _name;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public NodeControl Host { get; set; }
        public List<INodeModel> Connections { get; set; } = [];
        #endregion

        #region INotifedPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

    }
}
