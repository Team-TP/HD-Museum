using HD.Station.Feature.Models;
using HD.Station.Museum;
using HD.Station.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HD.Station.Feature.Mvc
{
    [Area("Museums")]
    public class CategoriesController : Controller
    {
        private ICategoryManager _manager;
        public CategoriesController(ICategoryManager manager)
        {
            _manager = manager;
        }
        [HttpGet("[area]/[controller]")]
        public IActionResult Index(string filter, bool includeDisabled)
        {
            var model = new CategoriesIndexViewModel
            {
                Filter = filter,
                IncludeDisabled = includeDisabled
            };
            return View(model);
        }
        [Permission(nameof(Index))]
        public virtual async Task<IActionResult> ReadAsync([DataSourceRequest] DataSourceRequest request, [FromForm] string filterText, [FromForm] bool includeDisabled)
        {
            try
            {
                var start = DateTime.Now;
                var query = await _manager.QueryAsync(filterText, includeDisabled);
                var result = await query.ToDataSourceResultAsync(request);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, ex.StackTrace });
            }
        }
        [HttpGet]
        public virtual IActionResult Create(Guid id, string layout = "_")
        {
            var categories = new CategoriesEditViewModel(null);
            ViewData["Layout"] = layout == "_" ? "" : layout;

            //if (id == Guid.Empty)
            //{
            //    ViewBag.Id = null;
            //}
            //else
            //{
            //    ViewBag.Id = id;
            //}

            //categories.ParentId = id;
            return View(categories);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(CategoriesEditViewModel model,  IEnumerable<Guid> parentIds)
        {
            var categories = model.ToModel();
            //Guid paId = new Guid();
            if (parentIds == null)
            {
                categories.ParentId = null;
            }
            else
            {
                categories.ParentId = parentIds.First();
            }          
            var (state, edit) = await _manager.AddAsync(categories);
            return RedirectToAction("Details", new { id = edit.Id });
        }
        [HttpGet("[area]/[controller]/{id:guid}")]
        public virtual async Task<IActionResult> DetailsAsync(Guid id)
        {
            var (state, viewItem) = await _manager.GetByIdAsync(id);
            var model = new CategoriesViewModel(viewItem);
            return View(model);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new CategoriesViewModel(viewItem);

            ViewBag.Id = id;
            return View(model);
        }
        public virtual async Task<IActionResult> DeleteConfirmedAsync(Guid id)
        {
            var state = await _manager.DeleteByIdAsync(id);
            if (state.Succeeded)
            {
                return RedirectToAction("Deleted");
            }
            ViewBag.Notice = state.ToAlert();
            return await DeleteAsync(id);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeletedAsync()
        {
            ViewBag.Notice = new AlertModel
            {
                Contextual = "success",
                Message = "Da xoa thanh cong",
                Title = "Ok"
            };
            return View(new CategoriesViewModel());
        }
        [HttpGet]
        public virtual async Task<IActionResult> EditAsync(Guid id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var (state, categories) = await _manager.GetByIdAsync(id);
            return View(new CategoriesEditViewModel(categories));
        }

        [HttpPost]
        public virtual async Task<IActionResult> EditAsync(CategoriesEditViewModel categories)
        {
            var dt = categories.ToModel();

            var result = await _manager.UpdateAsync(dt);

            if (result.Succeeded)
            {
                ViewBag.Notice = new AlertModel
                {
                    Contextual = "success",
                    Message = "Da sua thanh cong",
                    Title = "Ok"
                };

                return RedirectToAction("Details", new { id = dt.Id });
            }
            ViewBag.Notice = result.ToAlert();
            return View(categories);

        }
        [HttpGet]
        public virtual async Task<IActionResult> GetCategoriesAsync(Guid? id)
        {

            var data = (await _manager.QueryAsync()).Where(c => c.ParentId == id)
                .Select(c => new CategoriesTreeModel(c)).ToList();

            return Json(data);
        }
        [HttpGet]
        public virtual async Task<IActionResult> GetCategoriesEditAsync([FromForm]Guid? id, Guid parentId)
        {

            var data = (await _manager.QueryAsync()).Where(c => (c.ParentId == id) && (c.Id != parentId))
                .Select(c => new CategoriesTreeModel(c)).ToList();

            return Json(data);
        }
        public class CategoriesTreeModel {
            public CategoriesTreeModel(Categories item)
            {
                Id = item.Id;
                ParentId = item.ParentId;
                Name = item.Name;
                HasChildren = item.ChildrenCategory.Count > 0;

            }
            public Guid? Id { get; private set; }

            public Guid? ParentId { get; private set; }

            public string Name { get; set; }
            public bool HasChildren { get; set; }


        }
        //[HttpGet]
        //public virtual async Task<IActionResult> GetCategoriesAsync()
        //{
        //    var data = (await _manager.QueryAsync()).Select(c => new CategoriesViewModel(c)).ToList();
        //    return Json(data);
        //}
    }
}

