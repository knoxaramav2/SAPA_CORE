using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public enum DesignMode
    {
        Disabled,
        SystemMode,
        CircuitMode
    }

    public class ViewData
    {
        public Point GlobalOrigin { get; set; }
        public string Name { get; private set; }
        public Guid ID { get; private set; }
        public DesignMode ViewMode { get; private set; }
        public List<NodeControl> Controls { get; } = [];
        public NodeControl? Selected { get; private set; }

        public ViewData(string name, DesignMode viewMode)
        {
            Name = name;
            ID = Guid.NewGuid();
            ViewMode = viewMode;
            GlobalOrigin = new Point(0, 0);
        }

        public bool AddControl(NodeControl node)
        {
            if (ViewMode == DesignMode.Disabled)
            {
                throw new Exception("Unable to add control to disabled view.");
            }
            if (Controls.Contains(node)) { return false; }
            Controls.Add(node);
            return true;
        }

        public bool RemoveControl(NodeControl node)
        {
            if (ViewMode == DesignMode.Disabled)
            {
                throw new Exception("Unable to remove control from disabled view.");
            }
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

        private const string ROOT_ID = "Regions";
        private const string EMTPY_ID = "None";

        public ViewManager()
        {
            CreateView(ROOT_ID, DesignMode.SystemMode, true);
            if (CurrentView == null) { throw new Exception("Voodoo has occurred"); }
        }

        public ViewData? SwitchView(Guid id)
        {
            if (!Views.TryGetValue(id, out ViewData? viewData))
            {
                return null;
            }

            if (viewData.ViewMode == DesignMode.SystemMode)
            { RegionHost = null; }
            else if (viewData.ViewMode == DesignMode.CircuitMode)
            { RegionHost = (RegionModel?)viewData.Selected?.Model; }
            else { throw new NotImplementedException(); }

            CurrentView = viewData;
            return viewData;
        }

        public bool CreateView(string name, DesignMode mode, bool switchTo = false)
        {
            var id = Guid.NewGuid();
            var newView = new ViewData(name, mode);
            Views.Add(id, newView);

            if (switchTo)
            {
                CurrentView = newView;
            }

            return true;
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
    
        public string? GetRegionID()
        {
            string? ret = null;

            if (RegionHost != null) { ret = RegionHost.Name; }
            else if (CurrentView.Selected is RegionControl region)
            {
                ret = region.Model.Name;
            }

            return ret;
        }

        public void SetRegionID(string id)
        {
            if (RegionHost != null) { RegionHost.Name = id; }
            else if (CurrentView.Selected is RegionControl region)
            {
                region.Model.Name = id;
            }
        }
    
        public string? GetNeuronID()
        {
            var model = GetFocused() as NeuronModel;
            return model?.Name;
        }

        public bool SetNeuronID(string id)
        {
            var model = GetFocused() as NeuronModel;
            if (RegionHost == null || model == null) { return false; }

            

            return true;
        }
    }

}
