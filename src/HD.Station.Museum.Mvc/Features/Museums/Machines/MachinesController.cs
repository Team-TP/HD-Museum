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
    public class MachinesController : Controller
    {
        private IMachineManager _manager;

        public MachinesController(IMachineManager manager)
        {
            _manager = manager;

        }
        [HttpGet("[area]/[controller]")]
        public IActionResult Index(string filter, bool includeDisabled)
        {
            var model = new MachinesIndexViewModel
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
                var query = (await _manager.QueryAsync()).Select(a => new MachinesViewModel(a));
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
            var machines = new MachinesEditViewModel(null)

            {
                //Students = await GetStudentsSelectListAsync(),
                //Courses = await GetCoursesSelectListAsync()
            };
            return View(machines);
        }
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(MachinesEditViewModel model)
        {

            var enrollment = model.ToModel();
            var (state, edit) = await _manager.AddAsync(enrollment);
            return RedirectToAction("Details", new { id = edit.Id });
        }
        [HttpGet("[area]/[controller]/{id:guid}")]
        public virtual async Task<IActionResult> DetailsAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadMachineByIdAsync(id);
            var model = new MachinesViewModel(viewItem);
            return View(model);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadByIdAsync(id);
            var model = new MachinesViewModel(viewItem);
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
            return View(new MachinesViewModel());
        }
        [HttpGet]
        public virtual async Task<IActionResult> EditAsync(Guid id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var (state, machine) = await _manager.GetByIdAsync(id);
            return View(new MachinesEditViewModel(machine));
        }

        [HttpPost]
        public virtual async Task<IActionResult> EditAsync(MachinesEditViewModel machine)
        {
            var dt = machine.ToModel();

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
            return View(machine);

        }

        [HttpGet]
        public virtual async Task<IActionResult> GetChildMachinesAsync(Guid? id)
        {
            var data = (await _manager.QueryAsync()).Where(c => c.ParentId == id)
                .Select(c => new MachinesViewModel(c)).ToList();

            return Json(data);
        }

    }
}
