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

public partial class WebPages_State_State : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Postback
        if (!Page.IsPostBack)
        {
            displayState();
        }
        #endregion Postback

    }
    protected void gvStateShow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            #region Command Argument
            if (e.CommandArgument != "")
            {
                deleteState(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                displayState();
            }
            #endregion Command Argument
        }
    }

    private void displayState()
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
            objCmd.CommandText = "PR_State_SelectWithCountryNameList";
            #endregion Connection Open and Object Command

            #region Data Read , Execute and DataBind
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvStateShow.DataSource = objSDR;
            gvStateShow.DataBind();
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

    private void deleteState(SqlInt32 StateID)
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
            objCmd.CommandText = "PR_State_DeleteByPK";
            objCmd.Parameters.AddWithValue("@StateID", StateID);
            objCmd.ExecuteNonQuery();

            #endregion Connection Open , Object Command , Execute Command

            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close

        }
        catch(Exception ex){
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