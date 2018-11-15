using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Config
{
    public class ProjectProperties : WebDriverFactory
    {
        public class Component
        {
            public const string Breaksheet_Module = "Breaksheet_Module";
            public const string CDR = "CDR";
            public const string CDR_WF_Complex = "CDR_WF_Complex";
            public const string Control_Point = "Control_Point";
            public const string Correspondence_Log = "Correspondence_Log";
            public const string CVL_List_Items = "CVL_List_Items";
            public const string CVL_Lists = "CVL_Lists";
            public const string CVL_List = "CVL_List";
            public const string DesignDoc_CommentReview = "DesignDoc_CommentReview";
            public const string DIR = "DIR";
            public const string DIR_WF_Simple_QA = "DIR_WF_Simple_QA";
            public const string DIR_WF_Simple_QC = "DIR_WF_Simple_QC";
            public const string DIR_WF_Complex = "DIR_WF_Complex";
            public const string Document_Repository = "Document_Repository";
            public const string Environmental = "Environmental";
            public const string Equipment = "Equipment";
            public const string Girder_Tracker = "Girder_Tracker";
            public const string Material_Mix_Codes = "Material_Mix_Codes";
            public const string MPL_Tracker = "MPL_Tracker";
            public const string NCR = "NCR";
            public const string NCR_WF_Complex = "NCR_WF_Complex";
            public const string NCR_WF_Simple = "NCR_WF_Simple";
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


            //Tenant Specific Components
            public const string Garnet = "Garnet";
            public const string GLX = "GLX";
            public const string I15South = "I15South";
            public const string I15Tech = "I15Tech";
            public const string LAX = "LAX";
            public const string SGWay = "SGWay";
            public const string SH249 = "SH249";
            
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
                log.Error("Exception occured during GetComponentsForProject method", e);
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

                case TenantName.LAX:
                    additionalComponents = Components_LAX;
                    break;
            }
            return additionalComponents;
        }

        private readonly List<string> Components_Garnet = new List<string>
        {
            Component.Garnet,
            Component.DesignDoc_CommentReview,
            Component.RFI,
            Component.QAField,
        };

        private readonly List<string> Components_GreenLineExt = new List<string>
        {
            Component.GLX,
            Component.CDR,
            Component.CDR_WF_Complex,
            Component.DesignDoc_CommentReview,
            Component.RFI,
            Component.NCR,
            Component.NCR_WF_Complex,
            Component.DIR,
            Component.DIR_WF_Simple_QA,
            Component.DIR_WF_Simple_QC
        };

        private readonly List<string> Components_I15Southbound = new List<string>
        {
            Component.I15South,
            Component.CDR,
            Component.CDR_WF_Complex,
            Component.OV_Test,
            Component.QAField,
            Component.NCR,
            Component.NCR_WF_Complex,
            Component.DIR,
            Component.DIR_WF_Simple_QA
        };

        private readonly List<string> Components_I15TechCorridor = new List<string>
        {
            Component.I15Tech,
            Component.CDR,
            Component.CDR_WF_Complex,
            Component.OV_Test,
            Component.QAField,
            Component.NCR,
            Component.NCR_WF_Complex,
            Component.DIR,
            Component.DIR_WF_Simple_QA
        };

        private readonly List<string> Components_SH249Ext = new List<string>
        {
            Component.SH249,
            Component.CDR,
            Component.DesignDoc_CommentReview,
            Component.QAField,
            Component.NCR,
            Component.NCR_WF_Simple,
            Component.DIR,
            Component.DIR_WF_Complex
        };

        private readonly List<string> Components_SouthernGateway = new List<string>
        {
            Component.SGWay,
            Component.CDR,
            Component.DesignDoc_CommentReview,
            Component.QAField,
            Component.NCR,
            Component.NCR_WF_Simple,
            Component.DIR,
            Component.DIR_WF_Complex
        };

        private readonly List<string> Components_LAX = new List<string>
        {
            Component.LAX,
            Component.CDR,
            Component.NCR,
            Component.NCR_WF_Complex,
            Component.DIR,
            Component.DIR_WF_Simple_QA,
            Component.DIR_WF_Simple_QC
        };

        private readonly List<string> CommonComponents = new List<string>
        {
            Component.Link_Coverage,
            Component.Breaksheet_Module,
            Component.CVL_List,
            Component.CVL_Lists,
            Component.CVL_List_Items,
            Component.Other,
            Component.Project_Configuration,
            Component.Search,
            Component.Submittals
        };
    }
}