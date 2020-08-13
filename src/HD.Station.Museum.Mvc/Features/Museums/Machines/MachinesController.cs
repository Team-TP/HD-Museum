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
using System.Reflection.PortableExecutable;

namespace HD.Station.Feature.Mvc
{
    [Area("Museums")]
    public class MachinesController : Controller
    {
        private IMachineManager _manager;
        private IMachineProduceManager _pmanager;
        private IMachineWareHouseManager _whmanager;
        private ICategoryMachineManager _cmanager;


        public MachinesController(IMachineManager manager, ICategoryMachineManager cmanager)
        {
            _manager = manager;
            _cmanager = cmanager;

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
                var query = (await _manager.QueryAsync(filterText, includeDisabled)).Select(a => new MachinesViewModel(a));
                var result = await query.ToDataSourceResultAsync(request);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, ex.StackTrace });
            }
        }
        [HttpGet]
        public virtual async Task<IActionResult> CreateAsync(Guid? id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var machines = new MachinesEditViewModel(null);

            if(id == Guid.Empty)
            {
                machines.ParentId = null;
            }
            else
            {
                machines.ParentId = id;
            }    
            var model = new MachineCreateViewModel
            {
                Machine = machines,
                MachineProduce = new MachineProduceEditViewModel { },
                MachineWareHouse = new MachineWareHouseEditViewModel { }
            };
            return View(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> CreateAsync(MachineComponentsViewModel model)
        {
            //var machine = model.ToModel();
            var error = ModelState.Values.Select(a => a.Errors);
            var Mstate = ModelState.IsValid;
            if (Mstate)
            {
                var (state, edit) = await _manager.AddAsync(model);
                return Json(new { redirectToUrl = Url.Action("Index", "Machines") });
            }
            return View(model);
            
            //return RedirectToAction("Index");           
        }
        [HttpGet("[area]/[controller]/{id:guid}")]
        public virtual async Task<IActionResult> DetailsAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadMachineByIdAsync(id);
            //var machine = new MachinesViewModel(viewItem);
            var model = new MachinesViewModel(viewItem)
            {
                MachineProduce = new MachineProduceViewModel(viewItem.MachineProduces),
                MachineWareHouse = new MachineWareHouseViewModel(viewItem.MachineWarehouses)
            };
            return View(model);
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (state, viewItem) = await _manager.ReadMachineByIdAsync(id);
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

        public virtual async Task<IActionResult> EditAsync(Guid id, string layout = "_")
        {
            ViewData["Layout"] = layout == "_" ? "" : layout;
            var (state, machine) = await _manager.ReadMachineByIdAsync(id);

            //if (machine.CategoryMachines.Count() > 0) // Cách đọc CateIds thứ 2
            //{
            //    ViewBag.CateId = machine.CategoryMachines.First().CategoryId;
            //}
            //else
            //{
            //    ViewBag.CateId = null;
            //}

            if (state.Succeeded)
            {
                if(machine == null)
                {
                    return NotFound();
                }
                var machines = new MachinesEditViewModel(machine);
               
                var model = new MachineCreateViewModel()
                {
                    Machine = machines,
                    MachineProduce = new MachineProduceEditViewModel(machine.MachineProduces) { },
                    MachineWareHouse = new MachineWareHouseEditViewModel(machine.MachineWarehouses) { }
                };
                return View(model);
            }
            else
            {
                ViewBag.Notice = AlertModel.Error(state.ToString());
                return View();
            }

        }

        [HttpPost]
        public virtual async Task<IActionResult> EditAsync(MachineCreateViewModel viewItem)
        {

            var dt = viewItem.ToModel();
            var result = await _manager.UpdateAsync(dt);

            if (result.Succeeded)
            {
                ViewBag.Notice = new AlertModel
                {
                    Contextual = "success",
                    Message = "Da sua thanh cong",
                    Title = "Ok"
                };

                return Json(new { redirectToUrl = Url.Action("Index", "Machines") });
                //return RedirectToAction("Details", new { id = dt.Id });
            }
            ViewBag.Notice = result.ToAlert();
            return View(viewItem);

        }

    }
}
