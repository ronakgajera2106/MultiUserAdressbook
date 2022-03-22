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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();
            #region Edit Section
            if (Request.QueryString["CityID"] != null)
            {
                lblPageTitle.Text = "City Edit Data";
                btnSubmitCity.Text = "Edit";
                fillControl(Convert.ToInt32(Request.QueryString["CityID"].ToString()));
            }
            #endregion Edit Section

            #region Add Section
            else
            {
                lblPageTitle.Text = "City Add Data";
                btnSubmitCity.Text = "Submit";
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
            #region Open Connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            #endregion Open Connection

            #region Command Object
            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectForDropDownListByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            #endregion Command Object

            #region Execute Data and Data Bind
            SqlDataReader objSDR = objCmd.ExecuteReader();


            if (objSDR.HasRows == true)
            {
                ddlStateList.DataSource = objSDR;
                ddlStateList.DataValueField = "StateID";
                ddlStateList.DataTextField = "StateName";
                ddlStateList.DataBind();
            }
            #endregion Execute Data and Data Bind

            ddlStateList.Items.Insert(0, new ListItem("---- Select State Name ----", "-1"));

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

    protected void btnSubmitCity_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);

        try
        {
            #region Open Connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            #endregion Open Connection

            #region Local Variable
            SqlInt32 strStateID = SqlInt32.Null;
            SqlString strCityName = SqlString.Null;
            SqlString strStdCode = SqlString.Null;
            SqlString strPinCode = SqlString.Null;

            #endregion Local Variable

            #region Server Side Validation
            String strErrorMessage = "";

            if (ddlStateList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select State <br/>";
            }
            if (txtCityName.Text.Trim() == "")
            {
                strErrorMessage += "Kindly Enter City Name <br/>";
            }
            if (strErrorMessage.Trim() != "")
            {
                lblErrorMessage.Text = strErrorMessage;
                return;
            }

            if (ddlStateList.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlStateList.SelectedValue);
            }
            if (txtCityName.Text.Trim() != "")
            {
                strCityName = txtCityName.Text.Trim();
            }
            if (txtStdCode.Text.Trim() != "")
            {
                strStdCode = txtStdCode.Text.Trim();
            }
            if (txtPinCode.Text.Trim() != "")
            {
                strPinCode = txtPinCode.Text.Trim();
            }
            #endregion Server Side Validation

            #region Create Command
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityName", strCityName);
            objCmd.Parameters.AddWithValue("@STDCode", strStdCode);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);

            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            if (Request.QueryString["CityID"] != null)
            {
                #region Update Record

                objCmd.Parameters.AddWithValue("@CityID", Request.QueryString["CityID"].ToString().Trim());
                objCmd.CommandText = "PR_City_UpdateByUserID&PK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/City.aspx");

                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();

                #endregion Update Record
            }
            else
            {
                objCmd.CommandText = "PR_City_InsertByUserID";
                objCmd.ExecuteNonQuery();
                objConn.Close();

                txtCityName.Text = txtStdCode.Text = "";
                ddlStateList.SelectedIndex = 0;
                ddlStateList.Focus();

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



    private void fillControl(SqlInt32 CityID)
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
            objCmd.CommandText = "[dbo].[PR_City_SelectByUserID&PK]";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }

            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion Set Connection and Command Object

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    #region Data Show in textbox
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtStdCode.Text = objSDR["StdCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateList.SelectedValue = objSDR["StateID"].ToString().Trim();
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