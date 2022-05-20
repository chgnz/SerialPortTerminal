namespace SerialTerminal
{
    partial class SimpleFormTerminal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
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
            this.toolStrip_shortcuts = new System.Windows.Forms.ToolStrip();
            this.tsbutton_dtrButton = new System.Windows.Forms.ToolStripButton();
            this.tsbutton_autoRetry = new System.Windows.Forms.ToolStripButton();
            this.tsbutton_clearTextbox = new System.Windows.Forms.ToolStripButton();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tab_MainTab = new System.Windows.Forms.TabPage();
            this.textbox_SendBox = new System.Windows.Forms.TextBox();
            this.textbox_ReceiveBox = new System.Windows.Forms.TextBox();
            this.comboBox_Port = new System.Windows.Forms.ComboBox();
            this.button_openSerialPort = new System.Windows.Forms.Button();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.toolStrip_shortcuts.SuspendLayout();
            this.tabContainer.SuspendLayout();
            this.tab_MainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_shortcuts
            // 
            this.toolStrip_shortcuts.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip_shortcuts.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip_shortcuts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_shortcuts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbutton_dtrButton,
            this.tsbutton_autoRetry,
            this.tsbutton_clearTextbox});
            this.toolStrip_shortcuts.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_shortcuts.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.toolStrip_shortcuts.Name = "toolStrip_shortcuts";
            this.toolStrip_shortcuts.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip_shortcuts.Size = new System.Drawing.Size(25, 561);
            this.toolStrip_shortcuts.TabIndex = 9;
            // 
            // tsbutton_dtrButton
            // 
            this.tsbutton_dtrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_dtrButton.Enabled = false;
            this.tsbutton_dtrButton.Image = global::SerialTerminal.Properties.Resources.checkGreen;
            this.tsbutton_dtrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_dtrButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbutton_dtrButton.Name = "tsbutton_dtrButton";
            this.tsbutton_dtrButton.Size = new System.Drawing.Size(22, 20);
            this.tsbutton_dtrButton.Text = "Turn Off DTR";
            // 
            // tsbutton_autoRetry
            // 
            this.tsbutton_autoRetry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_autoRetry.Enabled = false;
            this.tsbutton_autoRetry.Image = global::SerialTerminal.Properties.Resources.RefreshGreen;
            this.tsbutton_autoRetry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_autoRetry.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbutton_autoRetry.Name = "tsbutton_autoRetry";
            this.tsbutton_autoRetry.Size = new System.Drawing.Size(22, 20);
            this.tsbutton_autoRetry.Text = "autoRetry";
            // 
            // tsbutton_clearTextbox
            // 
            this.tsbutton_clearTextbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_clearTextbox.Enabled = false;
            this.tsbutton_clearTextbox.Image = global::SerialTerminal.Properties.Resources.trash;
            this.tsbutton_clearTextbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_clearTextbox.Name = "tsbutton_clearTextbox";
            this.tsbutton_clearTextbox.Size = new System.Drawing.Size(24, 20);
            this.tsbutton_clearTextbox.Text = "Clear TextBox";
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tab_MainTab);
            this.tabContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Location = new System.Drawing.Point(25, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(715, 561);
            this.tabContainer.TabIndex = 10;
            // 
            // tab_MainTab
            // 
            this.tab_MainTab.Controls.Add(this.textbox_SendBox);
            this.tab_MainTab.Controls.Add(this.textbox_ReceiveBox);
            this.tab_MainTab.Controls.Add(this.comboBox_Port);
            this.tab_MainTab.Controls.Add(this.button_openSerialPort);
            this.tab_MainTab.Controls.Add(this.comboBox_Baudrate);
            this.tab_MainTab.Location = new System.Drawing.Point(4, 22);
            this.tab_MainTab.Name = "tab_MainTab";
            this.tab_MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.tab_MainTab.Size = new System.Drawing.Size(707, 535);
            this.tab_MainTab.TabIndex = 1;
            this.tab_MainTab.Text = "Main";
            this.tab_MainTab.UseVisualStyleBackColor = true;
            // 
            // textbox_SendBox
            // 
            this.textbox_SendBox.AcceptsReturn = true;
            this.textbox_SendBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_SendBox.Enabled = false;
            this.textbox_SendBox.Location = new System.Drawing.Point(3, 509);
            this.textbox_SendBox.Name = "textbox_SendBox";
            this.textbox_SendBox.Size = new System.Drawing.Size(697, 20);
            this.textbox_SendBox.TabIndex = 1;
            // 
            // textbox_ReceiveBox
            // 
            this.textbox_ReceiveBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_ReceiveBox.Enabled = false;
            this.textbox_ReceiveBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.textbox_ReceiveBox.Location = new System.Drawing.Point(3, 34);
            this.textbox_ReceiveBox.Multiline = true;
            this.textbox_ReceiveBox.Name = "textbox_ReceiveBox";
            this.textbox_ReceiveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_ReceiveBox.Size = new System.Drawing.Size(697, 469);
            this.textbox_ReceiveBox.TabIndex = 0;
            // 
            // comboBox_Port
            // 
            this.comboBox_Port.FormattingEnabled = true;
            this.comboBox_Port.Location = new System.Drawing.Point(3, 7);
            this.comboBox_Port.Name = "comboBox_Port";
            this.comboBox_Port.Size = new System.Drawing.Size(68, 21);
            this.comboBox_Port.TabIndex = 2;
            this.comboBox_Port.DropDown += new System.EventHandler(this.comboBox_Port_DropDown);
            this.comboBox_Port.SelectedIndexChanged += new System.EventHandler(this.comboBox_Port_SelectedIndexChanged);
            this.comboBox_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_Port_KeyPress);
            // 
            // button_openSerialPort
            // 
            this.button_openSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openSerialPort.Location = new System.Drawing.Point(151, 6);
            this.button_openSerialPort.Name = "button_openSerialPort";
            this.button_openSerialPort.Size = new System.Drawing.Size(549, 21);
            this.button_openSerialPort.TabIndex = 4;
            this.button_openSerialPort.Text = "Open";
            this.button_openSerialPort.UseVisualStyleBackColor = true;
            this.button_openSerialPort.Click += new System.EventHandler(this.button_openSerialPort_Click);
            // 
            // comboBox_Baudrate
            // 
            this.comboBox_Baudrate.FormattingEnabled = true;
            this.comboBox_Baudrate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox_Baudrate.Location = new System.Drawing.Point(77, 6);
            this.comboBox_Baudrate.MaxDropDownItems = 10;
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(68, 21);
            this.comboBox_Baudrate.TabIndex = 3;
            this.comboBox_Baudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_Baudrate_SelectedIndexChanged);
            this.comboBox_Baudrate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_Baudrate_KeyUp);
            // 
            // SimpleFormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 561);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.toolStrip_shortcuts);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "SimpleFormTerminal";
            this.Text = "Terminal";
            this.toolStrip_shortcuts.ResumeLayout(false);
            this.toolStrip_shortcuts.PerformLayout();
            this.tabContainer.ResumeLayout(false);
            this.tab_MainTab.ResumeLayout(false);
            this.tab_MainTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_shortcuts;
        private System.Windows.Forms.ToolStripButton tsbutton_dtrButton;
        private System.Windows.Forms.ToolStripButton tsbutton_autoRetry;
        private System.Windows.Forms.ToolStripButton tsbutton_clearTextbox;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tab_MainTab;
        private System.Windows.Forms.TextBox textbox_SendBox;
        private System.Windows.Forms.TextBox textbox_ReceiveBox;
        private System.Windows.Forms.ComboBox comboBox_Port;
        private System.Windows.Forms.Button button_openSerialPort;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
    }
}

