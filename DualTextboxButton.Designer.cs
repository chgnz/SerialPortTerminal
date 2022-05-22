
namespace SerialTerminal
{
    partial class DualTextboxButton
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.labelMilisec = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBoxInterval);
            this.groupBox.Controls.Add(this.labelInterval);
            this.groupBox.Controls.Add(this.labelData);
            this.groupBox.Controls.Add(this.labelMilisec);
            this.groupBox.Controls.Add(this.textBoxData);
            this.groupBox.Controls.Add(this.button);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(207, 77);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInterval.Location = new System.Drawing.Point(20, 45);
            this.textBoxInterval.Multiline = true;
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(125, 20);
            this.textBoxInterval.TabIndex = 7;
            this.textBoxInterval.Click += new System.EventHandler(this.button_Click);
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.labelInterval.Location = new System.Drawing.Point(3, 47);
            this.labelInterval.Margin = new System.Windows.Forms.Padding(0);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(14, 15);
            this.labelInterval.TabIndex = 6;
            this.labelInterval.Text = "I";
            this.labelInterval.Click += new System.EventHandler(this.button_Click);
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.labelData.Location = new System.Drawing.Point(3, 21);
            this.labelData.Margin = new System.Windows.Forms.Padding(0);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(14, 15);
            this.labelData.TabIndex = 5;
            this.labelData.Text = "T";
            this.labelData.Click += new System.EventHandler(this.button_Click);
            // 
            // labelMilisec
            // 
            this.labelMilisec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMilisec.AutoSize = true;
            this.labelMilisec.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.labelMilisec.Location = new System.Drawing.Point(151, 48);
            this.labelMilisec.Name = "labelMilisec";
            this.labelMilisec.Size = new System.Drawing.Size(21, 15);
            this.labelMilisec.TabIndex = 4;
            this.labelMilisec.Text = "ms";
            this.labelMilisec.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxData
            // 
            this.textBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxData.Location = new System.Drawing.Point(20, 19);
            this.textBoxData.Multiline = true;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(175, 20);
            this.textBoxData.TabIndex = 3;
            this.textBoxData.Click += new System.EventHandler(this.button_Click);
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button.Cursor = System.Windows.Forms.Cursors.Default;
            this.button.Location = new System.Drawing.Point(175, 45);
            this.button.Margin = new System.Windows.Forms.Padding(0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(20, 20);
            this.button.TabIndex = 2;
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // DualTextboxButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DualTextboxButton";
            this.Size = new System.Drawing.Size(207, 77);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label labelMilisec;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.TextBox textBoxInterval;
    }
}
