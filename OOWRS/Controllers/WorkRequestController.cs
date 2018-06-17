using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOWRS.Models;
using System.IO;
using OOWRS.Repository;

namespace OOWRS.Controllers
{
    public class WorkRequestController : Controller
    {

        private IWorkRepository _WorkRequestRepository = new WorkRequestRepository();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            
            return View("CreateWorkRequest");
        }



        public ActionResult Edit(int id)
        {

            var selected = _WorkRequestRepository.GetWorkRequest(id);
            return View("EditWorkRequest",selected);
        }


        public ActionResult Delete(int id)
        {
            _WorkRequestRepository.DeleteWorkRequest(id);

            return View("AllWorkRequests", _WorkRequestRepository.GetAllWorkRequests());
        }


        public ActionResult Details(int id)
        {
    
            return View("WorkRequestDetails",_WorkRequestRepository.GetWorkRequest(id));
        }


        public ActionResult AllWorkRequests()
        {
           
            return View(_WorkRequestRepository.GetAllWorkRequests());
        }


        public ActionResult Search()
        {
            return View("WorkRequest");
        }


        [HttpPost]
        public ActionResult Save(WorkRequestModel workRequest)
        {
          
            _WorkRequestRepository.SaveWorkRequest(workRequest);
            return View("~/Views/WorkRequest/AllWorkRequests.cshtml", _WorkRequestRepository.GetAllWorkRequests());
        }

        [HttpPost]
        public ActionResult Create(WorkRequestModel workRequest)
        {
            HttpCookie userCookie = HttpContext.Request.Cookies["UserCookie"] as HttpCookie;
            workRequest.OwnerName = userCookie["UserName"];
            workRequest.CreatedDate = DateTime.Now;
            _WorkRequestRepository.SaveWorkRequest(workRequest);
            
            return View("~/Views/WorkRequest/AllWorkRequests.cshtml",_WorkRequestRepository.GetAllWorkRequests());
        }

    }
}