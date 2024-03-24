﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    public partial class DesignNode : UserControl
    {
        public DesignNode()
        {
            InitializeComponent();
        }

        public virtual void MoveTo(Point pos) { }
    }
}
