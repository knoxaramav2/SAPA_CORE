namespace CircuitDesigner.Controls
{
    partial class InputSlot
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
            TransmitterList = new ComboBox();
            NameLabel = new Label();
            SuspendLayout();
            // 
            // TransmitterList
            // 
            TransmitterList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TransmitterList.FormattingEnabled = true;
            TransmitterList.Location = new Point(31, 5);
            TransmitterList.Name = "TransmitterList";
            TransmitterList.Size = new Size(289, 23);
            TransmitterList.TabIndex = 0;
            // 
            // NameLabel
            // 
            NameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(3, 8);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(22, 15);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "---";
            // 
            // InputSlot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NameLabel);
            Controls.Add(TransmitterList);
            Name = "InputSlot";
            Size = new Size(323, 35);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox TransmitterList;
        private Label NameLabel;
    }
}
