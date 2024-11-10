using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BypassLogicAttributeUpdater
{
    public partial class PluginStepSelectionControl : Form
    {
        private readonly IOrganizationService _service;
        private readonly string schemaName;
        public List<Guid> selectedStepIds = new List<Guid>();
        public PluginStepSelectionControl(IOrganizationService service, string schemaName)
        {
            this._service = service;
            InitializeComponent();
            this.schemaName = schemaName;
        }

        private void pluginStepSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PluginStepSelectionControl_Load(object sender, EventArgs e)
        {
            PopulatePluginSteps();
        }
        private void PopulatePluginSteps()
        {
            RetrievalService retrieval = new RetrievalService(_service);
            List<(string, string)> steps = retrieval.GetAllPluginSteps(schemaName);
            pluginStepSelectionView.Items.Clear();
            foreach (var (stepName, stepId) in steps)
            {
                ListViewItem item = new ListViewItem(stepName);
                item.SubItems.Add(stepId);
                pluginStepSelectionView.Items.Add(item);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (pluginStepSelectionView.SelectedItems.Count > 0)
            {
                var selectedItems = pluginStepSelectionView.SelectedItems;

                foreach (ListViewItem selectedItem in selectedItems)
                {
                    if (Guid.TryParse(selectedItem.SubItems[1].Text, out Guid stepId))
                    {
                        selectedStepIds.Add(stepId);
                    }
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void pluginStepSelectionView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
