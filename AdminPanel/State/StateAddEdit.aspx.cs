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

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();
            #region Edit Section
            if (Request.QueryString["StateID"] != null)
            {
                lblPageTitle.Text = "State Edit Data";
                btnSubmitState.Text = "Edit";
                fillControl(Convert.ToInt32(Request.QueryString["StateID"].ToString()));
            }
            #endregion Edit Section

            #region Add Section
            else
            {
                lblPageTitle.Text = "State Add Data";
                btnSubmitState.Text = "Submit";
            }
            #endregion Add Section
        }
    }

    private void FillDropDownList()
    {
        #region Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Connection String

        try
        {
            #region Set Connection and Command Object
            objConn.Open();
            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            #endregion Set Connection and Command Object

            #region Command Execute and DataBind
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCountryList.DataSource = objSDR;
                ddlCountryList.DataValueField = "CountryID";
                ddlCountryList.DataTextField = "CountryName";
                ddlCountryList.DataBind();
            }
            #endregion Command Execute and DataBind

            ddlCountryList.Items.Insert(0, new ListItem("---- Select Country Name ----", "-1"));
            
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

    protected void btnSubmitState_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);

        try
        {
            #region Open Connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            #endregion Open Connection

            #region Local Variable
            SqlInt32 strCountryID = SqlInt32.Null;
            SqlString strStateName = SqlString.Null;
            SqlString strStateCode = SqlString.Null;
            #endregion Local Variable

            #region Server Side Validation
            String strErrorMessage = "";

            if (ddlCountryList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select Country <br/>";
            }
            if (txtStateName.Text.Trim() == "")
            {
                strErrorMessage += "Kindly Enter State Name <br/>";
            }
            if (strErrorMessage.Trim() != "")
            {
                lblErrorMessage.Text = strErrorMessage;
                return;
            }

            if (ddlCountryList.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountryList.SelectedValue);
            }
            if (txtStateName.Text.Trim() != "")
            {
                strStateName = txtStateName.Text.Trim();
            }
            if (txtStateCode.Text.Trim() != "")
            {
                strStateCode = txtStateCode.Text.Trim();
            }
            #endregion Server Side Validation

            #region Create Command
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);


            if (Request.QueryString["StateID"] != null)
            {
                #region Update Record

                objCmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                objCmd.CommandText = "PR_State_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/WebPages/State/State.aspx");

                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();

                #endregion Update Record
            }
            else
            {
                objCmd.CommandText = "PR_State_InsretByUserID";
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
                }
                objCmd.ExecuteNonQuery();
                objConn.Close();

                txtStateName.Text = txtStateCode.Text = "";
                ddlCountryList.SelectedIndex = 0;
                ddlCountryList.Focus();

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

   


    private void fillControl(SqlInt32 StateID)
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
            objCmd.CommandText = "[dbo].[PR_State_SelectByUserID&PK]";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }

            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion Set Connection and Command Object

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    #region Data Show in textbox
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }
                    if(!objSDR["CountryID"].Equals(DBNull.Value)){
                        ddlCountryList.SelectedValue = objSDR["CountryID"].ToString().Trim();
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