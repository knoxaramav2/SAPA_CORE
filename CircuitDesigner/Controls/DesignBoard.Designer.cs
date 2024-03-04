namespace CircuitDesigner.Controls
{
    partial class DesignBoard
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
            DesignContainer = new Panel();
            SuspendLayout();
            // 
            // DesignContainer
            // 
            DesignContainer.BackColor = Color.Silver;
            DesignContainer.Dock = DockStyle.Fill;
            DesignContainer.Location = new Point(0, 0);
            DesignContainer.Name = "DesignContainer";
            DesignContainer.Size = new Size(535, 479);
            DesignContainer.TabIndex = 0;
            DesignContainer.MouseClick += DesignContainer_Click;
            DesignContainer.MouseDown += DesignContainer_MouseDown;
            DesignContainer.MouseLeave += DesignContainer_MouseLeave;
            DesignContainer.MouseMove += DesignContainer_MouseMove;
            DesignContainer.MouseUp += DesignContainer_MouseUp;
            // 
            // DesignBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(DesignContainer);
            Name = "DesignBoard";
            Size = new Size(535, 479);
            KeyDown += DesignBoard_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Panel DesignContainer;
    }
}
