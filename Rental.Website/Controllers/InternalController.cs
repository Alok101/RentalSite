using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rental.Schema.Internal;
using Rental.Website.Factory;
using Rental.Website.Models;
using Rental.Website.Utilities;
using Rental.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.Website.Controllers
{
    [Route("internal")]
    public class InternalController : Controller
    {
        private readonly IOptions<ConfigureServiceModel> appSettings;

        public InternalController(IOptions<ConfigureServiceModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("common")]
        public IActionResult FieldMaster()
        {
            return View("../Internal/CommonMaster", new FieldMasterViewModel());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMaster([Bind(include:"GroupCode,Name,Status,Description")]FieldMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                FieldMaster fmaster = new FieldMaster()
                {
                    GroupCode = model.GroupCode.ToUpper(),
                    Name = model.Name,
                    Status = model.Status,
                    CreateDate = CommonMethod.GetCurrentDate(),
                    Description = model.Description
                };
                var response = await ApiClientFactory.Instance.SaveCommonMasterDetails(fmaster);
                if (response.StatusCode == 201)
                {
                    return RedirectToAction("FieldMaster");
                }
            }

            return View("../Internal/CommonMaster", model);
        }

        

        [HttpPost]
        [Route("master/update")]
        public async Task<IActionResult> UpdateMaster(FieldMasterViewModel model)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={model.Id} cannot be found";
                return View("../Error/NotFound");
            }
            else
            {
                    JsonPatchDocument<FieldMaster> patchDoc = new JsonPatchDocument<FieldMaster>();
                    patchDoc.Replace(e => e.Status, model.Status);
                    patchDoc.Replace(e => e.Description, model.Description);
                    var response = await ApiClientFactory.Instance.UpdateFieldMaster(model.Id, patchDoc);
                    return RedirectToAction("FieldMaster");
            }
        }

        [HttpGet]
        [Route("masters/all/groups")]
        public async Task<IActionResult> GetAllGroupRecords()
        {
            var response = await ApiClientFactory.Instance.GetAllGroupRecords();
            return new JsonResult(response);
        }

        [HttpDelete]
        [Route("masters/record/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var response = await ApiClientFactory.Instance.DeleteFieldRecords(id);
                return StatusCode(204);
            }
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsMasterNameInUse(string name)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("name", name);
            var response = await ApiClientFactory.Instance.GetMasterDetailsByName(keyValuePairs);

            if (response == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Master {name} is already in use");
            }
        }
    }
}