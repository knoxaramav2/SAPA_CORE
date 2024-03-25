namespace CircuitDesigner.Forms
{
    partial class InputOutputForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AcceptBtn = new Button();
            CancelBtn = new Button();
            label1 = new Label();
            NameInput = new TextBox();
            ConnectionList = new ListBox();
            ConnectionsLabel = new Label();
            EnabledCheckbox = new CheckBox();
            SuspendLayout();
            // 
            // AcceptBtn
            // 
            AcceptBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AcceptBtn.Location = new Point(266, 144);
            AcceptBtn.Name = "AcceptBtn";
            AcceptBtn.Size = new Size(75, 23);
            AcceptBtn.TabIndex = 0;
            AcceptBtn.Text = "Accept";
            AcceptBtn.UseVisualStyleBackColor = true;
            AcceptBtn.Click += AcceptBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelBtn.CausesValidation = false;
            CancelBtn.Location = new Point(185, 144);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 1;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "Name";
            // 
            // NameInput
            // 
            NameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NameInput.Location = new Point(57, 12);
            NameInput.Name = "NameInput";
            NameInput.Size = new Size(284, 23);
            NameInput.TabIndex = 3;
            NameInput.Validating += NameInput_Validating;
            // 
            // ConnectionList
            // 
            ConnectionList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ConnectionList.FormattingEnabled = true;
            ConnectionList.ItemHeight = 15;
            ConnectionList.Location = new Point(12, 73);
            ConnectionList.Name = "ConnectionList";
            ConnectionList.Size = new Size(167, 94);
            ConnectionList.TabIndex = 4;
            // 
            // ConnectionsLabel
            // 
            ConnectionsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ConnectionsLabel.AutoSize = true;
            ConnectionsLabel.Location = new Point(12, 55);
            ConnectionsLabel.Name = "ConnectionsLabel";
            ConnectionsLabel.Size = new Size(74, 15);
            ConnectionsLabel.TabIndex = 5;
            ConnectionsLabel.Text = "Connections";
            // 
            // EnabledCheckbox
            // 
            EnabledCheckbox.AutoSize = true;
            EnabledCheckbox.Location = new Point(185, 73);
            EnabledCheckbox.Name = "EnabledCheckbox";
            EnabledCheckbox.Size = new Size(68, 19);
            EnabledCheckbox.TabIndex = 6;
            EnabledCheckbox.Text = "Enabled";
            EnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // InputOutputForm
            // 
            AcceptButton = AcceptBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = CancelBtn;
            ClientSize = new Size(353, 179);
            Controls.Add(EnabledCheckbox);
            Controls.Add(ConnectionsLabel);
            Controls.Add(ConnectionList);
            Controls.Add(NameInput);
            Controls.Add(label1);
            Controls.Add(CancelBtn);
            Controls.Add(AcceptBtn);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputOutputForm";
            ShowIcon = false;
            Text = "Input/Output";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AcceptBtn;
        private Button CancelBtn;
        private Label label1;
        private TextBox NameInput;
        private ListBox ConnectionList;
        private Label ConnectionsLabel;
        private CheckBox EnabledCheckbox;
    }
}