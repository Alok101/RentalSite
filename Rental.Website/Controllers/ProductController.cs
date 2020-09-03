using Microsoft.AspNetCore.Mvc;
using Rental.Website.ViewModels;

namespace Rental.Website.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        [Route("items")]
        [HttpGet]
        public ActionResult ShowProduct()
        {
            return View("../Product/Product");
        }

        // GET: Product/Details/5
        [Route("items")]
        [HttpPost]
        public ActionResult AddNewItems()
        {
            return View();
        }

        [Route("attributes")]
        [HttpGet]
        public ActionResult ViewAttributes()
        {
            AttributeViewModel model = new AttributeViewModel();
            return View("../Product/Attributes",model);
        }

        [Route("attributes")]
        [HttpPost]
        public ActionResult AddNewAttributes(AttributeViewModel model)
        {
            return View("../Product/Attributes");
        }
        [Route("tags")]
        [HttpGet]
        public ActionResult ViewTags()
        {
            return View("../Product/Tags");
        }

        [Route("tags")]
        [HttpPost]
        public ActionResult AddNewTags()
        {
            return View("../Product/Tags");
        }
        [Route("categories")]
        [HttpGet]
        public ActionResult ViewCategories()
        {
            return View("../Product/Categories");
        }

        [Route("categories")]
        [HttpPost]
        public ActionResult AddNewCategories()
        {
            return View("../Product/Categories");
        }
    }
}