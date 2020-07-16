using HD.Station.Feature.Models;
using HD.Station.Museum;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Feature.Mvc
{
    [Area("Museums")]
    public class CoursesController : Controller
    {
        private ICourseManager _manager;
        public CoursesController(ICourseManager manager)
        {
            _manager = manager;
        }
        [HttpGet("[area]/[controller]")]
        public IActionResult Index(string filter, bool includeDisabled)
        {
            var model = new CoursesIndexViewModel
            {
                Filter = filter,
                IncludeDisabled = includeDisabled
            };
            return View(model);
        }
        [HD.Station.Security.Permission(nameof(Index))]
        public virtual async Task<IActionResult> ReadAsync([DataSourceRequest]DataSourceRequest request, [FromForm] string filterText, [FromForm] bool includeDisabled)
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
        public virtual IActionResult Create(string layout = "_")
        {
            var courses = new CoursesEditViewModel(null);
            ViewData["Layout"] = layout == "_" ? "" : layout;
            return View(courses);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(CoursesEditViewModel model)
        {
            var course = model.ToModel();
            var (state, edit) = await _manager.AddAsync(course);
            return RedirectToAction("Details", new { Id = edit.Id });
        }
        [HttpGet("[area]/[controller]/{id:guid}")]
        public virtual async Task<IActionResult> DetailsAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new CoursesViewModel(viewItem);
            return View(model);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new CoursesViewModel(viewItem);

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
            return View(new CoursesViewModel());
        }
        [HttpGet]
        public virtual async Task<IActionResult> EditAsync(Guid id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var (state, course) = await _manager.ReadByIdAsync(id);
            return View(new CoursesEditViewModel(course));
        }

        [HttpPost]
        public virtual async Task<IActionResult> EditAsync(CoursesEditViewModel book)
        {
            var dt = book.ToModel();

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
            return View(book);

        }
    }
}
