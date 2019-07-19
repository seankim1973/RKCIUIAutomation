using Dapper;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Tools
{
    public static class DbHelper
    {
        internal static IDbConnection GetSqlConnection()
        {
            string connectVal = string.Empty;
            IDbConnection connection = null;

            try
            {
                connectVal = BaseClass.testPlatform.Equals(TestPlatformType.Grid)
                    ? ConfigurationManager.ConnectionStrings["StageDB"].ConnectionString
                    : ConfigurationManager.ConnectionStrings["StageDB_VPN"].ConnectionString;

                connection = new SqlConnection(connectVal);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

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
                case Config.TenantNameType.Garnet:
                    projID = 1;
                    break;
                case Config.TenantNameType.SH249:
                    projID = 2;
                    break;
                case Config.TenantNameType.SGWay:
                    projID = 3;
                    break;
                case Config.TenantNameType.I15Tech:
                    projID = 4;
                    break;
                case Config.TenantNameType.GLX:
                    projID = 5;
                    break;
                case Config.TenantNameType.I15South:
                    projID = 7;
                    break;
                case Config.TenantNameType.LAX:
                    projID = 9;
                    break;
                case Config.TenantNameType.I15North:
                    projID = 13;
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
                throw;
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
                    string sql = $"SELECT * FROM DIRPackage where Id = (select max(Id) from DIRPackage as pkg where ProjectID={projID} and PackageNo='{packageNumber}')";
                    output = connection.Query<DirDbData>(sql).ToList();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Error(e.StackTrace);
                throw;
            }
            
            return output[0];
        }

        public void SetDIRPackageNo_AndReferences_AsDeleted(string packageNumber)
        {
            try
            {
                int projID = GetCurrentProjectID();

                using (IDbConnection connection = DbHelper.GetSqlConnection())
                {
                    string sql =
                        $"DECLARE @pkgId int " +
                        $"SET @pkgId = (Select max(Id) from DIRPackage as pkg where ProjectId={projID} and PackageNo='{packageNumber}') " +
                        $"update DIR set DIRPackageID = null from(Select * From DIR) t1 where DIR.DIRPackageID = @pkgId " +
                        $"update DIRPackage set IsDeleted = 1 where DIRPackage.Id = @pkgId";

                    connection.Execute(sql);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                log.Error(e.StackTrace);
                throw;
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
