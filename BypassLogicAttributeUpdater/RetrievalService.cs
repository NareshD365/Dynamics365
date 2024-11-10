using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Xml;

namespace BypassLogicAttributeUpdater
{
    public class RetrievalService
    {
        private IOrganizationService Service { get; set; }
        public RetrievalService(IOrganizationService service) {
            this.Service =  service;
        }

        public List<(string DisplayName, string LogicalName)> GetAllEntityNames()
        {
            List<(string DisplayName, string LogicalName)> entityNames = new List<(string, string)>(); 
            RetrieveAllEntitiesRequest metaDataRequest = new RetrieveAllEntitiesRequest();
            metaDataRequest.EntityFilters = EntityFilters.Entity;

            RetrieveAllEntitiesResponse metaDataResponse = (RetrieveAllEntitiesResponse)Service.Execute(metaDataRequest);

            foreach (var entity in metaDataResponse.EntityMetadata) {
                if (entity.IsCustomizable.Value) {
                    var displayName = entity.DisplayName?.UserLocalizedLabel?.Label ?? string.Empty;
                    var logicalName = entity.LogicalName.ToString();
                    entityNames.Add((displayName, logicalName));
                }
            }

            return entityNames;
        }

        public List<AttributeMetadata> RetrieveAllAttributes(string logicalName)
        {
            List<AttributeMetadata> attributeMetadata = new List<AttributeMetadata>();
            var retrieveEntityRequest = new RetrieveEntityRequest
            {
                LogicalName = logicalName,
                EntityFilters = EntityFilters.Attributes
            };

            var retrieveEntityResponse = (RetrieveEntityResponse)Service.Execute(retrieveEntityRequest);

            foreach (var attribute in retrieveEntityResponse.EntityMetadata.Attributes)
            {
                attributeMetadata.Add(attribute);
            }
            return attributeMetadata;
        }

        public List<(string, string)> GetAllPluginSteps(string schemaName) {

            List<(string, string)> pluginSteps = new List<(string, string)>();
            QueryExpression sdkmessagefilterQuery = new QueryExpression("sdkmessagefilter")
            {
                ColumnSet = new ColumnSet("sdkmessagefilterid"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("primaryobjecttypecode", ConditionOperator.Equal, schemaName),
                    }
                }
            };

            // Expand on sdkmessageprocessingstep with specific filters
            LinkEntity linkEntity = new LinkEntity("sdkmessagefilter", "sdkmessageprocessingstep", "sdkmessagefilterid", "sdkmessagefilterid", JoinOperator.Inner)
            {
                Columns = new ColumnSet("name", "sdkmessageprocessingstepid"),
                EntityAlias = "steps",
                LinkCriteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("statecode", ConditionOperator.Equal, 0),
                        new ConditionExpression("customizationlevel", ConditionOperator.Equal, 1),
                        new ConditionExpression("iscustomizable", ConditionOperator.Equal, true)
                    }
                }
            };

            sdkmessagefilterQuery.LinkEntities.Add(linkEntity);

            EntityCollection stepsEc = Service.RetrieveMultiple(sdkmessagefilterQuery);

            foreach (var entity in stepsEc.Entities)
            {
                // Retrieve 'name' and 'sdkmessageprocessingstepid' from the linked entity "steps"
                var stepName = entity.GetAttributeValue<AliasedValue>("steps.name")?.Value as string;
                var stepId = entity.GetAttributeValue<AliasedValue>("steps.sdkmessageprocessingstepid")?.Value as Guid?;

                if (stepName != null && stepId.HasValue && !pluginSteps.Any(step => step.Item2 == stepId.ToString()))
                {
                    pluginSteps.Add((stepName, stepId.ToString()));
                }
            }

            return pluginSteps;
        }

        public XmlDocument BuildFetchXml(string entityLogicalName, string[] columns, string filter)
        {
            var doc = new XmlDocument();

            // Add XML declaration
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);

            var elfetch = doc.CreateElement(string.Empty, "fetch", string.Empty);
            doc.AppendChild(elfetch);

            var attversion = doc.CreateAttribute("version");
            attversion.Value = "1.0";
            elfetch.Attributes.Append(attversion);

            var attoutputformat = doc.CreateAttribute("output-format");
            attoutputformat.Value = "xml-platform";
            elfetch.Attributes.Append(attoutputformat);

            var attmapping = doc.CreateAttribute("mapping");
            attmapping.Value = "logical";
            elfetch.Attributes.Append(attmapping);

            var attdistinct = doc.CreateAttribute("distinct");
            attdistinct.Value = "false";
            elfetch.Attributes.Append(attdistinct);

            var elEntity = doc.CreateElement(string.Empty, "entity", string.Empty);
            elfetch.AppendChild(elEntity);

            var attname = doc.CreateAttribute("name");
            attname.Value = entityLogicalName;
            elEntity.Attributes.Append(attname);

            // Add attributes (columns) to be selected
            foreach (var column in columns)
            {
                var elAttribute = doc.CreateElement(string.Empty, "attribute", string.Empty);
                elEntity.AppendChild(elAttribute);

                var attaname = doc.CreateAttribute("name");
                attaname.Value = column;
                elAttribute.Attributes.Append(attaname);
            }

            // Handle the filter if provided
            if (!string.IsNullOrEmpty(filter))
            {
                var elFilter = doc.CreateElement(string.Empty, "filter", string.Empty);
                elFilter.SetAttribute("type", "and");
                elEntity.AppendChild(elFilter);

                var filterdoc = new XmlDocument();
                filterdoc.LoadXml(filter);
                var rootfilter = filterdoc.DocumentElement;
                var importedFilter = doc.ImportNode(rootfilter, true);
                elFilter.AppendChild(importedFilter);
            }

            return doc;
        }

        public EntityCollection RetrieveRecords(string fetchXml)
        {
            EntityCollection entityCollection = null;
            try
            {
                entityCollection = Service.RetrieveMultiple(new FetchExpression(fetchXml));
            }
            catch (Exception ex)
            {
                // Handle any errors here (e.g., invalid FetchXML, network issues, etc.)
                //MessageBox.Show($"Error retrieving records: {ex.Message}");
            }
            return entityCollection;
        }

    }
}
