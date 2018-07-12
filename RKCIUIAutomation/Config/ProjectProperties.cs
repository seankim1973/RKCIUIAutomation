using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;

namespace RKCIUIAutomation.Config
{
    public class ProjectProperties : WebDriverFactory
    {
        public class Component
        {
            public const string Breaksheet = "Breaksheet";
            public const string CDR = "CDR";
            public const string Control_Point = "Control_Point";
            public const string Correspondence_Log = "Correspondence_Log";
            public const string CVL_List_Item = "CVL_List_Item";
            public const string Design_Comment_Review = "Design_Comment_Review";
            public const string DIR = "DIR";
            public const string Document_Repository = "Document_Repository";
            public const string Environmental = "Environmental";
            public const string Equipment = "Equipment";
            public const string Girder_Tracker = "Girder_Tracker";
            public const string Material_Mix_Codes = "Material_Mix_Codes";
            public const string MPL_Tracker = "MPL_Tracker";
            public const string NCR = "NCR";
            public const string Notifications = "Notifications";
            public const string Other = "Other";
            public const string Project_Configuration = "Project_Configuration";
            public const string QMS = "QMS";
            public const string Quantity_Tracker = "Quantity_Tracker";
            public const string Random_Number = "Random_Number";
            public const string RFC = "RFC";
            public const string RFI = "RFI";
            public const string Search = "Search";
            public const string Testing_Module = "Testing_Module";
            public const string User_Mgmt = "User_Mgmt";
            public const string Submittals = "Submittals";
            public const string Guide_Schedule = "Guide_Schedule";
            public const string Link_Coverage = "Link_Coverage";

            //Secondary Components (not in Jira)
            public const string OV_Test = "OV_Test";
            public const string QAField = "QAField";
        }

        public List<string> GetComponentsForProject(TenantName tenantName)
        {
            List<string> components = new List<string>();
            try
            {
                components = CommonComponents;
                components.AddRange(DefineAdditionalComponents(tenantName));
            }
            catch (Exception e)
            {
                BaseUtils.LogInfo("Exception occured during GetComponentsForProject method", e);
            }

            return components;
        }
        private List<string> DefineAdditionalComponents(TenantName tenantName)
        {
            List<string> additionalComponents = new List<string>();
            switch (tenantName)
            {
                case TenantName.Garnet:
                    additionalComponents = Components_Garnet;
                    break;
                case TenantName.GLX:
                    additionalComponents = Components_GreenLineExt;
                    break;
                case TenantName.I15South:
                    additionalComponents = Components_I15Southbound;
                    break;
                case TenantName.I15Tech:
                    additionalComponents = Components_I15TechCorridor;
                    break;
                case TenantName.SH249:
                    additionalComponents = Components_SH249Ext;
                    break;
                case TenantName.SGWay:
                    additionalComponents = Components_SouthernGateway;
                    break;
            }
            return additionalComponents;
        }

        private readonly List<string> Components_Garnet = new List<string>
        {
            Component.RFI,
            Component.QAField
        };

        private readonly List<string> Components_GreenLineExt = new List<string>
        {
            Component.RFI
        };

        private readonly List<string> Components_I15Southbound = new List<string>
        {
            Component.OV_Test,
            Component.QAField
        };

        private readonly List<string> Components_I15TechCorridor = new List<string>
        {
            Component.OV_Test,
            Component.QAField
        };

        private readonly List<string> Components_SH249Ext = new List<string>
        {
            Component.QAField
        };

        private readonly List<string> Components_SouthernGateway = new List<string>
        {
            Component.QAField

        };

        private readonly List<string> CommonComponents = new List<string>
        {
            Component.Link_Coverage,
            Component.Breaksheet,
            Component.CDR,
            Component.CVL_List_Item,
            Component.Other,
            Component.Project_Configuration,
            Component.Submittals
        };
    }
}
