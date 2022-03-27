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

public partial class AdminPanel_Registration_Registrastion : System.Web.UI.Page
{
    private object objConn;
    private object lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {

        #region local variable
        // SqlInt32 strUserID = SqlInt32.Null;
        SqlString strUserName = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;

        #endregion local variable

        #region Server side validation
        string strErrorMessage = "";

        if (txtUserName.Text.Trim() == "")
        //if(txtUserNameLogin.Text.Trim()==String.Empty)
        {
            strErrorMessage += "- Enter User Name<br/>";
        }
        if (txtPassword.Text.Trim() == "")
        //if(txtUserNameLogin.Text.Trim()==String.Empty)
        {
            strErrorMessage += "- Enter Password <br/>";
        }

        if (txtDisplayName.Text.Trim() == "")
        //if(txtDisplayName.Text.Trim()==String.Empty)
        {
            strErrorMessage += "- Enter Display Name<br/>";
        }

        if (txtContactno.Text.Trim() == "")
        //if(txtUserNameLogin.Text.Trim()==String.Empty)
        {
            strErrorMessage += "- Enter Mobile No<br/>";
        }

        if (txtEmail.Text.Trim() == "")
        //if(txtUserNameLogin.Text.Trim()==String.Empty)
        {
            strErrorMessage += "- Enter Email<br/>";
        }

        if (strErrorMessage != String.Empty)
        {
            lblMessage.Text = "Kindly Solve following Error(s) <br/>" + strErrorMessage;
            return;
        }
        #endregion Server side validation

        #region Assign the Value
        if (txtUserName.Text.Trim() != "")
        {
            strUserName = txtUserName.Text.Trim();
        }
        if (txtPassword.Text.Trim() != "")
        {
            strPassword = txtPassword.Text.Trim();
        }
        if (txtDisplayName.Text.Trim() != "")
        {
            strDisplayName = txtDisplayName.Text.Trim();
        }
        if (txtEmail.Text.Trim() != "")
        {
            strEmail = txtEmail.Text.Trim();
        }
        if (txtContactno.Text.Trim() != "")
        {
            strContactNo = txtContactno.Text.Trim();
        }
        #endregion Assion the Value


        #region Connection String
        SqlConnection objconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Connection String
        try
        {
            #region Connection Open
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #endregion Connection Open

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_Insert";


            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Password", strPassword);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@DisplayName", strDisplayName);

            objCmd.ExecuteNonQuery();

            Session["IsLogin"] = "LoginSuccess";
            Response.Redirect("~/AdminPanel/Login/Login.aspx", true);


            textUserName.Text = txtDisplayName = String.Empty;
            txtPassword.Text = String.Empty;
            txtEmail.Text = txtContactno.Text = String.Empty;


            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
            {
                objConn.Close();
            }
            #endregion Connection Close

        }
        catch (Exception ex)
        {
            #region Exception Message
            lblMessage.Text = ex.Message;
            #region Exception Message
        }
        finally
        {
            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
            {
                objConn.Close();
            }
            #endregion Connection Close
        }

    }
}