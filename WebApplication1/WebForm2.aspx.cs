using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        //store procedure report, no dataSet required

        protected void Page_Load(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            Cache.Remove("CrystalReport1");
            Cache.Remove("CrystalReport2");
            Cache.Remove("CrystalReport3");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Cache.Remove("CrystalReport1");
            Cache.Remove("CrystalReport2");
            Cache.Remove("CrystalReport3");
            CrystalReportViewer1.ReportSource = null;
            var param = "ALFKI";

            var rpt = new CrystalReport1(); //create report
            SetConnection(rpt, "Northwind", "localhost, 1433", "sa", "PaSSword!2020");
            rpt.SetParameterValue("@CustomerID", param);

            CrystalReportViewer1.ReportSource = rpt; //let CR know which report to display
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Cache.Remove("CrystalReport1");
            Cache.Remove("CrystalReport2");
            Cache.Remove("CrystalReport3");
            CrystalReportViewer1.ReportSource = null;


            const string dbName = "Northwind2";
            var crConnectionInfo = new ConnectionInfo()
            {
                ServerName = "localhost, 1433",
                DatabaseName = dbName,
                UserID = "sa",
                Password = "PaSSword!2020"
            };

            var param = "ANATR";

            var rpt = new CrystalReport1(); //create report

            rpt.DataSourceConnections.Clear();
            rpt.DataSourceConnections[0].SetConnection("localhost, 1433", "Northwind2", "sa", "PaSSword!2020");

            rpt.SetParameterValue("@CustomerID", param);

            var crTables = rpt.Database.Tables;

            for (int i = 0; i < crTables.Count; i++)
            {
                var crTable = crTables[i];
                var crTableLogOnInfo = crTable.LogOnInfo;

                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;

                crTable.ApplyLogOnInfo(crTableLogOnInfo);

                //If your DatabaseName is changing at runtime, specify the table location. For example, when you are reporting off of a Northwind database 
                //on SQL server you should have the following line of code:

                crTable.Location = dbName + ".dbo." + crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1);

            };

            CrystalReportViewer1.ReportSource = rpt; //let CR know which report to display
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Cache.Remove("CrystalReport1");
            Cache.Remove("CrystalReport2");
            Cache.Remove("CrystalReport3");
            CrystalReportViewer1.ReportSource = null;
            var param = "ALFKI";

            var rpt = new CrystalReport1(); //create report
            SetConnection(rpt, "Northwind2", "localhost, 1433", "sa", "PaSSword!2020");
            rpt.SetParameterValue("@CustomerID", param);

            CrystalReportViewer1.ReportSource = rpt; //let CR know which report to display
        }

        private static void SetConnection(ReportDocument report, string databaseName, string serverName, string userName, string password)
        {
            foreach (Table table in report.Database.Tables)
            {
                if (table.Name != "Command")
                {
                    SetTableConnectionInfo(table, databaseName, serverName, userName, password);
                }
            }

            foreach (ReportObject obj in report.ReportDefinition.ReportObjects)
            {
                if (obj.Kind != ReportObjectKind.SubreportObject)
                {
                    return;
                }

                var subReport = (SubreportObject)obj;
                var subReportDocument = report.OpenSubreport(subReport.SubreportName);
                SetConnection(subReportDocument, databaseName, serverName, userName, password);
            }
        }

        private static void SetTableConnectionInfo(Table table, string databaseName, string serverName, string userName, string password)
        {
            // Get the ConnectionInfo Object.
            var logOnInfo = table.LogOnInfo;
            var connectionInfo = logOnInfo.ConnectionInfo;

            // Set the Connection parameters.
            connectionInfo.DatabaseName = databaseName;
            connectionInfo.ServerName = serverName;
            connectionInfo.Password = password;
            connectionInfo.UserID = userName;
            table.ApplyLogOnInfo(logOnInfo);

            if (!table.TestConnectivity())
            {
                throw new ApplicationException("Resource.UnableToConnectCrystalReportToDatabase");
            }

            table.Location = databaseName + "." + "dbo" + "." + table.Location;
        }
    }
}
/*
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

class Test {
    private CrystalReport1 crReportDocument = new  CrystalReport1();

    private Database crDatabase;
    private Tables crTables;
    private Table crTable;

    private TableLogOnInfo crTableLogOnInfo;
    private ConnectionInfo crConnectionInfo = new ConnectionInfo();

    //Setup the connection information structure to log on to the data source for the report.
    // If using ODBC, this should be the DSN. If using OLEDB, etc, this should be the physical server name

    public void ChangeDb() 
    {

        crConnectionInfo.ServerName = "DSN or Server Name";

        // If you are connecting to Oracle there is no DatabaseName. Use an empty string i.e. crConnectionInfo.DatabaseName = "";

        crConnectionInfo.DatabaseName = "DatabaseName";

        crConnectionInfo.UserID = "Your UserID";

        crConnectionInfo.Password = "Your Password";

        // This code works for both user tables and stored procedures. Get the table information from the report

        crDatabase = crReportDocument.Database;

        crTables = crDatabase.Tables;

        //Loop through all tables in the report and apply the connection information for each table.

        for (int i = 0; i < crTables.Count; i++)

        {

            crTable = crTables[i];

            crTableLogOnInfo = crTable.LogOnInfo;

            crTableLogOnInfo.ConnectionInfo = crConnectionInfo;

            crTable.ApplyLogOnInfo(crTableLogOnInfo);

            //If your DatabaseName is changing at runtime, specify the table location. For example, when you are reporting off of a Northwind database on SQL server you should have the following line of code:

            crTable.Location = "Northwind.dbo." + crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1)

        }

        //Set the viewer to the report object to be previewed.

        crystalReportViewer1.ReportSource = crReportDocument;
    }
}
*/