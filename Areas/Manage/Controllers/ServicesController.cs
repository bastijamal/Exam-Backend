using Boocic.DAL;
using Boocic.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace Boocic.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class ServicesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ServicesController(AppDbContext dbContext,IWebHostEnvironment hostEnvironment )
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var services=_dbContext.services.ToList();
            return View(services);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Services services)
        {
            if (!ModelState.IsValid) return View();
            if (!services.ImgFile.ContentType.Contains("image/")) return View(services);

            string path = _hostEnvironment.WebRootPath + @"/upload/";

            string fileame = Guid.NewGuid().ToString() + services.ImgFile.FileName;

            using (FileStream stream = new FileStream(Path.Combine(path, fileame), FileMode.Create))
            {
                await services.ImgFile.CopyToAsync(stream);
            }

            services.PhotoUrl = fileame;
            await _dbContext.services.AddAsync(services);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var serv = _dbContext.services.FirstOrDefault(x => x.Id == id);
            _dbContext.services.Remove(serv);
            _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        public IActionResult Update(int id)
        {
            var service = _dbContext.services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return View();
            }
            return View(service);
        }
        [HttpPost]
        public IActionResult Update(Services newservice)
        {
            var oldservice = _dbContext.services.FirstOrDefault(x => x.Id == newservice.Id);
            if (oldservice == null)
            {
                return View();
            }
            if (newservice.ImgFile != null)
            {
                string path = _hostEnvironment.WebRootPath + @"\upload\";
                FileInfo fileInfo = new FileInfo(path + oldservice.PhotoUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                string filename = Guid.NewGuid() + newservice.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    newservice.ImgFile.CopyTo(stream);
                }
                oldservice.PhotoUrl = filename;
            }
            oldservice.Category = newservice.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        /// ELAVE:
        /// 





    }
}
