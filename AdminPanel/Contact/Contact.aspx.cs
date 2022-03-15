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
public partial class AdminPanel_Contact_Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Postback
        if (!Page.IsPostBack)
        {
            displayContact();
        }
        #endregion Postback
    }
    protected void gvContactShow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            #region Command Argument
            if (e.CommandArgument != "")
            {
                deleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                displayContact();
            }
            #endregion Command Argument
        }
    }

    private void displayContact()
    {
        #region Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Connection String

        try
        {
            #region Connection Open and Object Command
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_SelectWithAllOtherId";
            #endregion Connection Open and Object Command

            #region Data Read , Execute and DataBind
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvContactShow.DataSource = objSDR;
            gvContactShow.DataBind();
            #endregion Data Read , Execute and DataBind

            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }
        catch (Exception ex)
        {
            #region Exception Message
            Exception.Visible = true;
            lblCatchMessage.Text = ex.Message;
            MainContent.Visible = false;
            #endregion Exception Message
        }
        finally
        {
            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }

    }

    private void deleteContact(SqlInt32 ContactID)
    {
        #region Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Connection String

        try
        {
            #region Connection Open , Object Command , Execute Command
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_DeleteByPK";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID);
            objCmd.ExecuteNonQuery();

            #endregion Connection Open , Object Command , Execute Command

            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close

        }
        catch (Exception ex)
        {
            #region Exception Message
            Exception.Visible = true;
            lblCatchMessage.Text = ex.Message;
            MainContent.Visible = false;
            #endregion Exception Message
        }
        finally
        {
            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }

    } 
}