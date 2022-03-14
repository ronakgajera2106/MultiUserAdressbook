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
public partial class WebPages_Category_ContactCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Postback
        if (!Page.IsPostBack)
        {
            displayContactCategory();
        }
        #endregion Postback
    }
    protected void gvContactCategoryShow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            #region Command Argument
            if (e.CommandArgument != "")
            {
                deleteContactCategory(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                displayContactCategory();
            }
            #endregion Command Argument
        }
    }

    private void displayContactCategory()
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
            objCmd.CommandText = "PR_ContactCategory_SelectAll";
            #endregion Connection Open and Object Command

            #region Data Read , Execute and DataBind
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvContactCategoryShow.DataSource = objSDR;
            gvContactCategoryShow.DataBind();
            #endregion Data Read , Execute and DataBind

            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }
        catch (Exception ex)
        {
            #region Exception Message
            pnlException.Visible = true;
            lblCatchMessage.Text = ex.Message;
            pnlMainContent.Visible = false;
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

    private void deleteContactCategory(SqlInt32 ContactCategoryID)
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
            objCmd.CommandText = "PR_ContactCategory_DeleteByPK";
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
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
            pnlException.Visible = true;
            lblCatchMessage.Text = ex.Message;
            pnlMainContent.Visible = false;
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