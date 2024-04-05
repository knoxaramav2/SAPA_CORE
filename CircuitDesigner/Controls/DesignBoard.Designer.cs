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
            SuspendLayout();
            // 
            // DesignBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Name = "DesignBoard";
            Size = new Size(473, 415);
            Paint += DesignBoard_Paint;
            KeyUp += DesignBoard_KeyUp;
            MouseDown += DesignBoard_MouseDown;
            MouseMove += DesignBoard_MouseMove;
            MouseUp += DesignBoard_MouseUp;
            Resize += DesignBoard_Resize;
            ResumeLayout(false);
        }

        #endregion
    }
}
