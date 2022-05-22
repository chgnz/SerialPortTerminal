namespace SerialTerminal
{
    partial class FormTerminal
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
            this.components = new System.ComponentModel.Container();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbutton_dtrButton = new System.Windows.Forms.ToolStripButton();
            this.tsbutton_autoRetry = new System.Windows.Forms.ToolStripButton();
            this.tsbutton_clearTextbox = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tab_MainTab = new System.Windows.Forms.TabPage();
            this.textbox_SendBox = new System.Windows.Forms.TextBox();
            this.textbox_ReceiveBox = new System.Windows.Forms.TextBox();
            this.comboBox_Port = new System.Windows.Forms.ComboBox();
            this.button_openSerialPort = new System.Windows.Forms.Button();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.tab_gbox4FwUpd = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbutton_ScroolToCarret = new System.Windows.Forms.ToolStripButton();
            this.tsbutton_clearDebugWindow = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_debug = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_distributorID = new System.Windows.Forms.TextBox();
            this.textBox_serialNumber = new System.Windows.Forms.TextBox();
            this.textBox_PhoneNumber = new System.Windows.Forms.TextBox();
            this.textBox_settingsAddress = new System.Windows.Forms.TextBox();
            this.textBox_fwAddress = new System.Windows.Forms.TextBox();
            this.textBox_fwUpdateDataExchange = new System.Windows.Forms.TextBox();
            this.button_moveToDistro = new System.Windows.Forms.Button();
            this.label_deviceModelVersion = new System.Windows.Forms.Label();
            this.button_createNewQueclink = new System.Windows.Forms.Button();
            this.comboBox_TeltonikaModelVersion = new System.Windows.Forms.ComboBox();
            this.label_serialNumber = new System.Windows.Forms.Label();
            this.teltonikaReconfigureCheckbox = new System.Windows.Forms.CheckBox();
            this.label_IMEI = new System.Windows.Forms.Label();
            this.button_createNewTeltonika = new System.Windows.Forms.Button();
            this.label_PhoneNumber = new System.Windows.Forms.Label();
            this.button_ReconfigureQueclink = new System.Windows.Forms.Button();
            this.button_ReconfigureTeltonika = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button_openHEXFile = new System.Windows.Forms.Button();
            this.button_startFwUpdate = new System.Windows.Forms.Button();
            this.comboBox_PortFwUpdate = new System.Windows.Forms.ComboBox();
            this.comboBox_BaudrateFwUpdate = new System.Windows.Forms.ComboBox();
            this.serialPortFwUpdate = new System.IO.Ports.SerialPort(this.components);
            this.serialPortForward = new System.IO.Ports.SerialPort(this.components);
            this.serialPort_GboxTest = new System.IO.Ports.SerialPort(this.components);
            this.timer_timeout = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabContainer.SuspendLayout();
            this.tab_MainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_debug)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DtrEnable = true;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbutton_dtrButton,
            this.tsbutton_autoRetry,
            this.tsbutton_clearTextbox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(25, 561);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
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
            // 
            // tsbutton_autoRetry
            // 
            this.tsbutton_autoRetry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_autoRetry.Image = global::SerialTerminal.Properties.Resources.checkGreen;
            this.tsbutton_autoRetry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_autoRetry.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbutton_autoRetry.Name = "tsbutton_autoRetry";
            this.tsbutton_autoRetry.Size = new System.Drawing.Size(22, 20);
            this.tsbutton_autoRetry.Text = "toolStrip_autoRetry";
            // 
            // tsbutton_clearTextbox
            // 
            this.tsbutton_clearTextbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_clearTextbox.Image = global::SerialTerminal.Properties.Resources.checkGreen;
            this.tsbutton_clearTextbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_clearTextbox.Name = "tsbutton_clearTextbox";
            this.tsbutton_clearTextbox.Size = new System.Drawing.Size(24, 20);
            this.tsbutton_clearTextbox.Text = "Clear TextBox";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(25, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabContainer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(715, 561);
            this.splitContainer.SplitterDistance = 338;
            this.splitContainer.TabIndex = 8;
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tab_MainTab);
            this.tabContainer.Controls.Add(this.tab_gbox4FwUpd);
            this.tabContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Location = new System.Drawing.Point(0, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(713, 336);
            this.tabContainer.TabIndex = 0;
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
            this.tab_MainTab.Size = new System.Drawing.Size(705, 310);
            this.tab_MainTab.TabIndex = 0;
            this.tab_MainTab.Text = "Main";
            this.tab_MainTab.UseVisualStyleBackColor = true;
            // 
            // textbox_SendBox
            // 
            this.textbox_SendBox.AcceptsReturn = true;
            this.textbox_SendBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_SendBox.Location = new System.Drawing.Point(3, 284);
            this.textbox_SendBox.Name = "textbox_SendBox";
            this.textbox_SendBox.Size = new System.Drawing.Size(695, 20);
            this.textbox_SendBox.TabIndex = 1;
            // 
            // textbox_ReceiveBox
            // 
            this.textbox_ReceiveBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_ReceiveBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.textbox_ReceiveBox.Location = new System.Drawing.Point(3, 34);
            this.textbox_ReceiveBox.Multiline = true;
            this.textbox_ReceiveBox.Name = "textbox_ReceiveBox";
            this.textbox_ReceiveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_ReceiveBox.Size = new System.Drawing.Size(695, 244);
            this.textbox_ReceiveBox.TabIndex = 0;
            // 
            // comboBox_Port
            // 
            this.comboBox_Port.FormattingEnabled = true;
            this.comboBox_Port.Location = new System.Drawing.Point(3, 7);
            this.comboBox_Port.Name = "comboBox_Port";
            this.comboBox_Port.Size = new System.Drawing.Size(68, 21);
            this.comboBox_Port.TabIndex = 2;
            // 
            // button_openSerialPort
            // 
            this.button_openSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openSerialPort.Location = new System.Drawing.Point(151, 6);
            this.button_openSerialPort.Name = "button_openSerialPort";
            this.button_openSerialPort.Size = new System.Drawing.Size(547, 21);
            this.button_openSerialPort.TabIndex = 4;
            this.button_openSerialPort.Text = "Open";
            this.button_openSerialPort.UseVisualStyleBackColor = true;
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
            // 
            // tab_gbox4FwUpd
            // 
            this.tab_gbox4FwUpd.Location = new System.Drawing.Point(4, 22);
            this.tab_gbox4FwUpd.Name = "tab_gbox4FwUpd";
            this.tab_gbox4FwUpd.Size = new System.Drawing.Size(705, 310);
            this.tab_gbox4FwUpd.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(713, 217);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbutton_ScroolToCarret,
            this.tsbutton_clearDebugWindow});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(24, 217);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // tsbutton_ScroolToCarret
            // 
            this.tsbutton_ScroolToCarret.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_ScroolToCarret.Image = global::SerialTerminal.Properties.Resources.downGreen;
            this.tsbutton_ScroolToCarret.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_ScroolToCarret.Name = "tsbutton_ScroolToCarret";
            this.tsbutton_ScroolToCarret.Size = new System.Drawing.Size(23, 20);
            this.tsbutton_ScroolToCarret.Text = "Scrool To Carret";
            // 
            // tsbutton_clearDebugWindow
            // 
            this.tsbutton_clearDebugWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutton_clearDebugWindow.Image = global::SerialTerminal.Properties.Resources.trash;
            this.tsbutton_clearDebugWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutton_clearDebugWindow.Name = "tsbutton_clearDebugWindow";
            this.tsbutton_clearDebugWindow.Size = new System.Drawing.Size(23, 20);
            this.tsbutton_clearDebugWindow.Text = "clear Debug Window";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgv_debug);
            this.splitContainer2.Size = new System.Drawing.Size(684, 217);
            this.splitContainer2.SplitterDistance = 244;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgv_debug
            // 
            this.dgv_debug.AllowUserToAddRows = false;
            this.dgv_debug.AllowUserToDeleteRows = false;
            this.dgv_debug.AllowUserToResizeColumns = false;
            this.dgv_debug.AllowUserToResizeRows = false;
            this.dgv_debug.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv_debug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_debug.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgv_debug.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_debug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_debug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgv_debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_debug.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_debug.GridColor = System.Drawing.SystemColors.Window;
            this.dgv_debug.Location = new System.Drawing.Point(0, 0);
            this.dgv_debug.Name = "dgv_debug";
            this.dgv_debug.ReadOnly = true;
            this.dgv_debug.RowHeadersVisible = false;
            this.dgv_debug.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_debug.RowTemplate.Height = 18;
            this.dgv_debug.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_debug.ShowCellErrors = false;
            this.dgv_debug.ShowCellToolTips = false;
            this.dgv_debug.ShowEditingIcon = false;
            this.dgv_debug.ShowRowErrors = false;
            this.dgv_debug.Size = new System.Drawing.Size(242, 215);
            this.dgv_debug.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "debug Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // textBox_distributorID
            // 
            this.textBox_distributorID.Location = new System.Drawing.Point(0, 0);
            this.textBox_distributorID.Name = "textBox_distributorID";
            this.textBox_distributorID.Size = new System.Drawing.Size(100, 20);
            this.textBox_distributorID.TabIndex = 0;
            // 
            // textBox_serialNumber
            // 
            this.textBox_serialNumber.Location = new System.Drawing.Point(0, 0);
            this.textBox_serialNumber.Name = "textBox_serialNumber";
            this.textBox_serialNumber.Size = new System.Drawing.Size(100, 20);
            this.textBox_serialNumber.TabIndex = 0;
            // 
            // textBox_PhoneNumber
            // 
            this.textBox_PhoneNumber.Location = new System.Drawing.Point(0, 0);
            this.textBox_PhoneNumber.Name = "textBox_PhoneNumber";
            this.textBox_PhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.textBox_PhoneNumber.TabIndex = 0;
            // 
            // textBox_settingsAddress
            // 
            this.textBox_settingsAddress.Location = new System.Drawing.Point(0, 0);
            this.textBox_settingsAddress.Name = "textBox_settingsAddress";
            this.textBox_settingsAddress.Size = new System.Drawing.Size(100, 20);
            this.textBox_settingsAddress.TabIndex = 0;
            // 
            // textBox_fwAddress
            // 
            this.textBox_fwAddress.Location = new System.Drawing.Point(0, 0);
            this.textBox_fwAddress.Name = "textBox_fwAddress";
            this.textBox_fwAddress.Size = new System.Drawing.Size(100, 20);
            this.textBox_fwAddress.TabIndex = 0;
            // 
            // textBox_fwUpdateDataExchange
            // 
            this.textBox_fwUpdateDataExchange.Location = new System.Drawing.Point(0, 0);
            this.textBox_fwUpdateDataExchange.Name = "textBox_fwUpdateDataExchange";
            this.textBox_fwUpdateDataExchange.Size = new System.Drawing.Size(100, 20);
            this.textBox_fwUpdateDataExchange.TabIndex = 0;
            // 
            // button_moveToDistro
            // 
            this.button_moveToDistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_moveToDistro.Location = new System.Drawing.Point(449, 268);
            this.button_moveToDistro.Name = "button_moveToDistro";
            this.button_moveToDistro.Size = new System.Drawing.Size(250, 23);
            this.button_moveToDistro.TabIndex = 25;
            this.button_moveToDistro.Text = "move devices to other distributor";
            this.button_moveToDistro.UseVisualStyleBackColor = true;
            // 
            // label_deviceModelVersion
            // 
            this.label_deviceModelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_deviceModelVersion.AutoSize = true;
            this.label_deviceModelVersion.Location = new System.Drawing.Point(577, 146);
            this.label_deviceModelVersion.Name = "label_deviceModelVersion";
            this.label_deviceModelVersion.Size = new System.Drawing.Size(82, 13);
            this.label_deviceModelVersion.TabIndex = 24;
            this.label_deviceModelVersion.Text = "unknown model";
            // 
            // button_createNewQueclink
            // 
            this.button_createNewQueclink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_createNewQueclink.Location = new System.Drawing.Point(580, 89);
            this.button_createNewQueclink.Name = "button_createNewQueclink";
            this.button_createNewQueclink.Size = new System.Drawing.Size(118, 23);
            this.button_createNewQueclink.TabIndex = 23;
            this.button_createNewQueclink.Text = "create queclink";
            this.button_createNewQueclink.UseVisualStyleBackColor = true;
            // 
            // comboBox_TeltonikaModelVersion
            // 
            this.comboBox_TeltonikaModelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_TeltonikaModelVersion.FormattingEnabled = true;
            this.comboBox_TeltonikaModelVersion.Items.AddRange(new object[] {
            "FM1100",
            "FM1200"});
            this.comboBox_TeltonikaModelVersion.Location = new System.Drawing.Point(449, 118);
            this.comboBox_TeltonikaModelVersion.Name = "comboBox_TeltonikaModelVersion";
            this.comboBox_TeltonikaModelVersion.Size = new System.Drawing.Size(118, 21);
            this.comboBox_TeltonikaModelVersion.TabIndex = 12;
            // 
            // label_serialNumber
            // 
            this.label_serialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_serialNumber.AutoSize = true;
            this.label_serialNumber.Location = new System.Drawing.Point(504, 202);
            this.label_serialNumber.Name = "label_serialNumber";
            this.label_serialNumber.Size = new System.Drawing.Size(70, 13);
            this.label_serialNumber.TabIndex = 22;
            this.label_serialNumber.Text = "SerialNumber";
            // 
            // teltonikaReconfigureCheckbox
            // 
            this.teltonikaReconfigureCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.teltonikaReconfigureCheckbox.AutoSize = true;
            this.teltonikaReconfigureCheckbox.Location = new System.Drawing.Point(449, 145);
            this.teltonikaReconfigureCheckbox.Name = "teltonikaReconfigureCheckbox";
            this.teltonikaReconfigureCheckbox.Size = new System.Drawing.Size(79, 17);
            this.teltonikaReconfigureCheckbox.TabIndex = 21;
            this.teltonikaReconfigureCheckbox.Text = "ignore ACK";
            this.teltonikaReconfigureCheckbox.UseVisualStyleBackColor = true;
            // 
            // label_IMEI
            // 
            this.label_IMEI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_IMEI.AutoSize = true;
            this.label_IMEI.Location = new System.Drawing.Point(577, 121);
            this.label_IMEI.Name = "label_IMEI";
            this.label_IMEI.Size = new System.Drawing.Size(68, 13);
            this.label_IMEI.TabIndex = 20;
            this.label_IMEI.Text = "IMEI Not Set";
            // 
            // button_createNewTeltonika
            // 
            this.button_createNewTeltonika.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_createNewTeltonika.Location = new System.Drawing.Point(449, 89);
            this.button_createNewTeltonika.Name = "button_createNewTeltonika";
            this.button_createNewTeltonika.Size = new System.Drawing.Size(118, 23);
            this.button_createNewTeltonika.TabIndex = 13;
            this.button_createNewTeltonika.Text = "create teltonika";
            this.button_createNewTeltonika.UseVisualStyleBackColor = true;
            // 
            // label_PhoneNumber
            // 
            this.label_PhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_PhoneNumber.AutoSize = true;
            this.label_PhoneNumber.Location = new System.Drawing.Point(499, 176);
            this.label_PhoneNumber.Name = "label_PhoneNumber";
            this.label_PhoneNumber.Size = new System.Drawing.Size(75, 13);
            this.label_PhoneNumber.TabIndex = 16;
            this.label_PhoneNumber.Text = "PhoneNumber";
            // 
            // button_ReconfigureQueclink
            // 
            this.button_ReconfigureQueclink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ReconfigureQueclink.Location = new System.Drawing.Point(580, 60);
            this.button_ReconfigureQueclink.Name = "button_ReconfigureQueclink";
            this.button_ReconfigureQueclink.Size = new System.Drawing.Size(118, 23);
            this.button_ReconfigureQueclink.TabIndex = 9;
            this.button_ReconfigureQueclink.Text = "reconfigure queclink";
            this.button_ReconfigureQueclink.UseVisualStyleBackColor = true;
            // 
            // button_ReconfigureTeltonika
            // 
            this.button_ReconfigureTeltonika.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ReconfigureTeltonika.Location = new System.Drawing.Point(449, 60);
            this.button_ReconfigureTeltonika.Name = "button_ReconfigureTeltonika";
            this.button_ReconfigureTeltonika.Size = new System.Drawing.Size(118, 20);
            this.button_ReconfigureTeltonika.TabIndex = 8;
            this.button_ReconfigureTeltonika.Text = "reconfigure teltonika fm11";
            this.button_ReconfigureTeltonika.UseVisualStyleBackColor = true;
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(154, 31);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(72, 23);
            this.button23.TabIndex = 6;
            this.button23.Text = "Load HEX";
            this.button23.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(80, 32);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(68, 22);
            this.button22.TabIndex = 5;
            this.button22.Text = "settings upd";
            this.button22.UseVisualStyleBackColor = true;
            // 
            // button_openHEXFile
            // 
            this.button_openHEXFile.Location = new System.Drawing.Point(154, 3);
            this.button_openHEXFile.Name = "button_openHEXFile";
            this.button_openHEXFile.Size = new System.Drawing.Size(72, 23);
            this.button_openHEXFile.TabIndex = 3;
            this.button_openHEXFile.Text = "Load HEX";
            this.button_openHEXFile.UseVisualStyleBackColor = true;
            // 
            // button_startFwUpdate
            // 
            this.button_startFwUpdate.Location = new System.Drawing.Point(80, 4);
            this.button_startFwUpdate.Name = "button_startFwUpdate";
            this.button_startFwUpdate.Size = new System.Drawing.Size(68, 22);
            this.button_startFwUpdate.TabIndex = 2;
            this.button_startFwUpdate.Text = "fw update";
            this.button_startFwUpdate.UseVisualStyleBackColor = true;
            // 
            // comboBox_PortFwUpdate
            // 
            this.comboBox_PortFwUpdate.FormattingEnabled = true;
            this.comboBox_PortFwUpdate.Location = new System.Drawing.Point(6, 6);
            this.comboBox_PortFwUpdate.Name = "comboBox_PortFwUpdate";
            this.comboBox_PortFwUpdate.Size = new System.Drawing.Size(68, 21);
            this.comboBox_PortFwUpdate.TabIndex = 0;
            // 
            // comboBox_BaudrateFwUpdate
            // 
            this.comboBox_BaudrateFwUpdate.FormattingEnabled = true;
            this.comboBox_BaudrateFwUpdate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox_BaudrateFwUpdate.Location = new System.Drawing.Point(6, 31);
            this.comboBox_BaudrateFwUpdate.MaxDropDownItems = 10;
            this.comboBox_BaudrateFwUpdate.Name = "comboBox_BaudrateFwUpdate";
            this.comboBox_BaudrateFwUpdate.Size = new System.Drawing.Size(68, 21);
            this.comboBox_BaudrateFwUpdate.TabIndex = 1;
            this.comboBox_BaudrateFwUpdate.Text = "9600";
            // 
            // serialPortForward
            // 
            this.serialPortForward.BaudRate = 115200;
            this.serialPortForward.DtrEnable = true;
            this.serialPortForward.PortName = "COM2";
            this.serialPortForward.RtsEnable = true;
            // 
            // FormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 561);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormTerminal";
            this.Text = "Terminal";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabContainer.ResumeLayout(false);
            this.tab_MainTab.ResumeLayout(false);
            this.tab_MainTab.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_debug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbutton_dtrButton;
        private System.Windows.Forms.ToolStripButton tsbutton_autoRetry;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.IO.Ports.SerialPort serialPortFwUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_debug;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ToolStripButton tsbutton_ScroolToCarret;
        private System.Windows.Forms.ToolStripButton tsbutton_clearDebugWindow;
        private System.IO.Ports.SerialPort serialPortForward;
        private System.Windows.Forms.ToolStripButton tsbutton_clearTextbox;
        private System.IO.Ports.SerialPort serialPort_GboxTest;
        private System.Windows.Forms.Timer timer_timeout;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tab_MainTab;
        private System.Windows.Forms.TextBox textbox_SendBox;
        private System.Windows.Forms.TextBox textbox_ReceiveBox;
        private System.Windows.Forms.ComboBox comboBox_Port;
        private System.Windows.Forms.Button button_openSerialPort;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        private System.Windows.Forms.TabPage tab_gbox4FwUpd;
        private System.Windows.Forms.TextBox textBox_distributorID;
        private System.Windows.Forms.TextBox textBox_serialNumber;
        private System.Windows.Forms.TextBox textBox_PhoneNumber;
        private System.Windows.Forms.TextBox textBox_settingsAddress;
        private System.Windows.Forms.TextBox textBox_fwAddress;
        private System.Windows.Forms.TextBox textBox_fwUpdateDataExchange;
        private System.Windows.Forms.Button button_moveToDistro;
        private System.Windows.Forms.Label label_deviceModelVersion;
        private System.Windows.Forms.Button button_createNewQueclink;
        private System.Windows.Forms.ComboBox comboBox_TeltonikaModelVersion;
        private System.Windows.Forms.Label label_serialNumber;
        private System.Windows.Forms.CheckBox teltonikaReconfigureCheckbox;
        private System.Windows.Forms.Label label_IMEI;
        private System.Windows.Forms.Button button_createNewTeltonika;
        private System.Windows.Forms.Label label_PhoneNumber;
        private System.Windows.Forms.Button button_ReconfigureQueclink;
        private System.Windows.Forms.Button button_ReconfigureTeltonika;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button_openHEXFile;
        private System.Windows.Forms.Button button_startFwUpdate;
        private System.Windows.Forms.ComboBox comboBox_PortFwUpdate;
        private System.Windows.Forms.ComboBox comboBox_BaudrateFwUpdate;

    }
}

