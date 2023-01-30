using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Management.Web.Controllers
{
    public class RequestAssetController : Controller
    {
        private readonly RequestAssetService _service;

        public RequestAssetController(ApplicationDbContext context)
        {
            _service = new RequestAssetService(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var request = _service.GetRequestAssets();
            return View(request);
        }
        [HttpGet]
        public IActionResult Approve(long? id)
        {
            _service.ApproveRequest(id);
            return Redirect("/RequestAsset");
        }

        [HttpGet]
        public IActionResult Reject(long? id)
        {
            //var approval = _service.RejectRequest(id);
            _service.RejectRequest(id);

            //return View(approval);
            return Redirect("/RequestAsset");
        }

        /*[HttpPost]
        public IActionResult SaveApproval([Bind("Id, RequestAssetId, Status, Reason, UpdatedDate")] ApprovalReq req)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateApproval(req);
                return Redirect("Index");
            }
            return View("Reject", req);
        }*/
    }
}
