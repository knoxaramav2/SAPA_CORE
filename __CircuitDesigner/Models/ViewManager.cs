using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public class ViewData
    {
        public ViewData? ParentView = null;
        private NodeControl? _view = null;
        public NodeControl Node 
        { 
            get { return _view ?? new NodeControl(); } 
            set {  _view = value; }
        }
        public Point GlobalOrigin { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ID { get; set; }
        public BindingList<NodeControl> Controls { get; } = [];
        public NodeControl? Selected { get; private set; } = null;

        public ViewData(){}

        public ViewData(
            NodeControl view,
            ViewData? parent)
        {
            Name = view.ModelName;
            ID = view.ModelID;
            GlobalOrigin = new Point(0, 0);
            Node = view;
            ParentView = parent;
        }

        public bool AddControl(NodeControl node)
        {
            if (Controls.Contains(node)) { return false; }
            Controls.Add(node);
            return true;
        }

        public bool RemoveControl(NodeControl node)
        {
            return Controls.Remove(node);
        }

        public void UnselectControl()
        {
            Selected = null;
        }

        public void SelectControl(NodeControl node)
        {
            if (! Controls.Contains(node))
            {
                return; 
            }

            Selected = node;
        }
    }

    public class ViewManager
    {
        public RegionModel? RegionHost = null;

        private readonly Dictionary<Guid, ViewData> Views = [];
        public ViewData CurrentView { get; private set; }
        public ViewData RootView { get; private set; }

        private const string ROOT_ID = "ROOT";
        private const string EMTPY_ID = "None";

        public ViewManager(DesignBoard designer)
        {
            var rootCtrl = new RegionControl(designer, null, ROOT_ID);
            var root = CreateView(rootCtrl, null, true);
            RootView = root;
            CurrentView = root;
        }

        public ViewData? SwitchView(Guid id)
        {
            if (!Views.TryGetValue(id, out ViewData? viewData))
            {
                return null;
            }

            if (viewData.Node.Model is RegionModel regionModel)
            {
                RegionHost = regionModel;
            }

            CurrentView = viewData;
            return viewData;
        }

        public ViewData CreateView( 
            NodeControl view,
            ViewData? parent,
            bool switchTo = false)
        {
            var newView = new ViewData(view, parent);
            Views.Add(view.ModelID, newView);

            if (switchTo)
            {
                CurrentView = newView;
            }
            PrintLineage();
            return newView;
        }

        private void PrintLineage()
        {
            foreach (var item in Views.Values)
            {
                Debug.WriteLine($"{item.Name}({item.ID})");
            }
        }

        public bool DeleteView(Guid id)
        {
            return Views.Remove(id);
        }

        public ViewData[] ListViews()
        {
            return Views.Values.ToArray() ?? [];
        }

        public INodeModel? GetFocused()
        {
            return CurrentView.Selected?.Model ?? RegionHost;
        }

        public ViewData? GetView(Guid id)
        {
            if (!Views.TryGetValue(id, out var view)){
                return null;
            }

            return view;
        }
    
        public string? GetRegionName()
        {
            string? ret = null;

            if (RegionHost != null) { ret = RegionHost.Name; }
            else if (CurrentView.Selected is RegionControl region)
            {
                ret = region.Model.Name;
            }

            return ret;
        }

        public bool SetRegionName(string name)
        {
            if (RegionHost != null) { RegionHost.Name = name; }
            else if (CurrentView.Selected is RegionControl region)
            {
                region.Model.Name = name;
            }
            else { return false; }

            return true;
        }
    
        public string? GetNeuronName()
        {
            var model = GetFocused() as NeuronModel;
            return model?.Name;
        }

        public bool SetNeuronName(string name)
        {
            NeuronModel? model = GetFocused() as NeuronModel;
            if (RegionHost == null || model == null) { return false; }

            model.Name = name;
            return true;
        }
    }

}
