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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Edit Section
            if (Request.QueryString["CountryID"] != null)
            {
                lblPageTitle.Text = "Country Edit Data";
                btnSubmitCountry.Text = "Edit";

                FillControl(Convert.ToInt32(Request.QueryString["CountryID"].ToString()));
            }
            #endregion Edit Section

            #region Add Section
            else
            {
                lblPageTitle.Text = "Country Add Data";
                btnSubmitCountry.Text = "Submit";
            }
            #endregion Add Section
        }
    }

    #endregion Page Load

    protected void btnSubmitCountry_Click(object sender, EventArgs e)
    {

        #region Local Variable and Connection String

        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;


        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);

        #endregion Local Variable and Connection String

        try
        {
            #region Server Side Validation
            string strErrorMessage = "";

            if (txtCountryName.Text.Trim() == "")
            {
                strErrorMessage += "Enter Name";
            }
            if (strErrorMessage != "")
            {
                lblErrorMessage.Text = strErrorMessage;
                return;
            }

            #endregion Server Side Validation

            #region Set Connection and Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmdAdd = objConn.CreateCommand();
            objCmdAdd.CommandType = CommandType.StoredProcedure;

            strCountryName = txtCountryName.Text.Trim();
            strCountryCode = txtCountryCode.Text.Trim();

            objCmdAdd.Parameters.AddWithValue("@CountryName", strCountryName);
            objCmdAdd.Parameters.AddWithValue("@CountryCode", strCountryCode);

            #endregion Set Connection and Command Object

            if (Request.QueryString["CountryID"] != null)
            {
                #region Update Record

                objCmdAdd.Parameters.AddWithValue("@CountryID", Request.QueryString["CountryID"].ToString().Trim());
                objCmdAdd.CommandText = "PR_Country_UpdateUserID&PK";
                objCmdAdd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                objCmdAdd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/Country.aspx");

                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();

                #endregion Update Record
            }
            else
            {
                #region Insert Record

                objCmdAdd.CommandText = "PR_Country_InsertByUserID";
                objCmdAdd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                objCmdAdd.ExecuteNonQuery();

                lblSuccessMessage.Text = "Data Enter Success Fully";

                txtCountryName.Text = txtCountryCode.Text = "";

                txtCountryName.Focus();

                #endregion Insert Record
            }

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
            #region Close Connection
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Close Connection
        }
    }


    private void FillControl(SqlInt32 CountryID)
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
            objCmd.CommandText = "[dbo].[PR_Country_SelectByUserID&PK]";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion Set Connection and Command Object

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    #region Data Show in textbox
                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                    #endregion Data Show in textbox
                }
            }

            else
            {
                lblSuccessMessage.Text = "No data available";
            }

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

}