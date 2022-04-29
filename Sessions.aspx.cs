using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace eFormsBiz
{
    public partial class Sessions : System.Web.UI.Page
    {
        private CSessionManager m_SessionMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_SessionMgr = new CSessionManager();

            if (Session[Strings.Session_Logout] != null)
            {
                Response.Redirect(Strings.Path_Default);
            }
            else
            {
                if (Session[Strings.Session_Username] == null)
                {
                    try
                    {
                        System.Security.Principal.WindowsPrincipal p = System.Threading.Thread.CurrentPrincipal as System.Security.Principal.WindowsPrincipal;

                        try
                        {
                            if (Functions.Check_If_Generic_User(p.Identity.Name.ToLower().Replace(Strings.DOMAIN, ""), Strings.Domain_AM))
                            {
                                Session[Strings.Session_Username] = null;
                                Session[Strings.Session_Logout] = true;
                                Response.Redirect(Strings.Path_Default);
                            }
                            else
                            {
                                Session[Strings.Session_Username] = p.Identity.Name.ToLower().Replace(Strings.DOMAIN, "");

                                if (Session[Strings.Session_Username].ToString().Length > 0)
                                {
                                    lblUsername.Text = Session[Strings.Session_Username].ToString();
                                }
                            }
                        }
                        catch
                        {
                            Session[Strings.Session_Username] = null;
                            Session[Strings.Session_Logout] = true;
                            Response.Redirect(Strings.Path_Default);
                        }
                    }
                    catch
                    {
                        Session[Strings.Session_Username] = null;
                        Session[Strings.Session_Logout] = true;
                        Response.Redirect(Strings.Path_Default);
                    }
                }
                else
                {
                    lblUsername.Text = Session[Strings.Session_Username].ToString();

                }
            }


            if (Session[Strings.Session_Username] != null)
            {
                // Access to all users
                //if (!Functions.Check_If_Member_Of_AD_Group(Session[Strings.Session_Username].ToString(), Strings.s_eForms_BI_Group, Strings.s_eForms_BI_Group_Domain))
                //{
                //    Response.Redirect(Strings.Path_Default);
                //}
            }
            else
            {
                Response.Redirect(Strings.Path_Default);
            }


            if (Page.IsPostBack)
            {
                postback();
            }
            else
            {
                not_postback();
            }

        }

        private void postback()
        {
            
        }

        private void not_postback()
        {
            lblTitle.Text = Strings.sTitle_TS;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session[Strings.Session_Username] = null;
            Session[Strings.Session_Logout] = true;
            Response.Redirect(Strings.Path_Default);
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // refresh the grid
                dsSessions.DataBind();
                gvSessions.DataBind();

                lblErrorMessage.Text = dsSessions.SelectCountMethod + " Sessions";
            }
            catch
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtServerName.Text = "";
            chkStatus.ClearSelection();
            foreach (ListItem item in chkStatus.Items)
            {
                item.Selected = item.Value.Contains("Evaluation") || item.Value.Contains("In Progress");
            }
        }

    }
}
