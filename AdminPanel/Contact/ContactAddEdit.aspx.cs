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

public partial class WebPages_Contact_ContactAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region All Drop Down
            FillDropDownListCountry();
            FillDropDownListState();
            FillDropDownListCity();
            FillDropDownListContactCategory();
            #endregion All Drop Down

            #region Edit Section
            if (Request.QueryString["ContactID"] != null)
            {
                lblPageTitle.Text = "Contact Edit Data";
                btnSubmitContact.Text = "Edit";
                fillControl(Convert.ToInt32(Request.QueryString["ContactID"].ToString()));
            }
            #endregion Edit Section

            #region Add Section
            else
            {
                lblPageTitle.Text = "Contact Add Data";
                btnSubmitContact.Text = "Submit";
            }
            #endregion Add Section
        }
    }
    protected void btnSubmitContact_Click(object sender, EventArgs e)
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
            SqlInt32 strStateID = SqlInt32.Null;
            SqlInt32 strCityID = SqlInt32.Null;
            SqlInt32 strCategoryID = SqlInt32.Null;
            SqlInt32 strAge = SqlInt32.Null;
            SqlString strContactName = SqlString.Null;
            SqlString strAddress = SqlString.Null;
            SqlString strContactNo = SqlString.Null;
            SqlString strWhatsAppNo = SqlString.Null;
            SqlString strEmail = SqlString.Null;
            SqlString strFacebookId = SqlString.Null;
            SqlString strLinkedinId = SqlString.Null;
            SqlString strBloodgroup = SqlString.Null;
            SqlDateTime strBirthDate = SqlDateTime.Null;

            #endregion Local Variable

            #region Server Side Validation
            String strErrorMessage = "";

            if (ddlCountryList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select Country <br/>";
            }
            if (ddlStateList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select Country <br/>";
            } if (ddlCityList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select Country <br/>";
            } if (ddlContactCategoryList.SelectedIndex == 0)
            {
                strErrorMessage += "Kindly Select Country <br/>";
            }
            if (txtContactName.Text.Trim() == "")
            {
                strErrorMessage += "Kindly fill required Items <br/>";
            }
            if (txtAddress.Text.Trim() == "")
            {
                strErrorMessage += "Kindly fill required Items <br/>";
            }
            if (txtContactNumber.Text.Trim() == "")
            {
                strErrorMessage += "Kindly fill required Items <br/>";

            }
            if (txtEmail.Text.Trim() == "")
            {
                strErrorMessage += "Kindly fill required Items <br/>";
            }
            if (strErrorMessage.Trim() != "")
            {
                lblErrorMessage.Text = strErrorMessage;
                return;
            }

            if (ddlCountryList.SelectedIndex > 0)
                strCountryID = Convert.ToInt32(ddlCountryList.SelectedValue);
            if (ddlStateList.SelectedIndex > 0)
                strStateID = Convert.ToInt32(ddlStateList.SelectedValue);
            if (ddlCityList.SelectedIndex > 0)
                strCityID = Convert.ToInt32(ddlCityList.SelectedValue);
            if (ddlContactCategoryList.SelectedIndex > 0)
                strCategoryID = Convert.ToInt32(ddlContactCategoryList.SelectedValue);

            if (txtContactName.Text.Trim() != "")
                strContactName = txtContactName.Text.Trim();
            if (txtAddress.Text.Trim() != "")
                strAddress = txtAddress.Text.Trim();
            if (txtContactNumber.Text.Trim() != "")
                strContactNo = txtContactNumber.Text.Trim();
            if (txtWhatsappNumber.Text.Trim() != "")
                strWhatsAppNo = txtWhatsappNumber.Text.Trim();
            if (txtEmail.Text.Trim() != "")
                strEmail = txtEmail.Text.Trim();
            if (txtFaceBook.Text.Trim() != "")
                strFacebookId = txtFaceBook.Text.Trim();
            if (txtLinkedIn.Text.Trim() != "")
                strLinkedinId = txtLinkedIn.Text.Trim();
            if (txtBloodGroup.Text.Trim() != "")
                strBloodgroup = txtBloodGroup.Text.Trim();
            if (txtBirthdate.Text.Trim() != "")
                strBirthDate = Convert.ToDateTime(txtBirthdate.Text);
            if (txtAge.Text.Trim() != "")
                strAge = Convert.ToInt32(txtAge.Text);
            #endregion Server Side Validation

            #region Create Command
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", strCategoryID);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@FacebookID", strFacebookId);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedinId);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodgroup);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            #endregion Create Command

            if (Request.QueryString["ContactID"] != null)
            {
                #region Update Record

                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmd.CommandText = "[PR_Contact_UpdateByPK]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/WebPages/Contact/Contact.aspx");

                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "PR_Contact_Insert";
                objCmd.ExecuteNonQuery();
                objConn.Close();

                txtContactName.Text = "";
                txtContactNumber.Text = "";
                txtWhatsappNumber.Text = "";
                txtEmail.Text = "";
                txtFaceBook.Text = "";
                txtLinkedIn.Text = txtBloodGroup.Text = "";
                txtBirthdate.Text = "";
                txtAge.Text = "";
                ddlCountryList.SelectedIndex = 0;
                ddlStateList.SelectedIndex = 0;
                ddlCityList.SelectedIndex = 0;
                ddlContactCategoryList.SelectedIndex = 0;
                ddlCountryList.Focus();

                lblSuccessMessage.Text = "Data Inserted Successfully";

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
            #region Connection Close
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
            #endregion Connection Close
        }


    }





    private void fillControl(SqlInt32 ContactID)
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
            objCmd.CommandText = "[dbo].[PR_Contact_SelectByPK]";

            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #endregion Set Connection and Command Object

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    #region Data Show in textbox

                    #region Allow Not Null Textbox
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNumber.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    #endregion Allow Not Null Textbox

                    #region Allow Null Textbox
                    if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                    {
                        txtWhatsappNumber.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthdate.Text = objSDR["BirthDate"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFaceBook.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                    {
                        txtLinkedIn.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    #endregion Allow Null Textbox

                    #region All DropDown
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateList.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountryList.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddlCityList.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                    {
                        ddlContactCategoryList.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    }
                    #endregion All DropDown

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




    #region Country DropDown
    private void FillDropDownListCountry()
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
            objCmd.CommandText = "PR_Country_SelectForDropDownList";
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
    #endregion Country DropDown

    #region State DropDown
    private void FillDropDownListState()
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
            objCmd.CommandText = "PR_State_SelectForDropDownList";
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
    #endregion State DropDown

    #region City DropDown
    private void FillDropDownListCity()
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
            objCmd.CommandText = "PR_City_SelectForDropDownList";
            #endregion Command Object

            #region Execute Data and Data Bind
            SqlDataReader objSDR = objCmd.ExecuteReader();


            if (objSDR.HasRows == true)
            {
                ddlCityList.DataSource = objSDR;
                ddlCityList.DataValueField = "CityID";
                ddlCityList.DataTextField = "CityName";
                ddlCityList.DataBind();
            }
            #endregion Execute Data and Data Bind

            ddlCityList.Items.Insert(0, new ListItem("---- Select City Name ----", "-1"));

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
    #endregion State DropDown

    #region ContactCategory DropDown
    private void FillDropDownListContactCategory()
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
            objCmd.CommandText = "PR_ContactCategory_SelectForDropDownList";
            #endregion Command Object

            #region Execute Data and Data Bind
            SqlDataReader objSDR = objCmd.ExecuteReader();


            if (objSDR.HasRows == true)
            {
                ddlContactCategoryList.DataSource = objSDR;
                ddlContactCategoryList.DataValueField = "ContactCategoryID";
                ddlContactCategoryList.DataTextField = "ContactCategoryName";
                ddlContactCategoryList.DataBind();
            }

            #endregion Execute Data and Data Bind

            ddlContactCategoryList.Items.Insert(0, new ListItem("---- Select ContactCategory Name ----", "-1"));

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
    #endregion ContactCategory DropDown
}