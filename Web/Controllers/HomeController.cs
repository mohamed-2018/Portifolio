using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortifolioItem> _porifolio;
        public HomeController(IUnitOfWork<Owner> Owner, IUnitOfWork<PortifolioItem> Porifolio)
        {
            _owner = Owner;
            _porifolio = Porifolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                PortifolioItems = _porifolio.Entity.GetAll().ToList(),

            };
            return View(homeViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}