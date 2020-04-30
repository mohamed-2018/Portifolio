using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortifolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortifolioItem> _porifolio;
        private readonly IHostingEnvironment _hosting;  // to add files to browser from computer 

        public PortifolioItemsController(IUnitOfWork<PortifolioItem> Porifolio,IHostingEnvironment hosting)
        {
            _porifolio = Porifolio;
            _hosting = hosting;
        }

        // GET: PortifolioItems
        public IActionResult Index()
        {
            return View(_porifolio.Entity.GetAll());
        }

        // GET: PortifolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portifolioItem = _porifolio.Entity.GetById(id);
            if (portifolioItem == null)
            {
                return NotFound();
            }

            return View(portifolioItem);
        }

        // GET: PortifolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortifolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortifolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File != null) 
                {
                    string uploads = Path.Combine(_hosting.WebRootPath,@"img\portfolio");
                    string FullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(FullPath,FileMode.Create));
                }
                PortifolioItem portifolioItem = new PortifolioItem
                { 
                  Projectname = model.Projectname,
                  Description = model.Description,
                  ImageUrl = model.File.FileName,

                };
                _porifolio.Entity.Insert(portifolioItem);
                _porifolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortifolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portifolioItem = _porifolio.Entity.GetById(id);
            if (portifolioItem == null)
            {
                return NotFound();
            }
            PortifolioViewModel portifolioViewModel = new PortifolioViewModel
            {
                Id = portifolioItem.Id,
                Description = portifolioItem.Description,
                Projectname = portifolioItem.Projectname,
                ImageUrl = portifolioItem.ImageUrl,
            };
            return View(portifolioViewModel);
        }

        // POST: PortifolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortifolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string FullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                    }
                    PortifolioItem portifolioItem = new PortifolioItem
                    {
                        Id = model.Id,
                        Projectname = model.Projectname,
                        Description = model.Description,
                        ImageUrl = model.File.FileName,

                    };
                    _porifolio.Entity.Update(portifolioItem);
                    _porifolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortifolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortifolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portifolioItem = _porifolio.Entity.GetById(id);
            if (portifolioItem == null)
            {
                return NotFound();
            }

            return View(portifolioItem);
        }

        // POST: PortifolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _porifolio.Entity.Delete(id);
            _porifolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortifolioItemExists(Guid id)
        {
            return _porifolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
