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
            this.WindowsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tab_MainTab = new System.Windows.Forms.TabPage();
            this.textbox_SendBox = new System.Windows.Forms.TextBox();
            this.textbox_ReceiveBox = new System.Windows.Forms.TextBox();
            this.comboBox_Port = new System.Windows.Forms.ComboBox();
            this.button_openSerialPort = new System.Windows.Forms.Button();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.PresetsTabControl = new System.Windows.Forms.TabControl();
            this.PresetsTabPage = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxNewLineFormat = new System.Windows.Forms.GroupBox();
            this.radioButtonNone = new System.Windows.Forms.RadioButton();
            this.radioButton_RN = new System.Windows.Forms.RadioButton();
            this.radioButton_R = new System.Windows.Forms.RadioButton();
            this.radioButton_N = new System.Windows.Forms.RadioButton();
            this.PeriodicDataTxControl = new SerialTerminal.DualTextboxButton();
            this.textboxButton8 = new SerialTerminal.TextboxButton();
            this.textboxButton7 = new SerialTerminal.TextboxButton();
            this.textboxButton6 = new SerialTerminal.TextboxButton();
            this.textboxButton5 = new SerialTerminal.TextboxButton();
            this.textboxButton4 = new SerialTerminal.TextboxButton();
            this.textboxButton3 = new SerialTerminal.TextboxButton();
            this.textboxButton2 = new SerialTerminal.TextboxButton();
            this.textboxButton1 = new SerialTerminal.TextboxButton();
            this.toolStrip_shortcuts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsSplitContainer)).BeginInit();
            this.WindowsSplitContainer.Panel1.SuspendLayout();
            this.WindowsSplitContainer.Panel2.SuspendLayout();
            this.WindowsSplitContainer.SuspendLayout();
            this.tabContainer.SuspendLayout();
            this.tab_MainTab.SuspendLayout();
            this.PresetsTabControl.SuspendLayout();
            this.PresetsTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxNewLineFormat.SuspendLayout();
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
            this.toolStrip_shortcuts.Size = new System.Drawing.Size(25, 517);
            this.toolStrip_shortcuts.TabIndex = 9;
            // 
            // tsbutton_dtrButton
            // 
            this.tsbutton_dtrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_dtrButton.Image = global::SerialTerminal.Properties.Resources.checkGreen;
            this.tsbutton_dtrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_dtrButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbutton_dtrButton.Name = "tsbutton_dtrButton";
            this.tsbutton_dtrButton.Size = new System.Drawing.Size(22, 20);
            this.tsbutton_dtrButton.Text = "Turn Off DTR";
            this.tsbutton_dtrButton.Click += new System.EventHandler(this.tsbutton_dtrButton_Click);
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
            this.tsbutton_clearTextbox.Image = global::SerialTerminal.Properties.Resources.trash;
            this.tsbutton_clearTextbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_clearTextbox.Name = "tsbutton_clearTextbox";
            this.tsbutton_clearTextbox.Size = new System.Drawing.Size(24, 20);
            this.tsbutton_clearTextbox.Text = "Clear TextBox";
            this.tsbutton_clearTextbox.Click += new System.EventHandler(this.tsbutton_clearTextbox_Click);
            // 
            // WindowsSplitContainer
            // 
            this.WindowsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.WindowsSplitContainer.Location = new System.Drawing.Point(25, 0);
            this.WindowsSplitContainer.Name = "WindowsSplitContainer";
            // 
            // WindowsSplitContainer.Panel1
            // 
            this.WindowsSplitContainer.Panel1.Controls.Add(this.tabContainer);
            // 
            // WindowsSplitContainer.Panel2
            // 
            this.WindowsSplitContainer.Panel2.Controls.Add(this.PresetsTabControl);
            this.WindowsSplitContainer.Size = new System.Drawing.Size(671, 517);
            this.WindowsSplitContainer.SplitterDistance = 506;
            this.WindowsSplitContainer.TabIndex = 11;
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tab_MainTab);
            this.tabContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.tabContainer.Location = new System.Drawing.Point(0, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(506, 517);
            this.tabContainer.TabIndex = 11;
            // 
            // tab_MainTab
            // 
            this.tab_MainTab.Controls.Add(this.textbox_SendBox);
            this.tab_MainTab.Controls.Add(this.textbox_ReceiveBox);
            this.tab_MainTab.Controls.Add(this.comboBox_Port);
            this.tab_MainTab.Controls.Add(this.button_openSerialPort);
            this.tab_MainTab.Controls.Add(this.comboBox_Baudrate);
            this.tab_MainTab.Location = new System.Drawing.Point(4, 24);
            this.tab_MainTab.Name = "tab_MainTab";
            this.tab_MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.tab_MainTab.Size = new System.Drawing.Size(498, 489);
            this.tab_MainTab.TabIndex = 1;
            this.tab_MainTab.Text = "Main";
            this.tab_MainTab.UseVisualStyleBackColor = true;
            // 
            // textbox_SendBox
            // 
            this.textbox_SendBox.AcceptsReturn = true;
            this.textbox_SendBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_SendBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textbox_SendBox.Location = new System.Drawing.Point(4, 458);
            this.textbox_SendBox.Name = "textbox_SendBox";
            this.textbox_SendBox.Size = new System.Drawing.Size(486, 23);
            this.textbox_SendBox.TabIndex = 1;
            this.textbox_SendBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textbox_SendBox_KeyUp);
            // 
            // textbox_ReceiveBox
            // 
            this.textbox_ReceiveBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_ReceiveBox.BackColor = System.Drawing.SystemColors.Window;
            this.textbox_ReceiveBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textbox_ReceiveBox.Location = new System.Drawing.Point(6, 36);
            this.textbox_ReceiveBox.Multiline = true;
            this.textbox_ReceiveBox.Name = "textbox_ReceiveBox";
            this.textbox_ReceiveBox.ReadOnly = true;
            this.textbox_ReceiveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_ReceiveBox.Size = new System.Drawing.Size(486, 416);
            this.textbox_ReceiveBox.TabIndex = 0;
            // 
            // comboBox_Port
            // 
            this.comboBox_Port.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.comboBox_Port.FormattingEnabled = true;
            this.comboBox_Port.Location = new System.Drawing.Point(6, 7);
            this.comboBox_Port.Name = "comboBox_Port";
            this.comboBox_Port.Size = new System.Drawing.Size(68, 23);
            this.comboBox_Port.TabIndex = 2;
            this.comboBox_Port.SelectedIndexChanged += new System.EventHandler(this.comboBox_Port_SelectedIndexChanged);
            // 
            // button_openSerialPort
            // 
            this.button_openSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openSerialPort.FlatAppearance.BorderSize = 0;
            this.button_openSerialPort.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button_openSerialPort.Location = new System.Drawing.Point(151, 7);
            this.button_openSerialPort.Margin = new System.Windows.Forms.Padding(0);
            this.button_openSerialPort.Name = "button_openSerialPort";
            this.button_openSerialPort.Size = new System.Drawing.Size(341, 23);
            this.button_openSerialPort.TabIndex = 4;
            this.button_openSerialPort.Text = "Open";
            this.button_openSerialPort.UseVisualStyleBackColor = false;
            this.button_openSerialPort.Click += new System.EventHandler(this.button_openSerialPort_Click);
            // 
            // comboBox_Baudrate
            // 
            this.comboBox_Baudrate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
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
            this.comboBox_Baudrate.Location = new System.Drawing.Point(80, 7);
            this.comboBox_Baudrate.MaxDropDownItems = 10;
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(68, 23);
            this.comboBox_Baudrate.TabIndex = 3;
            this.comboBox_Baudrate.Text = "9600";
            this.comboBox_Baudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_Baudrate_SelectedIndexChanged);
            this.comboBox_Baudrate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_Baudrate_KeyUp);
            // 
            // PresetsTabControl
            // 
            this.PresetsTabControl.Controls.Add(this.PresetsTabPage);
            this.PresetsTabControl.Controls.Add(this.tabPage1);
            this.PresetsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PresetsTabControl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.PresetsTabControl.ItemSize = new System.Drawing.Size(20, 20);
            this.PresetsTabControl.Location = new System.Drawing.Point(0, 0);
            this.PresetsTabControl.Name = "PresetsTabControl";
            this.PresetsTabControl.SelectedIndex = 0;
            this.PresetsTabControl.Size = new System.Drawing.Size(161, 517);
            this.PresetsTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.PresetsTabControl.TabIndex = 57;
            // 
            // PresetsTabPage
            // 
            this.PresetsTabPage.Controls.Add(this.PeriodicDataTxControl);
            this.PresetsTabPage.Controls.Add(this.textboxButton8);
            this.PresetsTabPage.Controls.Add(this.textboxButton7);
            this.PresetsTabPage.Controls.Add(this.textboxButton6);
            this.PresetsTabPage.Controls.Add(this.textboxButton5);
            this.PresetsTabPage.Controls.Add(this.textboxButton4);
            this.PresetsTabPage.Controls.Add(this.textboxButton3);
            this.PresetsTabPage.Controls.Add(this.textboxButton2);
            this.PresetsTabPage.Controls.Add(this.textboxButton1);
            this.PresetsTabPage.Location = new System.Drawing.Point(4, 24);
            this.PresetsTabPage.Name = "PresetsTabPage";
            this.PresetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PresetsTabPage.Size = new System.Drawing.Size(153, 489);
            this.PresetsTabPage.TabIndex = 0;
            this.PresetsTabPage.Text = "P";
            this.PresetsTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxNewLineFormat);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(153, 489);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "S";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxNewLineFormat
            // 
            this.groupBoxNewLineFormat.Controls.Add(this.radioButtonNone);
            this.groupBoxNewLineFormat.Controls.Add(this.radioButton_RN);
            this.groupBoxNewLineFormat.Controls.Add(this.radioButton_R);
            this.groupBoxNewLineFormat.Controls.Add(this.radioButton_N);
            this.groupBoxNewLineFormat.Location = new System.Drawing.Point(6, 3);
            this.groupBoxNewLineFormat.Name = "groupBoxNewLineFormat";
            this.groupBoxNewLineFormat.Size = new System.Drawing.Size(144, 121);
            this.groupBoxNewLineFormat.TabIndex = 57;
            this.groupBoxNewLineFormat.TabStop = false;
            this.groupBoxNewLineFormat.Text = "Line Endings";
            // 
            // radioButtonNone
            // 
            this.radioButtonNone.AutoSize = true;
            this.radioButtonNone.Location = new System.Drawing.Point(6, 19);
            this.radioButtonNone.Name = "radioButtonNone";
            this.radioButtonNone.Size = new System.Drawing.Size(67, 19);
            this.radioButtonNone.TabIndex = 57;
            this.radioButtonNone.Text = "\'none\'";
            this.radioButtonNone.UseVisualStyleBackColor = true;
            // 
            // radioButton_RN
            // 
            this.radioButton_RN.AutoSize = true;
            this.radioButton_RN.Location = new System.Drawing.Point(6, 88);
            this.radioButton_RN.Name = "radioButton_RN";
            this.radioButton_RN.Size = new System.Drawing.Size(137, 19);
            this.radioButton_RN.TabIndex = 55;
            this.radioButton_RN.TabStop = true;
            this.radioButton_RN.Text = "\'\\r\\n\' (Windows)";
            this.radioButton_RN.UseVisualStyleBackColor = true;
            // 
            // radioButton_R
            // 
            this.radioButton_R.AutoSize = true;
            this.radioButton_R.Location = new System.Drawing.Point(6, 42);
            this.radioButton_R.Name = "radioButton_R";
            this.radioButton_R.Size = new System.Drawing.Size(60, 19);
            this.radioButton_R.TabIndex = 53;
            this.radioButton_R.Text = "\'\\r\' ";
            this.radioButton_R.UseVisualStyleBackColor = true;
            // 
            // radioButton_N
            // 
            this.radioButton_N.AutoSize = true;
            this.radioButton_N.Location = new System.Drawing.Point(6, 65);
            this.radioButton_N.Name = "radioButton_N";
            this.radioButton_N.Size = new System.Drawing.Size(109, 19);
            this.radioButton_N.TabIndex = 54;
            this.radioButton_N.Text = "\'\\n\' (Linux)";
            this.radioButton_N.UseVisualStyleBackColor = true;
            // 
            // PeriodicDataTxControl
            // 
            this.PeriodicDataTxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PeriodicDataTxControl.Data = "";
            this.PeriodicDataTxControl.GroupBoxText = "Periodic Tx";
            this.PeriodicDataTxControl.Interval = "";
            this.PeriodicDataTxControl.Location = new System.Drawing.Point(3, 401);
            this.PeriodicDataTxControl.Margin = new System.Windows.Forms.Padding(0);
            this.PeriodicDataTxControl.Name = "PeriodicDataTxControl";
            this.PeriodicDataTxControl.Size = new System.Drawing.Size(144, 80);
            this.PeriodicDataTxControl.TabIndex = 5;
            this.PeriodicDataTxControl.Click += new System.EventHandler(this.PeriodicTransmitControl_Click);
            // 
            // textboxButton8
            // 
            this.textboxButton8.Location = new System.Drawing.Point(3, 248);
            this.textboxButton8.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton8.Name = "textboxButton8";
            this.textboxButton8.Size = new System.Drawing.Size(144, 35);
            this.textboxButton8.TabIndex = 64;
            this.textboxButton8.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton7
            // 
            this.textboxButton7.Location = new System.Drawing.Point(3, 213);
            this.textboxButton7.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton7.Name = "textboxButton7";
            this.textboxButton7.Size = new System.Drawing.Size(144, 35);
            this.textboxButton7.TabIndex = 63;
            this.textboxButton7.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton6
            // 
            this.textboxButton6.Location = new System.Drawing.Point(3, 178);
            this.textboxButton6.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton6.Name = "textboxButton6";
            this.textboxButton6.Size = new System.Drawing.Size(144, 35);
            this.textboxButton6.TabIndex = 62;
            this.textboxButton6.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton5
            // 
            this.textboxButton5.Location = new System.Drawing.Point(3, 143);
            this.textboxButton5.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton5.Name = "textboxButton5";
            this.textboxButton5.Size = new System.Drawing.Size(144, 35);
            this.textboxButton5.TabIndex = 61;
            this.textboxButton5.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton4
            // 
            this.textboxButton4.Location = new System.Drawing.Point(3, 108);
            this.textboxButton4.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton4.Name = "textboxButton4";
            this.textboxButton4.Size = new System.Drawing.Size(144, 35);
            this.textboxButton4.TabIndex = 60;
            this.textboxButton4.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton3
            // 
            this.textboxButton3.Location = new System.Drawing.Point(3, 73);
            this.textboxButton3.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton3.Name = "textboxButton3";
            this.textboxButton3.Size = new System.Drawing.Size(144, 35);
            this.textboxButton3.TabIndex = 59;
            this.textboxButton3.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton2
            // 
            this.textboxButton2.Location = new System.Drawing.Point(3, 38);
            this.textboxButton2.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton2.Name = "textboxButton2";
            this.textboxButton2.Size = new System.Drawing.Size(144, 35);
            this.textboxButton2.TabIndex = 58;
            this.textboxButton2.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // textboxButton1
            // 
            this.textboxButton1.Location = new System.Drawing.Point(3, 3);
            this.textboxButton1.Margin = new System.Windows.Forms.Padding(0);
            this.textboxButton1.Name = "textboxButton1";
            this.textboxButton1.Size = new System.Drawing.Size(144, 35);
            this.textboxButton1.TabIndex = 57;
            this.textboxButton1.Click += new System.EventHandler(this.textboxButtonClickHandler);
            // 
            // SimpleFormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 517);
            this.Controls.Add(this.WindowsSplitContainer);
            this.Controls.Add(this.toolStrip_shortcuts);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "SimpleFormTerminal";
            this.Text = "Terminal";
            this.toolStrip_shortcuts.ResumeLayout(false);
            this.toolStrip_shortcuts.PerformLayout();
            this.WindowsSplitContainer.Panel1.ResumeLayout(false);
            this.WindowsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsSplitContainer)).EndInit();
            this.WindowsSplitContainer.ResumeLayout(false);
            this.tabContainer.ResumeLayout(false);
            this.tab_MainTab.ResumeLayout(false);
            this.tab_MainTab.PerformLayout();
            this.PresetsTabControl.ResumeLayout(false);
            this.PresetsTabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxNewLineFormat.ResumeLayout(false);
            this.groupBoxNewLineFormat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_shortcuts;
        private System.Windows.Forms.ToolStripButton tsbutton_dtrButton;
        private System.Windows.Forms.ToolStripButton tsbutton_autoRetry;
        private System.Windows.Forms.ToolStripButton tsbutton_clearTextbox;
        private System.Windows.Forms.SplitContainer WindowsSplitContainer;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabControl PresetsTabControl;
        private System.Windows.Forms.TabPage PresetsTabPage;
        private System.Windows.Forms.TabPage tab_MainTab;
        private System.Windows.Forms.TextBox textbox_SendBox;
        private System.Windows.Forms.TextBox textbox_ReceiveBox;
        private System.Windows.Forms.ComboBox comboBox_Port;
        private System.Windows.Forms.Button button_openSerialPort;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        private TextboxButton textboxButton5;
        private TextboxButton textboxButton4;
        private TextboxButton textboxButton3;
        private TextboxButton textboxButton2;
        private TextboxButton textboxButton1;
        private TextboxButton textboxButton8;
        private TextboxButton textboxButton7;
        private TextboxButton textboxButton6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBoxNewLineFormat;
        private System.Windows.Forms.RadioButton radioButtonNone;
        private System.Windows.Forms.RadioButton radioButton_RN;
        private System.Windows.Forms.RadioButton radioButton_R;
        private System.Windows.Forms.RadioButton radioButton_N;
        private DualTextboxButton PeriodicDataTxControl;
    }
}

