using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        #region Local Variable
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;

        #endregion Local Variable

        #region Server Side Validation

        String strErrorMessage = "";
        if (txtUserNameLogin.Text.Trim() == "")
        {
            strErrorMessage += "-Enter UserName <br>";
        }
        if (txtPasswordLogin.Text.Trim() == "")
        {
            strErrorMessage += "-Enter Password <br>";
        }
        if (strErrorMessage != "")
        {
            lblmessage.Text = "Kindaly Solved Following Errors<br/>" + strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Assign the Value

        if (txtUserNameLogin.Text.Trim() != "")
        {
            strUserName = txtUserNameLogin.Text.Trim();
        }
        if (txtPasswordLogin.Text.Trim() != "")
        {
            strPassword = txtPasswordLogin.Text.Trim();
        }

        #endregion Assign the Value

        SqlConnection objconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserMultiUserAddressBookConnectionString"].ConnectionString);
        try
        {
            if (objconn.State != ConnectionState.Open)
                objconn.Open();
            SqlCommand objCmd = objconn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByUserNamePassword";

            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if(objSDR.HasRows)
            {
                while(objSDR.Read())
                {
                    if(!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if (!objSDR["UserDisplayName"].Equals(DBNull.Value))
                    {
                        Session["UserDisplayName"] = objSDR["UserDisplayName"].ToString().Trim();
                    }
                    break;
                }
                Response.Redirect("~/AdminPanel/Home/Home.aspx", true);
            }
            else
            {
                lblmessage.Text = "Either Username or Password is not valid, try again with differnt user";
            }

            if (objconn.State != ConnectionState.Closed)
                objconn.Close();
        }
       
        
        
        catch(Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
        finally
        {
        
        }
     


    }
}