namespace CircuitDesigner.Controls
{
    partial class NodeControl
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
            NodeLabel = new Label();
            SuspendLayout();
            // 
            // NodeLabel
            // 
            NodeLabel.BorderStyle = BorderStyle.Fixed3D;
            NodeLabel.Dock = DockStyle.Fill;
            NodeLabel.Location = new Point(0, 0);
            NodeLabel.Name = "NodeLabel";
            NodeLabel.Size = new Size(150, 150);
            NodeLabel.TabIndex = 0;
            NodeLabel.Text = "NODE PLACEHOLDER";
            NodeLabel.TextAlign = ContentAlignment.MiddleCenter;
            NodeLabel.MouseDown += NodeLabel_MouseDown;
            NodeLabel.MouseMove += NodeLabel_MouseMove;
            NodeLabel.MouseUp += NodeLabel_MouseUp;
            // 
            // NodeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NodeLabel);
            Name = "NodeControl";
            ResumeLayout(false);
        }

        #endregion

        private Label NodeLabel;
    }
}
