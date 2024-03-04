namespace CircuitDesigner.Controls
{
    partial class NeuronTabs
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
            Tabs = new TabControl();
            PropertiesTab = new TabPage();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            ConnectionsDropDown = new ComboBox();
            label3 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            Tabs.SuspendLayout();
            PropertiesTab.SuspendLayout();
            SuspendLayout();
            // 
            // Tabs
            // 
            Tabs.Controls.Add(PropertiesTab);
            Tabs.Dock = DockStyle.Fill;
            Tabs.Location = new Point(0, 0);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(221, 262);
            Tabs.TabIndex = 0;
            // 
            // PropertiesTab
            // 
            PropertiesTab.Controls.Add(textBox3);
            PropertiesTab.Controls.Add(label4);
            PropertiesTab.Controls.Add(textBox2);
            PropertiesTab.Controls.Add(label3);
            PropertiesTab.Controls.Add(ConnectionsDropDown);
            PropertiesTab.Controls.Add(label2);
            PropertiesTab.Controls.Add(textBox1);
            PropertiesTab.Controls.Add(label1);
            PropertiesTab.Location = new Point(4, 24);
            PropertiesTab.Name = "PropertiesTab";
            PropertiesTab.Padding = new Padding(3);
            PropertiesTab.Size = new Size(213, 234);
            PropertiesTab.TabIndex = 0;
            PropertiesTab.Text = "Properties";
            PropertiesTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 6);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(30, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(177, 23);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 90);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 2;
            label2.Text = "Connections";
            // 
            // ConnectionsDropDown
            // 
            ConnectionsDropDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ConnectionsDropDown.FormattingEnabled = true;
            ConnectionsDropDown.Location = new Point(6, 108);
            ConnectionsDropDown.Name = "ConnectionsDropDown";
            ConnectionsDropDown.Size = new Size(201, 23);
            ConnectionsDropDown.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 29);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 4;
            label3.Text = "Charge";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(57, 29);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 23);
            textBox2.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 58);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 6;
            label4.Text = "Bias";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(57, 58);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 23);
            textBox3.TabIndex = 7;
            // 
            // NodeTabs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Tabs);
            Name = "NodeTabs";
            Size = new Size(221, 262);
            Load += NodeTabs_Load;
            Tabs.ResumeLayout(false);
            PropertiesTab.ResumeLayout(false);
            PropertiesTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl Tabs;
        private TabPage PropertiesTab;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private ComboBox ConnectionsDropDown;
        private Label label3;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label4;
    }
}
