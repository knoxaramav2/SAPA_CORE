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
        RegionMode,
        NeuronMode
    }

    public class ViewData
    {
        public Point GlobalOrigin { get; set; }
        public string ID { get; private set; }
        public DesignMode ViewMode { get; private set; }
        public List<NodeControl> Controls { get; } = [];
        public NodeControl? Selected { get; private set; }

        public ViewData(string id, DesignMode viewMode)
        {
            ID = id;
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

        private readonly Dictionary<string, ViewData> Views = [];
        public ViewData CurrentView { get; private set; }

        private const string ROOT_ID = "Regions";
        private const string EMTPY_ID = "None";

        public ViewManager()
        {
            CreateView(ROOT_ID, DesignMode.RegionMode, true);
            if (CurrentView == null) { throw new Exception("Voodoo has occurred"); }
        }

        public ViewData? SwitchView(string id)
        {
            if (string.IsNullOrEmpty(id)) { id = ROOT_ID; }

            if (!Views.TryGetValue(id.ToUpper(), out ViewData? viewData))
            {
                return null;
            }

            if (viewData.ViewMode == DesignMode.RegionMode)
            { RegionHost = null; }
            else if (viewData.ViewMode == DesignMode.NeuronMode)
            { RegionHost = (RegionModel?)viewData.Selected?.Model; }

            CurrentView = viewData;
            return viewData;
        }

        public bool CreateView(string id, DesignMode mode, bool switchTo = false)
        {
            if (string.IsNullOrEmpty(id) || Views.ContainsKey(id.ToUpper())) { return false; }

            var newView = new ViewData(id, mode);
            Views.Add(id.ToUpper(), newView);

            if (switchTo)
            {
                CurrentView = newView;
            }

            return true;
        }

        public bool DeleteView(string id)
        {
            return Views.Remove(id.ToUpper());
        }

        public string[] ListViewIds()
        {
            return Views.Values.Select(x => x.ID).ToArray();
        }

        public INodeModel? GetFocused()
        {
            return CurrentView.Selected?.Model ?? RegionHost;
        }

        public ViewData? GetView(string id)
        {
            id = id.ToUpper();
            if (!Views.TryGetValue(id, out var view)){
                return null;
            }

            return view;
        }
    
        public string? GetRegionID()
        {
            string? ret = null;

            if (RegionHost != null) { ret = RegionHost.ID; }
            else if (CurrentView.Selected is RegionControl region)
            {
                ret = region.Model.ID;
            }

            return ret;
        }

        public void SetRegionID(string id)
        {
            if (RegionHost != null) { RegionHost.ID = id; }
            else if (CurrentView.Selected is RegionControl region)
            {
                region.Model.ID = id;
            }
        }
    
        public string? GetNeuronID()
        {
            var model = GetFocused() as NeuronModel;
            return model?.ID;
        }

        public bool SetNeuronID(string id)
        {
            var model = GetFocused() as NeuronModel;
            if (RegionHost == null || model == null) { return false; }

            

            return true;
        }
    }

}
