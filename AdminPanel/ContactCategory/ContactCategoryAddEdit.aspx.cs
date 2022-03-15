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

public partial class WebPages_Category_ContactCategoryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Edit Section
            if (Request.QueryString["ContactCategoryID"] != null)
            {
                lblPageTitle.Text = "Contact Category Edit Data";
                btnSubmitContactCategory.Text = "Edit";
                fillControl(Convert.ToInt32(Request.QueryString["ContactCategoryID"].ToString()));
            }
            #endregion Edit Section

            #region Add Section
            else
            {
                lblPageTitle.Text = "Contact Category Add Data";
                btnSubmitContactCategory.Text = "Submit";
            }
            #endregion Add Section
        }
    }
    protected void btnSubmitContactCategory_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);

        try
        {
            #region Open Connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            #endregion Open Connection

            #region Local Variable
            SqlString strContactCategoryName = SqlString.Null;

            #endregion Local Variable

            #region Server Side Validation
            String strErrorMessage = "";

            if (txtContactCategoryName.Text.Trim() == "")
            {
                strErrorMessage += "Kindly Enter ContactCategory Name <br/>";
            }
            if (strErrorMessage.Trim() != "")
            {
                lblErrorMessage.Text = strErrorMessage;
                return;
            }

            if (txtContactCategoryName.Text.Trim() != "")
            {
                strContactCategoryName = txtContactCategoryName.Text.Trim();
            }

            #endregion Server Side Validation

            #region Create Command
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);

            if (Request.QueryString["ContactCategoryID"] != null)
            {
                #region Update Record
                objCmd.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                objCmd.CommandText = "PR_ContactCetegory_UpdateByUserID&PK";
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
                }
                objCmd.ExecuteNonQuery();

                Response.Redirect("~/AdminPanel/ContactCategory/ContactCategory.aspx");
                #endregion Update Record

                #region Close Connection
                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();
                #endregion Close Connection

            }
            else
            {
                objCmd.CommandText = "PR_ContactCetegory_InsretByUserID";
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
                }
                objCmd.ExecuteNonQuery();
                objConn.Close();

                txtContactCategoryName.Text = "";
                txtContactCategoryName.Focus();

                lblSuccessMessage.Text = "Data Inserted Successfully";
            }
            #endregion Create Command
        }
        catch (Exception ex)
        {
            #region Exception Message
            Exception.Visible = true;
            lblCatchMessage.Text = ex.Message;
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

   

    private void fillControl(SqlInt32 ContactCategoryID)
    {
        #region Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Connection String

        try
        {
            #region Set Connection and Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_ContactCetegory_SelectByUserID&PK]";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }

            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion Set Connection and Command Object

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    #region Data Show in textbox
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                    #endregion Data Show in textbox
                }
            }

            else
            {
                lblSuccessMessage.Text = "No data available";
            }

            #region Connection
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }
        catch (Exception ex)
        {
            #region Exception Message
            Exception.Visible = true;
            lblCatchMessage.Text = ex.Message;
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
