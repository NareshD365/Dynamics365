using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Messages;
using McTools.Xrm.Connection.WinForms.UserControls;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Tooling.CrmConnectControl;

namespace BypassLogicAttributeUpdater
{
    public partial class BypassLogicAttributeUpdaterPluginControl : PluginControlBase
    {
        private string schemaName = string.Empty;
        private Settings mySettings;
        public string itemText = string.Empty;
        private BackgroundWorker entityLoaderWorker;
        private BackgroundWorker attributeLoaderWorker;
        private List<string> selectedBypassIdsOrModes = null;
        private string selectedBypassType = string.Empty;
        private bool isBypassSelected = false;
        private List<Microsoft.Xrm.Sdk.Metadata.AttributeMetadata> attributesMetadata;

        public BypassLogicAttributeUpdaterPluginControl()
        {
            InitializeComponent();
        }

        private void BypassLogicAttributeUpdaterPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BypassLogicAttributeUpdaterPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }        
        private void loadEntitiesBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(loadEntities);
        }
        private void loadEntities()
        {
            if (Service != null)
            {
                // Initialize and configure BackgroundWorker
                entityLoaderWorker = new BackgroundWorker();
                entityLoaderWorker.DoWork += EntityLoaderWorker_DoWork;
                entityLoaderWorker.RunWorkerCompleted += EntityLoaderWorker_RunWorkerCompleted;
                entityLoaderWorker.WorkerSupportsCancellation = true;

                // Disable button to prevent multiple clicks
                loadEntitiesBtn.Enabled = false;

                // Start the background worker
                entityLoaderWorker.RunWorkerAsync();
            }
        }

        private void EntityLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Retrieve the entity names in the background thread
            RetrievalService retrievalService = new RetrievalService(Service);
            e.Result = retrievalService.GetAllEntityNames();
        }

        private void EntityLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Re-enable the button after work is complete
            loadEntitiesBtn.Enabled = true;

            // Handle any errors that may have occurred
            if (e.Error != null)
            {
                MessageBox.Show($"Error loading entities: {e.Error.Message}");
                return;
            }

            // Retrieve the result from the background work
            var entityNames = (List<(string displayName, string logicalName)>)e.Result;
            listViewTables.Items.Clear();

            // Populate the ListView with the retrieved entities
            foreach (var (displayName, logicalName) in entityNames)
            {
                var logicalListViewItem = new ListViewItem(displayName);
                logicalListViewItem.SubItems.Add(logicalName);
                listViewTables.Items.Add(logicalListViewItem);
            }
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (0 <= e.ColumnIndex && e.ColumnIndex < dgView.Columns.Count && 0 <= e.RowIndex && e.RowIndex < dgView.Rows.Count)
            {
                var col = dgView.Columns[e.ColumnIndex];
                var row = dgView.Rows[e.RowIndex];

                if (col.Name == "NewValue")
                {
                    var type = (string)row.Cells[1].Value;
                    var value = (string)e.FormattedValue;
                    string format = null;
                    if (!ValidateValue(type, value, out format))
                    {
                        row.ErrorText = string.Format("'{0}' is not a valid value for the type {1}. Expected format: '{2}'", value, type, format);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void listViewTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExecuteMethod(onTableSelection);
        }

        private void onTableSelection()
        {
            // Ensure a single item is selected
            if (listViewTables.SelectedItems.Count == 1)
            {
                bypassSelectionBox.SelectedItems.Clear();
                isBypassSelected = false;
                // Get the selected schema name in the main thread
                var entity = listViewTables.SelectedItems[0].SubItems[1];
                schemaName = entity.Text;

                if (Service != null)
                {
                    // Initialize and configure BackgroundWorker for attribute loading
                    attributeLoaderWorker = new BackgroundWorker();
                    attributeLoaderWorker.DoWork += AttributeLoaderWorker_DoWork;
                    attributeLoaderWorker.RunWorkerCompleted += AttributeLoaderWorker_RunWorkerCompleted;
                    attributeLoaderWorker.WorkerSupportsCancellation = true;

                    // Pass the schema name as an argument to RunWorkerAsync
                    attributeLoaderWorker.RunWorkerAsync(schemaName);
                }
            }
        }
        private void AttributeLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Retrieve the schema name from the argument
            string schemaName = e.Argument as string;

            // Retrieve attributes for the selected entity in the background
            RetrievalService retrievalService = new RetrievalService(Service);
            attributesMetadata = retrievalService.RetrieveAllAttributes(schemaName);

            // Pass the attributes as the result to RunWorkerCompleted
            e.Result = attributesMetadata;
        }

        private void AttributeLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check for any error during the background work
            if (e.Error != null)
            {
                MessageBox.Show($"Error loading attributes: {e.Error.Message}");
                return;
            }

            // Check if there are any attributes returned
            if (e.Result is List<Microsoft.Xrm.Sdk.Metadata.AttributeMetadata> attributes)
            {
                // Clear and populate dgView with the retrieved attributes
                dgView.Rows.Clear();
                foreach (var attribute in attributes)
                {
                    // Skip the row if the attribute is the Primary ID
                    if (attribute.IsPrimaryId.HasValue && attribute.IsPrimaryId.Value)
                    {
                        continue;
                    }

                    // Add customizable attributes to dgView
                    if (attribute.IsCustomizable.Value)
                    {
                        dgView.Rows.Add(false, attribute.LogicalName, attribute.AttributeType.ToString(), "");
                    }
                }
            }
        }

        private static bool ValidateValue(string type, string value, out string format)
        {
            var result = true;
            format = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                switch (type.ToLower())
                {
                    case "integer":
                        // Value should be an integer
                        format = "#0";
                        int ti;
                        if (!int.TryParse(value, out ti))
                            result = false;
                        break;
                    case "picklist":
                        // value should be a valid integer -> available in options?
                        format = "#0";
                        int tp;
                        if (!int.TryParse(value, out tp))
                            result = false;
                        break;
                    case "string":
                    case "memo":
                        // Value should be text... no validation
                        break;
                    case "money":
                    case "double":
                        // Value should be a double
                        format = "#0.#";
                        double td;
                        if (!double.TryParse(value, out td))
                            result = false;
                        break;
                    case "boolean":
                        // true or false
                        format = "true/false";
                        bool tb;
                        if (!bool.TryParse(value, out tb))
                            result = false;
                        break;
                    case "owner":
                    case "lookup":
                        // Value should be a GUID
                        format = Guid.Empty.ToString();
                        Guid tg;
                        if (!Guid.TryParse(value, out tg))
                            result = false;
                        break;
                    case "datetime":
                        // Valid date
                        format = "yyyy-MM-dd HH:mm";
                        DateTime tdt;
                        if (!DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tdt))
                            result = false;
                        break;
                }
            }

            return result;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(onUpdateBtn);
        }

        private void onUpdateBtn()
        {
            if (Service != null)
            {
                if (string.IsNullOrEmpty(schemaName))
                {
                    MessageBox.Show("Please select an entity first.");
                    return;
                }

                // Retrieve columns and filter XML before starting the background work
                string[] columns = GetCheckedAttributeNames();
                string filterXML = filterXmlBox.Text;

                if (columns.Length > 0)
                {
                    // Initialize and configure BackgroundWorker
                    BackgroundWorker updateWorker = new BackgroundWorker();
                    updateWorker.DoWork += UpdateWorker_DoWork;
                    updateWorker.RunWorkerCompleted += UpdateWorker_RunWorkerCompleted;
                    updateWorker.WorkerSupportsCancellation = true;

                    // Prepare arguments to pass into DoWork
                    var args = new Tuple<string, string[], string>(schemaName, columns, filterXML);
                    updateWorker.RunWorkerAsync(args); // Start the background worker
                }
                else
                {
                    MessageBox.Show("Please select columns to Update");
                    return;
                }
            }
        }

        // Perform background operation for updating records
        private void UpdateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Unpack arguments
            var args = (Tuple<string, string[], string>)e.Argument;
            string schemaName = args.Item1;
            string[] columns = args.Item2;
            string filterXML = args.Item3;

            try
            {
                RetrievalService retrievalService = new RetrievalService(Service);

                // Construct and retrieve records to update
                var constructedFetchXML = retrievalService.BuildFetchXml(schemaName, columns, filterXML);
                EntityCollection recordsToUpdate = retrievalService.RetrieveRecords(constructedFetchXML.InnerXml);
                if (recordsToUpdate != null && recordsToUpdate.Entities.Count > 0) {
                    // Perform the update operation
                    bool isUpdateConfirmed = UpdateRecordsInCRM(recordsToUpdate, columns);
                    if (isUpdateConfirmed)
                    {
                        e.Result = "Records updated successfully!";
                    }
                }
                else
                {
                    MessageBox.Show("There are no records present with the given query");
                }
            }
            catch (Exception ex)
            {
                e.Result = $"Error updating records: {ex.Message}";
            }
        }

        // Runs on UI thread after background work is complete
        private void UpdateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Display the result message after background work completes
            if (e.Result != null)
            {
                MessageBox.Show(e.Result.ToString());
            }
        }

        private bool UpdateRecordsInCRM(EntityCollection recordsToUpdate, string[] columns)
        {
            DialogResult confirmBox;
            if (isBypassSelected)
            {
                confirmBox = MessageBox.Show($"You are going to update {recordsToUpdate.Entities.Count} records and {columns.Length} columns", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            else
            {
                confirmBox = MessageBox.Show($"You are going to update {recordsToUpdate.Entities.Count} records and {columns.Length} columns without selecting Bypass Logic", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            if (confirmBox == DialogResult.Yes) {
                foreach (var entity in recordsToUpdate.Entities)
                {
                    Entity updateEntity = new Entity(entity.LogicalName, entity.Id);

                    foreach (var column in columns)
                    {
                        if (attributesMetadata == null && attributesMetadata.Count == 0)
                        {
                            MessageBox.Show("Unable to retrieve Attributes for specific entity");
                        }
                        var attributeMetadata = attributesMetadata.FirstOrDefault(attr => attr.LogicalName == column);
                        if (attributeMetadata == null)
                            continue; // Skip if metadata not found

                        // Get the new value for the attribute based on its type
                        object newValue = GetNewValueForAttribute(column);

                        // Set value based on attribute type
                        switch (attributeMetadata.AttributeType)
                        {
                            case AttributeTypeCode.Picklist:
                                updateEntity[column] = new OptionSetValue(Convert.ToInt32(newValue));
                                break;

                            case AttributeTypeCode.Lookup:
                                updateEntity[column] = new EntityReference(((LookupAttributeMetadata)attributeMetadata).Targets[0], (Guid)newValue);
                                break;

                            case AttributeTypeCode.Integer:
                                updateEntity[column] = Convert.ToInt32(newValue);
                                break;

                            case AttributeTypeCode.Double:
                                updateEntity[column] = Convert.ToDouble(newValue);
                                break;

                            case AttributeTypeCode.Money:
                                updateEntity[column] = new Money(Convert.ToDecimal(newValue));
                                break;

                            case AttributeTypeCode.String:
                                updateEntity[column] = Convert.ToString(newValue);
                                break;

                            case AttributeTypeCode.Boolean:
                                updateEntity[column] = Convert.ToBoolean(newValue);
                                break;

                            case AttributeTypeCode.DateTime:
                                updateEntity[column] = Convert.ToDateTime(newValue);
                                break;

                            default:
                                // Handle other attribute types or skip unsupported types
                                MessageBox.Show($"Unsupported attribute type for '{column}'");
                                continue;
                        }
                    }

                    // Set RowVersion to handle concurrency if supported by the entity
                    updateEntity.RowVersion = entity.RowVersion;

                    // Create an UpdateRequest with concurrency and optional parameters
                    UpdateRequest updateRequest = new UpdateRequest
                    {
                        Target = updateEntity,
                        ConcurrencyBehavior = ConcurrencyBehavior.IfRowVersionMatches // Ensure row version matches to avoid conflicts
                    };

                    // Add optional parameters if required
                    if (isBypassSelected)
                    {
                        if (itemText == "BypassBusinessLogicExecution")
                        {
                            updateRequest.Parameters.Add("BypassBusinessLogicExecution", selectedBypassIdsOrModes[0]); // Bypass Sync/Async Logic
                        }
                        else if(itemText == "SuppressCallbackRegistrationExpanderJob")
                        {
                            updateRequest.Parameters.Add("SuppressCallbackRegistrationExpanderJob", true); // Bypass Flows
                        }
                        else if(itemText == "BypassBusinessLogicExecutionStepIds")
                        {
                            string stepIdsString = string.Join(",", selectedBypassIdsOrModes);
                            updateRequest.Parameters.Add("BypassBusinessLogicExecutionStepIds", stepIdsString); // Bypass StepIDs
                        }
                    }

                    try
                    {
                        // Execute the update request with optional parameters and concurrency behavior
                        Service.Execute(updateRequest);
                    }
                    catch (FaultException<OrganizationServiceFault> ex)
                    {
                        string errorMessage = ex.Detail?.Message ?? "Unknown error occurred.";
                        throw new InvalidOperationException($"Error updating record with ID {entity.Id}: {errorMessage}");
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private object GetNewValueForAttribute(string attributeName)
        {
            foreach (DataGridViewRow row in dgView.Rows)
            {
                if (row.Cells["AttributeName"].Value != null)
                {
                    string currentAttributeName = row.Cells["AttributeName"].Value.ToString();
                    if (currentAttributeName.Equals(attributeName, StringComparison.OrdinalIgnoreCase))
                    {
                        var newValueCell = row.Cells["NewValue"];
                        return newValueCell != null && newValueCell.Value != null ? newValueCell.Value : DBNull.Value;
                    }
                }
            }
            return null;
        }

        public string[] GetCheckedAttributeNames()
        {
            dgView.EndEdit();
            List<string> selectedAttributeNames = new List<string>();
            foreach (DataGridViewRow row in dgView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["select"].Value) == true)
                {
                    if (row.Cells["AttributeName"].Value != null)
                    {
                        string attributeName = row.Cells["AttributeName"].Value.ToString();
                        selectedAttributeNames.Add(attributeName);
                    }
                }
            }
            return selectedAttributeNames.ToArray();
        }


        private void BypassSelectionBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                string _itemText = bypassSelectionBox.Items[e.Index].ToString();
                e.Graphics.DrawString(_itemText, e.Font, System.Drawing.Brushes.Black, e.Bounds.Left, e.Bounds.Top + 4);
            }
            e.DrawFocusRectangle();

        }

        private void bypassSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExecuteMethod(onBypassSelection);                    
        }

        private void onBypassSelection()
        {
            if (Service != null)
            {
                if (string.IsNullOrEmpty(schemaName))
                {
                    MessageBox.Show("Please select an entity first.");
                    return;
                }

                //bypassSelectionBox.Visible = true;
                if (bypassSelectionBox.SelectedItems.Count == 1)
                {
                    isBypassSelected = true;
                    itemText = bypassSelectionBox.SelectedItems[0].ToString();

                    // Initialize BackgroundWorker
                    BackgroundWorker bypassWorker = new BackgroundWorker();
                    bypassWorker.DoWork += BypassWorker_DoWork;
                    bypassWorker.RunWorkerCompleted += BypassWorker_RunWorkerCompleted;
                    bypassWorker.WorkerSupportsCancellation = true;

                    // Pass itemText to DoWork
                    bypassWorker.RunWorkerAsync(itemText); // Start the background worker
                }
            }
        }

        private void BypassWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string selectedItemText = e.Argument as string;
            e.Result = null;

            if (selectedItemText == "BypassBusinessLogicExecutionStepIds")
            {
                // Show PluginStepSelectionControl and retrieve selected step IDs
                PluginStepSelectionControl pluginStepSelectionControl = new PluginStepSelectionControl(Service, schemaName);
                var result = pluginStepSelectionControl.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Pass selected Step IDs to RunWorkerCompleted
                    e.Result = pluginStepSelectionControl.selectedStepIds.Select(id => id.ToString()).ToList();
                }
            }
            else if (selectedItemText == "BypassBusinessLogicExecution")
            {
                // Show ModeSelectionControl and retrieve selected modes
                ModeSelectionControl modeSelectionControl = new ModeSelectionControl(Service);
                var result = modeSelectionControl.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Pass selected modes to RunWorkerCompleted
                    e.Result = modeSelectionControl.selectedMode;
                }
            }
        }

        private void BypassWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Error: {e.Error.Message}");
                return;
            }

            if (itemText == "BypassBusinessLogicExecutionStepIds" && e.Result is List<string> stepIdsAsString)
            {
                selectedBypassIdsOrModes = stepIdsAsString;
                selectedBypassType = "BypassBusinessLogicExecutionStepIds";
            }
            else if (itemText == "BypassBusinessLogicExecution" && e.Result is List<string> selectedModes)
            {
                selectedBypassIdsOrModes = new List<string> { string.Join(",", selectedModes) };
                selectedBypassType = "BypassBusinessLogicExecution";
            }
        }


        private void filterXmlBox_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}