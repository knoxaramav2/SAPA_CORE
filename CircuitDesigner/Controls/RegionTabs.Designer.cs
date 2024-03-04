namespace CircuitDesigner.Controls
{
    partial class RegionTabs
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
            Properties = new TabPage();
            ConnectionsDropDown = new ComboBox();
            label2 = new Label();
            IDInput = new TextBox();
            label1 = new Label();
            Tabs = new TabControl();
            Properties.SuspendLayout();
            Tabs.SuspendLayout();
            SuspendLayout();
            // 
            // Properties
            // 
            Properties.Controls.Add(ConnectionsDropDown);
            Properties.Controls.Add(label2);
            Properties.Controls.Add(IDInput);
            Properties.Controls.Add(label1);
            Properties.Location = new Point(4, 24);
            Properties.Name = "Properties";
            Properties.Padding = new Padding(3);
            Properties.Size = new Size(223, 267);
            Properties.TabIndex = 0;
            Properties.Text = "Properties";
            Properties.UseVisualStyleBackColor = true;
            // 
            // ConnectionsDropDown
            // 
            ConnectionsDropDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ConnectionsDropDown.FormattingEnabled = true;
            ConnectionsDropDown.Location = new Point(6, 59);
            ConnectionsDropDown.Name = "ConnectionsDropDown";
            ConnectionsDropDown.Size = new Size(211, 23);
            ConnectionsDropDown.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 41);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 2;
            label2.Text = "Connections";
            // 
            // IDInput
            // 
            IDInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            IDInput.Location = new Point(30, 6);
            IDInput.Name = "IDInput";
            IDInput.Size = new Size(187, 23);
            IDInput.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 9);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // Tabs
            // 
            Tabs.Controls.Add(Properties);
            Tabs.Dock = DockStyle.Fill;
            Tabs.Location = new Point(0, 0);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(231, 295);
            Tabs.TabIndex = 0;
            // 
            // RegionTabs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Tabs);
            Name = "RegionTabs";
            Size = new Size(231, 295);
            Properties.ResumeLayout(false);
            Properties.PerformLayout();
            Tabs.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage Properties;
        private TabControl Tabs;
        private ComboBox ConnectionsDropDown;
        private Label label2;
        private TextBox IDInput;
        private Label label1;
    }
}
