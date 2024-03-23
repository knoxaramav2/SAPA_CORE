namespace CircuitDesigner.Controls
{
    partial class RegionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            InputBar = new Panel();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // NodeLabel
            // 
            NodeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NodeLabel.Dock = DockStyle.None;
            NodeLabel.Location = new Point(57, 0);
            NodeLabel.Size = new Size(190, 264);
            NodeLabel.DoubleClick += NodeLabel_DoubleClick;
            // 
            // InputBar
            // 
            InputBar.BackColor = Color.LightBlue;
            InputBar.Dock = DockStyle.Left;
            InputBar.Location = new Point(0, 0);
            InputBar.Name = "InputBar";
            InputBar.Size = new Size(51, 264);
            InputBar.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightCoral;
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(253, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(77, 264);
            panel1.TabIndex = 2;
            // 
            // RegionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(InputBar);
            Name = "RegionControl";
            Size = new Size(330, 264);
            Controls.SetChildIndex(NodeLabel, 0);
            Controls.SetChildIndex(InputBar, 0);
            Controls.SetChildIndex(panel1, 0);
            ResumeLayout(false);
        }

        #endregion

        private Panel InputBar;
        private Panel panel1;
    }
}
