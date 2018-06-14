using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Config
{
    public static class ProjectProperties
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
        }

        private static List<string> additionalComponents;

        public static List<string> GetComponentsForProject(ProjectName projectName)
        {
            List<string> components = new List<string>();
            try
            {
                DefineAdditionalComponents(projectName);
                components = CommonComponents;
                components.AddRange(additionalComponents);
            }
            catch (Exception e)
            {
                LogInfo("Exception occured during GetComponentsForProject method", e);
            }

            return components;
        }

        private static List<string> DefineAdditionalComponents(ProjectName projectName)
        {
            additionalComponents = new List<string>();
            switch (projectName)
            {
                case ProjectName.Garnet:
                    additionalComponents = Components_Garnet;
                    break;
                case ProjectName.GLX:
                    additionalComponents = Components_GreenLineExt;
                    break;
                case ProjectName.I15South:
                    additionalComponents = Components_I15Southbound;
                    break;
                case ProjectName.I15Tech:
                    additionalComponents = Components_I15TechCorridor;
                    break;
                case ProjectName.SH249:
                    additionalComponents = Components_SH249Ext;
                    break;
                case ProjectName.SGWay:
                    additionalComponents = Components_SouthernGateway;
                    break;
            }
            return additionalComponents;
        }

        private static readonly List<string> Components_Garnet = new List<string>
        {
        };

        private static readonly List<string> Components_GreenLineExt = new List<string>
        {
        };

        private static readonly List<string> Components_I15Southbound = new List<string>
        {
        };

        private static readonly List<string> Components_I15TechCorridor = new List<string>
        {
        };

        private static readonly List<string> Components_SH249Ext = new List<string>
        {
        };

        private static readonly List<string> Components_SouthernGateway = new List<string>
        {
        };

        private static readonly List<string> CommonComponents = new List<string>
        {
            Component.Breaksheet,
            Component.CDR,
            Component.CVL_List_Item,
            Component.Other,
            Component.Project_Configuration,
            Component.Submittals
        };
    }
}
