using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HD.Station.Museum.Demo.Feature.Museum.Folder
{
    public class DemoController : Controller
    {
        private ICourseManager _courseManager;
        public DemoController(ICourseManager courseManager)
        {
             _courseManager = courseManager;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var rs = (await _courseManager.QueryAsync()).Take(10).ToList();
            return Json(rs);
        }
    }
}
