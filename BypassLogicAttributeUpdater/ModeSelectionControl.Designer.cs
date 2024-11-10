namespace BypassLogicAttributeUpdater
{
    partial class ModeSelectionControl
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
            this.modeSelectionBox = new System.Windows.Forms.CheckedListBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modeSelectionBox
            // 
            this.modeSelectionBox.CheckOnClick = true;
            this.modeSelectionBox.FormattingEnabled = true;
            this.modeSelectionBox.Items.AddRange(new object[] {
            "CustomSync",
            "CustomAsync"});
            this.modeSelectionBox.Location = new System.Drawing.Point(253, 124);
            this.modeSelectionBox.Name = "modeSelectionBox";
            this.modeSelectionBox.Size = new System.Drawing.Size(226, 88);
            this.modeSelectionBox.TabIndex = 0;
            this.modeSelectionBox.SelectedIndexChanged += new System.EventHandler(this.modeSelectionBox_SelectedIndexChanged);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(169, 303);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(114, 48);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(515, 303);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(114, 48);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ModeSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.modeSelectionBox);
            this.Name = "ModeSelectionControl";
            this.Text = "ModeSelectionControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox modeSelectionBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}