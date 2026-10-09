using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class AppMaster : System.Web.UI.MasterPage
{
    readonly MenuController _menuCtl = new MenuController();
    readonly RoleManagementController _objRole = new RoleManagementController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int roleId = (int)Session["RoleID"];
            lblUser.Text = Session["UserName"].ToString();
            lblWorkingDate.Text = ((DateTime)this.Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            switch (Request.QueryString["LevelType"])
            {
                case null:
                    LoadMainMenu(roleId, 2, Constants.IntNullValue);
                    break;
                case "1":
                    gvMain.DataSource = null;
                    gvMain.DataBind();
                    gvSubMenu2.DataSource = null;
                    gvSubMenu2.DataBind();
                    LoadSubMenu1(roleId, 3, Convert.ToInt32(Request.QueryString["LevelID"]));
                    break;
                case "2":
                    imgAddMenu.Visible = false;
                    gvMain.DataSource = null;
                    gvMain.DataBind();
                    gvSubMenu1.DataSource = null;
                    gvSubMenu1.DataBind();
                    LoadSubMenu2(roleId, 4, Convert.ToInt32(Request.QueryString["LevelID"]));
                    break;
                case "3":
                {
                    imgAddMenu.Visible = false;

                    gvMain.DataSource = null;
                    gvMain.DataBind();

                   
                    gvSubMenu1.DataSource = null;
                    gvSubMenu1.DataBind();

                    DataTable dtSubMenu2 = _objRole.SelectRoleWiseModule(roleId, 5, Convert.ToInt32(Request.QueryString["LevelID"]));

                    var trdLayerId = Convert.ToInt32(dtSubMenu2.Rows[0]["TrdLayerId"].ToString());

                    LoadSubMenu2(roleId, 4, trdLayerId);
                }
                    break;
            }
        }
    }

    private void LoadMainMenu(int roleId, int typeId, int moduleId)
    {
       
        DataTable dtParent = _objRole.SelectRoleWiseModule(roleId, typeId, moduleId);
        if (dtParent == null) return;
        gvMain.DataSource = dtParent;
        gvMain.DataBind();
    }

    private void LoadSubMenu1(int roleId, int typeId, int moduleId)
    {
       
        DataTable dtSubMenu1 = _objRole.SelectRoleWiseModule(roleId, typeId, moduleId);
        if (dtSubMenu1 == null) return;
        gvSubMenu1.DataSource = dtSubMenu1;
        gvSubMenu1.DataBind();
    }

    private void LoadSubMenu2(int roleId, int typeId, int moduleId)
    {
       
        DataTable dtSubMenu2 = _objRole.SelectRoleWiseModule(roleId, typeId, moduleId);
        if (dtSubMenu2 == null) return;
        gvSubMenu2.DataSource = dtSubMenu2;
        gvSubMenu2.DataBind();
    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int roleId = (int)Session["RoleID"];
            var btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            var btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            var btnView = (LinkButton)e.Row.FindControl("btnView");
            if (roleId == 1) return;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnView.Visible = false;
            imgAddMenu.Visible = false;
        }
    }

    protected void gvSubMenu1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        int roleId = (int)Session["RoleID"];
        var btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
        var btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
        var btnView = (LinkButton)e.Row.FindControl("btnView");
        if (roleId == 1) return;
        btnEdit.Visible = false;
        btnDelete.Visible = false;
        btnView.Visible = false;
        imgAddMenu.Visible = false;
    }

    protected void gvSubMenu2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        int roleId = (int)Session["RoleID"];
        var btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
        var btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
        if (roleId == 1) return;
        btnEdit.Visible = false;
        btnDelete.Visible = false;
        imgAddMenu.Visible = false;
    }

    protected void imgAddMenu_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["LevelID"] != null)
        {
            Response.Redirect("Menu.aspx?LevelID=" + Request.QueryString["LevelID"] + "&LevelType=" + Request.QueryString["LevelType"]);
        }
        else
        {
            Response.Redirect("Menu.aspx?");
        }
    }

    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":
                Response.Redirect("Menu.aspx?mode=edit&LevelID=" + e.CommandArgument + "&LevelType=1");
                break;
            case "view":
                Response.Redirect("Level.aspx?LevelID=" + e.CommandArgument + "&LevelType=1");
                break;
            case "delete":
            {
                DataTable dtDelete = _menuCtl.DeleteMenuLevel(Convert.ToInt32(e.CommandArgument));
                if (dtDelete.Rows[0]["Message"].ToString() != "NO")
                {
                    Response.Redirect("Home.aspx?");
                }
                else
                {
                    Session.Add("Message", "Can not be deleted. This level has child levels.");
                    Response.Redirect("Home.aspx?");
                }
            }
                break;
        }
    }

    protected void gvSubMenu1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":
                Response.Redirect("Menu.aspx?mode=edit&LevelID=" + e.CommandArgument + "&LevelType=2");
                break;
            case "view":
                Response.Redirect("Level2.aspx?LevelID=" + e.CommandArgument + "&LevelType=2");
                break;
            case "delete":
            {
                DataTable dtDelete = _menuCtl.DeleteMenuLevel(Convert.ToInt32(e.CommandArgument));
                if (dtDelete.Rows[0]["Message"].ToString() != "NO")
                {
                    if (Request.QueryString["LevelID"] != null)
                    {
                        Response.Redirect("Home.aspx?LevelID=" + dtDelete.Rows[0]["Message"] + "&LevelType=" + Request.QueryString["LevelType"]);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx?");
                    }
                }
                else
                {
                    Session.Add("Message", "Can not be deleted. This level has child levels.");
                    if (Request.QueryString["LevelID"] != null)
                    {
                        Response.Redirect("Home.aspx?LevelID=" + Request.QueryString["LevelID"] + "&LevelType=" + Request.QueryString["LevelType"]);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx?");
                    }
                }
            }
                break;
        }
    }

    protected void gvSubMenu2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":
                Response.Redirect("Menu.aspx?mode=edit&LevelID=" + e.CommandArgument + "&LevelType=3");
                break;
            case "view":
            {
                var row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int rowIndex = row.RowIndex;
                var dataKey = gvSubMenu2.DataKeys[rowIndex];
                if (dataKey != null)
                    Response.Redirect(dataKey["MODULE_ID"] + "?LevelType=3&LevelID=" + e.CommandArgument);
            }
                break;
            case "delete":
            {
                DataTable dtDelete = _menuCtl.DeleteMenuLevel(Convert.ToInt32(e.CommandArgument));
                if (dtDelete.Rows[0]["Message"].ToString() != "NO")
                {
                    if (Request.QueryString["LevelID"] != null)
                    {
                        Response.Redirect("Home.aspx?LevelID=" + dtDelete.Rows[0]["Message"] + "&LevelType=" + Request.QueryString["LevelType"]);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx?");
                    }
                }
                else
                {
                    Session.Add("Message", "Can not be deleted. This level has child levels.");

                    if (Request.QueryString["LevelID"] != null)
                    {
                        Response.Redirect("Home.aspx?LevelID=" + Request.QueryString["LevelID"] + "&LevelType=" + Request.QueryString["LevelType"]);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx?");
                    }
                }
            }
                break;
        }
    }
}
