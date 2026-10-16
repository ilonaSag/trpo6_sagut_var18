using Microsoft.AspNetCore.Mvc;
using sagut_packages_trpo6.Models;
using sagut_packages_trpo6.Service;

namespace sagut_packages_trpo6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private PackageService packageService = new PackageService();
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? recipient, [FromQuery] decimal? minWeight)
        {
            List<Package> packages = packageService.FindProduct();
            if (recipient!=null && recipient != "")
            {
                List<Package> filtered = new List<Package>();
                foreach(Package p in packages)
                {
                    if (p.Recipient == recipient)
                    {
                        filtered.Add(p);
                    }
                }
                if (filtered.Count == 0)
                {
                    return NotFound();
                }
                packages = filtered;

            }
            if (minWeight!=null)
            {
                if (minWeight <= 0)
                {
                    return BadRequest();
                }
                List<Package> filtered = new List<Package>();
                foreach (Package p in packages)
                {
                    if (p.Weight>=minWeight)
                    {
                        filtered.Add(p);
                    }
                }
                if (filtered.Count == 0)
                {
                    return NotFound();
                }
                packages = filtered;

            }
            return Ok(packages);

        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var package = packageService.FindProductId(id);
            if (package == null)
                return NotFound();
            return Ok(package);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Package package)
        {
            if (package == null)
                return BadRequest();
            if (package.TrackingNumber == null || package.TrackingNumber == "")
                return BadRequest();
            if (package.Recipient == null || package.Recipient == "")
                return BadRequest();
            if (package.Weight<=0)
                return BadRequest();
            if (package.Status == null || package.Status== "")
                return BadRequest();
            Package create = packageService.Create(package);
            return Ok(create);
        }
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Package package)
        {
            if (package == null)
                return BadRequest();
            if (package.TrackingNumber == null || package.TrackingNumber == "")
                return BadRequest();
            if (package.Recipient == null || package.Recipient == "")
                return BadRequest();
            if (package.Weight < 0)
                return BadRequest();
            if (package.Status == null || package.Status == "")
                return BadRequest();
            Package su = packageService.FindProductId(id);
            if (su == null)
                return NotFound();
            Package create = packageService.EditAll(id, package);
            return Ok(create);
        }
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] Package package)
        {
            Package update = packageService.Edit(id,package);
            if (update == null)
                return NotFound();
            return Ok(update);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            Package su = packageService.FindProductId(id);
            if (su == null)
                return NotFound();
            bool del = packageService.Delete(id);
            return Ok(del);
        }
    }
}
