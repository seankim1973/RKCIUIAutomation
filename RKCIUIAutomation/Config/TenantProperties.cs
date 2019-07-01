using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System.Collections.Generic;

namespace RKCIUIAutomation.Config
{
    public interface ITenantProperties
    {
        List<string> TenantComponents { get; set; }
        void ConfigTenantComponents(TenantName tenantName);
    }

    public class TenantProperties : BaseUtils, ITenantProperties
    {
        TenantName _tenantName { get; set; }

        public TenantProperties()
        {
        }

        public TenantProperties(IWebDriver driver) => Driver = driver;

        public TenantProperties(TenantName tenantName)
        {
            _tenantName = tenantName;
        }

        private static readonly List<string> commonComponents = new List<string>
        {
            Component.Link_Coverage,
            Component.Breaksheet_Module,
            Component.CVL_List,
            Component.CVL_Lists,
            Component.CVL_List_Items,
            Component.Other,
            Component.Project_Configuration,
            Component.Search,
            Component.Submittals,
            Component.TestMethods,
            Component.Correspondence_Log
        };

        public List<string> TenantComponents { get; set; }

        public class Component
        {
            public const string Breaksheet_Module = "Breaksheet_Module";
            public const string CDR = "CDR";
            public const string CDR_WF_Complex = "CDR_WF_Complex";
            public const string CDR_WF_Simple = "CDR_WF_Simple";
            public const string Control_Point = "Control_Point";
            public const string Correspondence_Log = "Correspondence_Log";
            public const string CVL_List_Items = "CVL_List_Items";
            public const string CVL_Lists = "CVL_Lists";
            public const string CVL_List = "CVL_List";
            public const string DesignDoc_CommentReview = "DesignDoc_CommentReview";
            public const string CommentReview_NoComment = "CommentReview_NoComment";
            public const string CommentReview_RegularComment = "CommentReview_RegularComment";
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
            public const string TestMethods = "TestMethods";
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
            public const string I15North = "I15North";
            public const string I15South = "I15South";
            public const string I15Tech = "I15Tech";
            public const string LAX = "LAX";
            public const string SGWay = "SGWay";
            public const string SH249 = "SH249";
            
        }

        public void ConfigTenantComponents(TenantName tenantName)
        {
            ITenantProperties instance = null;

            switch (tenantName)
            {
                case TenantName.Garnet:
                    instance = new ProjectProperties_Garnet();
                    break;

                case TenantName.GLX:
                    instance = new ProjectProperties_GLX();
                    break;

                case TenantName.I15North:
                    instance = new ProjectProperties_I15NB();
                    break;

                case TenantName.I15South:
                    instance = new ProjectProperties_I15SB();
                    break;

                case TenantName.I15Tech:
                    instance = new ProjectProperties_I15Tech();
                    break;

                case TenantName.SH249:
                    instance = new ProjectProperties_SH249();
                    break;

                case TenantName.SGWay:
                    instance = new ProjectProperties_SGWay();
                    break;

                case TenantName.LAX:
                    instance = new ProjectProperties_LAX();
                    break;
            }

            List<string> components = new List<string>();
            components.AddRange(commonComponents);
            components.AddRange(instance.TenantComponents);
            TenantComponents = components;
        }
    }

    public class ProjectProperties_LAX : TenantProperties, ITenantProperties
    {
        public ProjectProperties_LAX()
        {
            TenantComponents = new List<string>
            {
                Component.LAX,
                Component.CDR,
                Component.CDR_WF_Complex,
                Component.NCR,
                Component.NCR_WF_Complex,
                Component.DIR,
                Component.DIR_WF_Simple_QA,
                Component.DIR_WF_Simple_QC,
                Component.DesignDoc_CommentReview,
                Component.CommentReview_RegularComment,
                Component.Control_Point
            };
        }
    }

    public class ProjectProperties_SH249 : TenantProperties, ITenantProperties
    {
        public ProjectProperties_SH249()
        {
            TenantComponents = new List<string>
            {
                Component.SH249,
                Component.CDR,
                Component.CDR_WF_Simple,
                Component.QAField,
                Component.NCR,
                Component.NCR_WF_Simple,
                Component.DIR,
                Component.DIR_WF_Complex,
                Component.DesignDoc_CommentReview,
                Component.CommentReview_NoComment,
                Component.CommentReview_RegularComment
            };
        }
    }

    public class ProjectProperties_SGWay : TenantProperties, ITenantProperties
    {
        public ProjectProperties_SGWay()
        {
            TenantComponents = new List<string>
            {
                Component.SGWay,
                Component.CDR,
                Component.CDR_WF_Simple,
                Component.QAField,
                Component.NCR,
                Component.NCR_WF_Simple,
                Component.DIR,
                Component.DIR_WF_Complex,
                Component.DesignDoc_CommentReview,
                Component.CommentReview_NoComment,
                Component.CommentReview_RegularComment
            };
        }
    }

    public class ProjectProperties_I15NB : TenantProperties, ITenantProperties
    {
        public ProjectProperties_I15NB()
        {
            TenantComponents = new List<string>
            {
                Component.I15North,
                Component.CDR,
                Component.CDR_WF_Complex,
                Component.OV_Test,
                Component.QAField,
                Component.NCR,
                Component.NCR_WF_Complex,
                Component.DIR,
                Component.DIR_WF_Simple_QA,
                Component.Control_Point
            };
        }
    }

    public class ProjectProperties_I15SB : TenantProperties, ITenantProperties
    {
        public ProjectProperties_I15SB()
        {
            TenantComponents = new List<string>
            {
                Component.I15South,
                Component.CDR,
                Component.CDR_WF_Complex,
                Component.OV_Test,
                Component.QAField,
                Component.NCR,
                Component.NCR_WF_Complex,
                Component.DIR,
                Component.DIR_WF_Simple_QA,
                Component.Control_Point
            };
        }
    }

    public class ProjectProperties_I15Tech : TenantProperties, ITenantProperties
    {
        public ProjectProperties_I15Tech()
        {
            TenantComponents = new List<string>
            {
                Component.I15Tech,
                Component.CDR,
                Component.CDR_WF_Complex,
                Component.OV_Test,
                Component.QAField,
                Component.NCR,
                Component.NCR_WF_Complex,
                Component.DIR,
                Component.DIR_WF_Simple_QA,
                Component.Control_Point

            };
        }
    }

    public class ProjectProperties_GLX : TenantProperties, ITenantProperties
    {
        public ProjectProperties_GLX()
        {
            TenantComponents = new List<string>
            {
                Component.GLX,
                Component.CDR,
                Component.CDR_WF_Complex,
                Component.RFI,
                Component.NCR,
                Component.NCR_WF_Complex,
                Component.DIR,
                Component.DIR_WF_Simple_QA,
                Component.DIR_WF_Simple_QC
            };
        }
    }

    public class ProjectProperties_Garnet : TenantProperties, ITenantProperties
    {
        public ProjectProperties_Garnet()
        {
            TenantComponents = new List<string>
            {
                Component.Garnet,
                Component.CDR,
                Component.CDR_WF_Simple,
                Component.RFI,
                Component.QAField
            };
        }
    }
}