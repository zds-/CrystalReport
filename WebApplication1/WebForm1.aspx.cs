using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/*
 * To run CR in IISExpress. Let Crystal report to find its js files. Find config file and add virtual dir by this example (your paths and app name can be different)
 *      - path - C:\Temp\WebApplication1\.vs\WebApplication1\config\applicationhost.config
 *      <site name="WebApplication1" id="2">
                <application path="/" applicationPool="Clr4IntegratedAppPool">
                    <virtualDirectory path="/" physicalPath="C:\Temp\WebApplication1\WebApplication1" />
                    <virtualDirectory path="/aspnet_client" physicalPath="C:\inetpub\wwwroot\aspnet_client" /> -- this must be added 
                </application>
                <bindings>
                    <binding protocol="http" bindingInformation="*:57769:localhost" />
                </bindings>
        </site>
 * 
 */
namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //uses Northwind test db
            var id = 0; //this id may come from query string and we use it as parameter in sql query

            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString; //check you connection in web.config
            var query = "SELECT LastName, FirstName, EmployeeID, Title FROM Employees WHERE(EmployeeID > @id)";
            
            //connect to db and get data
            var ds = new DataSet();
            using (var cmd = new SqlCommand(query, new SqlConnection(connectionString)))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
            }

            //show info that table
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            //show info in CR
            var rpt = new CrystalReport2(); //create report
            rpt.SetDataSource(ds.Tables[0]); //pass data to report
            CrystalReportViewer.ReportSource = rpt; //let CR know which report to display
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var param = "ALFKI"; //this id may come from query string and we use it as parameter in sql query

            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString; //check you connection in web.config

            var dt = new DataTable();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var adapter = new SqlDataAdapter("CustOrderHist", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@CustomerID", param);
                    adapter.Fill(dt);
                }
            }

            var rpt = new CrystalReport1(); //create report
            rpt.SetDataSource(dt); //pass data to report
            CrystalReportViewer.ReportSource = rpt; //let CR know which report to display
        }

        protected void Button1_Click2(object sender, EventArgs e)
        {
            CrystalReportViewer.ReportSource = null;
            var param = "ALFKI"; //this id may come from query string and we use it as parameter in sql query

            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString2"].ConnectionString; //check you connection in web.config

            var dt = new DataTable();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var adapter = new SqlDataAdapter("CustOrderHist", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@CustomerID", param);
                    adapter.Fill(dt);
                }
            }

            var rpt = new CrystalReport1(); //create report
            rpt.SetDataSource(dt); //pass data to report
            CrystalReportViewer.ReportSource = rpt; //let CR know which report to display
        }
    }
}