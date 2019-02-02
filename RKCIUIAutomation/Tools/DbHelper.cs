using Dapper;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Tools
{
    public static class DbHelper
    {
        internal static IDbConnection GetSqlConnection()
        {
            string connectVal = ConfigurationManager.ConnectionStrings["StageDB"].ConnectionString;
            IDbConnection connection = new SqlConnection(connectVal);
            return connection;
        }
    }

    public class DirDbAccess : BaseUtils
    {
        private int GetCurrentProjectID()
        {
            int projID = 0;

            var tenant = BaseClass.tenantName;

            switch (tenant)
            {
                case Config.TenantName.Garnet:
                    projID = 1;
                    break;
                case Config.TenantName.SH249:
                    projID = 2;
                    break;
                case Config.TenantName.SGWay:
                    projID = 3;
                    break;
                case Config.TenantName.I15Tech:
                    projID = 4;
                    break;
                case Config.TenantName.GLX:
                    projID = 5;
                    break;
                case Config.TenantName.I15South:
                    projID = 7;
                    break;
                case Config.TenantName.LAX:
                    projID = 9;
                    break;
            }

            return projID;
        }

        public DirDbData GetDirData(string dirNumber, string revision = "A")
        {
            List<DirDbData> output = new List<DirDbData>();
            try
            {
                int projID = GetCurrentProjectID();

                using (IDbConnection connection = DbHelper.GetSqlConnection())
                {
                    output = connection.Query<DirDbData>($"select * from DIR where ProjectID={projID} and DIRNO='{dirNumber}' and Revision='{revision}'").ToList();
                }              
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
            return output[0];
        }

        public void SetDIR_DIRNO_IsDeleted(string dirNumber, string revision = "A", bool setAsDeleted = true)
        {
            try
            {
                int projID = GetCurrentProjectID();
                int isDeleted = setAsDeleted ? 1 : 0;

                using (IDbConnection connection = DbHelper.GetSqlConnection())
                {
                    connection.Execute($"update DIR set IsDeleted={isDeleted} where ProjectId={projID} and DIRNO='{dirNumber}' and Revision='{revision}'");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Debug(e.StackTrace);
            }
        }

        public DirDbData GetDirPackageData(string packageNumber)
        {
            List<DirDbData> output = new List<DirDbData>();

            try
            {
                int projID = GetCurrentProjectID();

                using (IDbConnection connection = DbHelper.GetSqlConnection())
                {
                    output = connection.Query<DirDbData>($"select * from DIRPackage where ProjectID={projID} and PackageNo='{packageNumber}'").ToList();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
            
            return output[0];
        }

        public void SetDIRPackageNo_AndReferences_AsDeleted(string packageNumber)
        {
            DirDbData data = new DirDbData();
            List<DirDbData> output = new List<DirDbData>();

            try
            {
                int projID = GetCurrentProjectID();

                using (IDbConnection connection = DbHelper.GetSqlConnection())
                {
                    output = connection.Query<DirDbData>($"select [Id] from DIRPackage where ProjectId={projID} and PackageNo='{packageNumber}'").ToList();
                    data = output[0];

                    var pkgId = data.Id;

                    connection.Execute($"update DIR set DIRPackageID=null where ProjectId={projID} and DIRPackageID={pkgId}");
                    connection.Execute($"update DIRPackage set IsDeleted=1 from (select * from DIRPackage where Id=" +
                        $"(select max(Id) from DIRPackage as pkg where ProjectId={projID} and PackageNo='{packageNumber}')" +
                        $") t1 where DIRPackage.Id=t1.Id");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Debug(e.StackTrace);
            }
        }


    }

    public class DirDbData
    {
        public int IsDeleted { get; set; }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public object DIRPackageID { get; set; }

        public string PackageNo { get; set; }

        public string DIRNO { get; set; }

        public string Revision { get; set; }



        public string DirData
        {
            get
            {
                return $"Project ID : {ProjectId} - DIR No. : {DIRNO} DIR Rev. : {Revision} IsDeleted : {IsDeleted} ID : {Id}";
            }
        }

    }
}
