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
using System.IO;

public partial class BTUpload : System.Web.UI.Page
{
    //public string SubFolder
    //{
    //    get
    //    {
    //        if (Request["type"] != null)
    //            return Request["type"].ToString();
    //        else
    //            return string.Empty;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        string folder = ResolveUrl("~/images/ckeditor/");
        if (Request.Files.Count > 0)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string filename = Path.GetFileName(Request.Files[i].FileName);
                Request.Files[i].SaveAs(Server.MapPath(folder)+filename);
                liScript.Text = "var CKEditorFuncNum = " + Request["CKEditorFuncNum"] + ";window.parent.CKEDITOR.tools.callFunction( CKEditorFuncNum, '"+folder+filename+"');";
            }
        }
    }
}
