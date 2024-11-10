using System.Windows.Forms;

namespace BypassLogicAttributeUpdater
{
    partial class PluginStepSelectionControl
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.pluginStepSelectionView = new System.Windows.Forms.ListView();
            this.stepName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stepId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(953, 785);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(114, 48);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(1163, 785);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(114, 48);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // pluginStepSelectionView
            // 
            this.pluginStepSelectionView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stepName,
            this.stepId});
            //this.pluginStepSelectionView.HideSelection = false;
            this.pluginStepSelectionView.MultiSelect = true;
            this.pluginStepSelectionView.Location = new System.Drawing.Point(84, 76);
            this.pluginStepSelectionView.Name = "pluginStepSelectionView";
            this.pluginStepSelectionView.Size = new System.Drawing.Size(1212, 634);
            this.pluginStepSelectionView.TabIndex = 3;
            this.pluginStepSelectionView.UseCompatibleStateImageBehavior = false;
            this.pluginStepSelectionView.View = System.Windows.Forms.View.Details;
            this.pluginStepSelectionView.FullRowSelect = true;
            this.pluginStepSelectionView.SelectedIndexChanged += new System.EventHandler(this.pluginStepSelectionView_SelectedIndexChanged);
            // 
            // stepName
            // 
            this.stepName.Text = "Step Name";
            this.stepName.Width = 500;
            // 
            // stepId
            // 
            this.stepId.Text = "Step ID";
            this.stepId.Width = 500;
            // 
            // PluginStepSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 903);
            this.Controls.Add(this.pluginStepSelectionView);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "PluginStepSelectionControl";
            this.Text = "PluginStepSelectionControl";
            this.Load += new System.EventHandler(this.PluginStepSelectionControl_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Button okBtn;
        private Button cancelBtn;
        private ListView pluginStepSelectionView;
        private ColumnHeader stepName;
        private ColumnHeader stepId;
    }
}