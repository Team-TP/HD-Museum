using HD.Station.Feature.Models;
using HD.Station.Museum;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HD.Station.Security;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HD.Station.Feature.Mvc
{
    [Area("Museums")]
    public class EnrollmentsController : Controller
    {
        private IEnrollmentManager _manager;
        private IStudentManager _Smanager;
        private ICourseManager _Cmanager;
        public EnrollmentsController(IEnrollmentManager manager, IStudentManager smanager, ICourseManager cmanager)
        {
            _manager = manager;
            _Smanager = smanager;
            _Cmanager = cmanager;

        }
        [HttpGet("[area]/[controller]")]
        public IActionResult Index(string filter, bool includeDisabled)
        {
            var model = new EnrollmentsIndexViewModel
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
                var query = (await _manager.QueryAsync()).Select(a => new EnrollmentsViewModel(a));
                var result = await query.ToDataSourceResultAsync(request);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, ex.StackTrace });
            }
        }
        [HttpGet]
        public virtual async Task<IActionResult> CreateAsync(Guid id, string layout = "_")
        {           
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var enrollments = new EnrollmentsEditViewModel(null)
                
            {
                //Students = await GetStudentsSelectListAsync(),
                //Courses = await GetCoursesSelectListAsync()
            };
            return View(enrollments);
        }
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(EnrollmentsEditViewModel model)
        {
            
            var enrollment = model.ToModel();
            var (state, edit) = await _manager.AddAsync(enrollment);
            return RedirectToAction("Details", new { Id = edit.Id });
        }
        [HttpGet("[area]/[controller]/{id:guid}")]
        public virtual async Task<IActionResult> DetailsAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new EnrollmentsViewModel(viewItem);
            return View(model);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new EnrollmentsViewModel(viewItem);
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
            return View(new EnrollmentsViewModel());
        }
        [HttpGet]
        public virtual async Task<IActionResult> EditAsync(Guid id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var (state, enrollment) = await _manager.GetByIdAsync(id);  
            return View(new EnrollmentsEditViewModel(enrollment));
        }

        [HttpPost]
        public virtual async Task<IActionResult> EditAsync(EnrollmentsEditViewModel enroll)
        {
            var dt = enroll.ToModel();

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
            return View(enroll);

        }

        [HttpGet]
        private async Task<List<SelectListItem>> GetStudentsSelectListAsync()
            => (await _Smanager.QueryAsync()).Select(o => new SelectListItem { Text = o.LastName, Value = o.Id.ToString() }).ToList();

        [HttpGet]
        private async Task<List<SelectListItem>> GetCoursesSelectListAsync()
            => (await _Cmanager.QueryAsync()).Select(o => new SelectListItem { Text = o.Title, Value = o.Id.ToString() }).ToList();

        [HttpGet]
        public virtual async Task<IActionResult> GetCoursesAsync()
        {
            var data = (await _Cmanager.QueryAsync()).Select(c => new CoursesViewModel(c)).ToList();
            return Json(data);
        }
        [HttpGet]
        public virtual async Task<IActionResult> GetStudentsAsync()
        {
            var data = (await _Smanager.QueryAsync()).Select(c => new StudentsViewModel(c)).ToList();
            return Json(data);
        }

    }
}
