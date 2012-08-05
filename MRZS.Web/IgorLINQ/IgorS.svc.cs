using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

using MRZS;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MRZS.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IgorS
    {
        [OperationContract]
        public List<mrzs05mMenu>  GetMenuElement(int id)
        {        
            //get row record from db by id
            LINQClassesDataContext d = new LINQClassesDataContext(ConfigurationManager.ConnectionStrings["MRZSConnectionString"].ConnectionString);
            return d.mrzs05mMenu.Where(menu => menu.id == id).ToList();              
        }
        //[OperationContract]
        //public string GetMenuElement2(int id)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MRZSConnectionString"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("SELECT [menuElement] as menuelem FROM [mrzs].[dbo].[mrzs05mMenu] where id=@id", con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@id", (object)id);
        //    con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
        //    if (reader.Read())
        //    {
        //        return reader["menuelem"].ToString();
        //    }
        //    else return null;
        //}
    }
}
