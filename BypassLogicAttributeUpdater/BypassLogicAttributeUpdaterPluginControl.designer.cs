using System.Windows.Forms;

namespace BypassLogicAttributeUpdater
{
    partial class BypassLogicAttributeUpdaterPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadEntitiesBtn = new System.Windows.Forms.ToolStripButton();
            this.updateBtn = new System.Windows.Forms.ToolStripButton();
            this.listViewTables = new System.Windows.Forms.ListView();
            this.DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgView = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AttributeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttributeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterXmlBox = new System.Windows.Forms.RichTextBox();
            this.bypassSelectionBox = new System.Windows.Forms.ListBox();
            this.filterXMLLabel = new System.Windows.Forms.Label();
            this.selectBypassLabel = new System.Windows.Forms.Label();
            this.fetchXMLInfo = new System.Windows.Forms.Label();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.loadEntitiesBtn,
            this.updateBtn});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(2539, 50);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(170, 44);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // loadEntitiesBtn
            // 
            this.loadEntitiesBtn.Image = global::BypassLogicAttributeUpdater.Properties.Resources.entities;
            this.loadEntitiesBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadEntitiesBtn.Name = "loadEntitiesBtn";
            this.loadEntitiesBtn.Size = new System.Drawing.Size(177, 44);
            this.loadEntitiesBtn.Text = "Load Entities";
            this.loadEntitiesBtn.Click += new System.EventHandler(this.loadEntitiesBtn_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.Image = global::BypassLogicAttributeUpdater.Properties.Resources.paper_plane;
            this.updateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(119, 44);
            this.updateBtn.Text = "Update";
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // listViewTables
            // 
            this.listViewTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DisplayName,
            this.LogicalName});
            this.listViewTables.FullRowSelect = true;
            this.listViewTables.GridLines = true;
            this.listViewTables.HideSelection = false;
            this.listViewTables.Location = new System.Drawing.Point(49, 67);
            this.listViewTables.MultiSelect = false;
            this.listViewTables.Name = "listViewTables";
            this.listViewTables.Size = new System.Drawing.Size(600, 1220);
            this.listViewTables.TabIndex = 5;
            this.listViewTables.UseCompatibleStateImageBehavior = false;
            this.listViewTables.View = System.Windows.Forms.View.Details;
            this.listViewTables.SelectedIndexChanged += new System.EventHandler(this.listViewTables_SelectedIndexChanged);
            // 
            // DisplayName
            // 
            this.DisplayName.Text = "Display Name";
            this.DisplayName.Width = 150;
            // 
            // LogicalName
            // 
            this.LogicalName.Text = "Logical Name";
            this.LogicalName.Width = 150;
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.AttributeName,
            this.AttributeType,
            this.NewValue});
            this.dgView.Location = new System.Drawing.Point(672, 325);
            this.dgView.MultiSelect = false;
            this.dgView.Name = "dgView";
            this.dgView.RowHeadersWidth = 82;
            this.dgView.RowTemplate.Height = 33;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(1800, 962);
            this.dgView.TabIndex = 7;
            this.dgView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellContentClick);
            this.dgView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgView_CellValidating);
            // 
            // select
            // 
            this.select.HeaderText = "Select";
            this.select.MinimumWidth = 10;
            this.select.Name = "select";
            this.select.Width = 50;
            // 
            // AttributeName
            // 
            this.AttributeName.HeaderText = "Attribute Name";
            this.AttributeName.MinimumWidth = 10;
            this.AttributeName.Name = "AttributeName";
            this.AttributeName.ReadOnly = true;
            this.AttributeName.Width = 200;
            // 
            // AttributeType
            // 
            this.AttributeType.HeaderText = "Attribute Type";
            this.AttributeType.MinimumWidth = 10;
            this.AttributeType.Name = "AttributeType";
            this.AttributeType.ReadOnly = true;
            this.AttributeType.Width = 200;
            // 
            // NewValue
            // 
            this.NewValue.HeaderText = "New Value";
            this.NewValue.MinimumWidth = 10;
            this.NewValue.Name = "NewValue";
            this.NewValue.Width = 200;
            // 
            // filterXmlBox
            // 
            this.filterXmlBox.BackColor = System.Drawing.SystemColors.Window;
            this.filterXmlBox.Location = new System.Drawing.Point(672, 107);
            this.filterXmlBox.Name = "filterXmlBox";
            this.filterXmlBox.Size = new System.Drawing.Size(1010, 191);
            this.filterXmlBox.TabIndex = 8;
            this.filterXmlBox.Text = "<filter type=\"and\">\n      <condition attribute=\"accountid\" operator=\"eq\" uitype=\"" +
    "account\" value=\"{81883308-7AD5-EA11-A813-000D3A33F3B4}\" />\n</filter>";
            this.filterXmlBox.TextChanged += new System.EventHandler(this.filterXmlBox_TextChanged);
            // 
            // bypassSelectionBox
            // 
            this.bypassSelectionBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.bypassSelectionBox.FormattingEnabled = true;
            this.bypassSelectionBox.ItemHeight = 30;
            this.bypassSelectionBox.Items.AddRange(new object[] {
            "SuppressCallbackRegistrationExpanderJob",
            "BypassBusinessLogicExecution",
            "BypassBusinessLogicExecutionStepIds"});
            this.bypassSelectionBox.Location = new System.Drawing.Point(1872, 114);
            this.bypassSelectionBox.Name = "bypassSelectionBox";
            this.bypassSelectionBox.Size = new System.Drawing.Size(600, 184);
            this.bypassSelectionBox.TabIndex = 10;
            this.bypassSelectionBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.BypassSelectionBox_DrawItem);
            this.bypassSelectionBox.SelectedIndexChanged += new System.EventHandler(this.bypassSelectionBox_SelectedIndexChanged);
            // 
            // filterXMLLabel
            // 
            this.filterXMLLabel.AutoSize = true;
            this.filterXMLLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.filterXMLLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.filterXMLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterXMLLabel.Location = new System.Drawing.Point(672, 31);
            this.filterXMLLabel.Name = "filterXMLLabel";
            this.filterXMLLabel.Size = new System.Drawing.Size(136, 31);
            this.filterXMLLabel.TabIndex = 11;
            this.filterXMLLabel.Text = "Filter XML";
            // 
            // selectBypassLabel
            // 
            this.selectBypassLabel.AutoSize = true;
            this.selectBypassLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.selectBypassLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.selectBypassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBypassLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.selectBypassLabel.Location = new System.Drawing.Point(1872, 67);
            this.selectBypassLabel.Name = "selectBypassLabel";
            this.selectBypassLabel.Size = new System.Drawing.Size(180, 31);
            this.selectBypassLabel.TabIndex = 12;
            this.selectBypassLabel.Text = "Select Bypass";
            this.selectBypassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fetchXMLInfo
            // 
            this.fetchXMLInfo.AutoSize = true;
            this.fetchXMLInfo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.fetchXMLInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fetchXMLInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fetchXMLInfo.Location = new System.Drawing.Point(672, 73);
            this.fetchXMLInfo.Name = "fetchXMLInfo";
            this.fetchXMLInfo.Size = new System.Drawing.Size(930, 27);
            this.fetchXMLInfo.TabIndex = 13;
            this.fetchXMLInfo.Text = "Enter the filter section from the Fetch Xml query created by the \"Advanced Find\" " +
    "feature in CRM";
            // 
            // BypassLogicAttributeUpdaterPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fetchXMLInfo);
            this.Controls.Add(this.selectBypassLabel);
            this.Controls.Add(this.filterXMLLabel);
            this.Controls.Add(this.bypassSelectionBox);
            this.Controls.Add(this.filterXmlBox);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.listViewTables);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "BypassLogicAttributeUpdaterPluginControl";
            this.Size = new System.Drawing.Size(2539, 1304);
            this.Load += new System.EventHandler(this.BypassLogicAttributeUpdaterPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ListView listViewTables;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.RichTextBox filterXmlBox;
        private System.Windows.Forms.ToolStripButton loadEntitiesBtn;
        private System.Windows.Forms.ToolStripButton updateBtn;
        private ColumnHeader DisplayName;
        private ColumnHeader LogicalName;
        private ListBox bypassSelectionBox;
        private DataGridViewCheckBoxColumn select;
        private DataGridViewTextBoxColumn AttributeName;
        private DataGridViewTextBoxColumn AttributeType;
        private DataGridViewTextBoxColumn NewValue;
        private Label filterXMLLabel;
        private Label selectBypassLabel;
        private Label fetchXMLInfo;
    }
}
