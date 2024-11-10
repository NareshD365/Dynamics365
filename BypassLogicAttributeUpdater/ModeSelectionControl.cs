using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace BypassLogicAttributeUpdater
{    
    public partial class ModeSelectionControl : Form
    {
        private readonly IOrganizationService _service;
        public List<string> selectedMode = new List<string>();
        public ModeSelectionControl(IOrganizationService service)
        {
            this._service = service;
            InitializeComponent();
        }

        private void modeSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (modeSelectionBox.CheckedItems.Count > 0) {
                foreach (string checkedItem in modeSelectionBox.CheckedItems) {
                    var mode = checkedItem.ToString();
                    selectedMode.Add(mode);                
                }    
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }
    }
}
