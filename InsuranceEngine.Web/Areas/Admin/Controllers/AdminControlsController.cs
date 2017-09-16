using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.DTO.AttributeNames;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.Web.Areas.Admin.Models;
using System;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AdminControlsController : Controller
    {

        public ActionResult TreeView()
        {
            TreeViewDataVM data = new TreeViewDataVM();
            if (RouteData.Values.ContainsKey(RouteAttributeNames.SchemeID))
            {
                data.SchemeID = Convert.ToInt32(RouteData.Values[RouteAttributeNames.SchemeID]);
            }
            return View(data);
        }

        [Route("AdminControls/GetTreeNodes/{" + RouteAttributeNames.SchemeID + ":int}")]
        public JsonResult GetTreeNodes(int schemeId)
        {
            TreeNodeDTO root = QuestionnaireAdminBusinessManager.Instance.GetTreeStructureForScheme(schemeId);
            //set up expanded items and selected item
            root = SetSelectedNode(root);
            return Json(root, JsonRequestBehavior.AllowGet);
        }

        private TreeNodeDTO SetSelectedNode(TreeNodeDTO root)
        {
            string id = string.Empty;

            string test = Request.RawUrl;

            root.state = new TreeNodeStateDTO() { disabled = false, opened = true, selected = false };
            
            root.children.ForEach(folder => 
            {
                folder.state = new TreeNodeStateDTO() { disabled = false, opened = true, selected = false }; //folder level
                folder.children.ForEach(item =>
                    {
                        item.state = new TreeNodeStateDTO() { disabled = false, opened = true, selected = false }; //item level
                        if (item.children != null && item.children.Count > 0)
                        {
                            item.children.ForEach(childItem =>
                            {
                                childItem.state = new TreeNodeStateDTO() { disabled = false, opened = true, selected = false };
                            });
                        }
                    }
                );
            });

            return root;
        }

        

	}
}