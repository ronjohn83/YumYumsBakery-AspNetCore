using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieRepository _pieRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //public IActionResult List()
        //{
        //    return View();
        //}

        public IActionResult List(string category)
        {
            //PiesListViewModel piesListViewModel = new PiesListViewModel();
            //piesListViewModel.Pies = _pieRepository.Pies;

            //return View(piesListViewModel);

            IEnumerable<Pie> pies;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.Pies.OrderBy(p => p.PieId);
                currentCategory = "All Pies";
            }
            else
            {
                pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }

            //PiesListViewModel piesViewModel = new PiesListViewModel
            //{
            //    Pies = pies,
            //    CurrentCategory = currentCategory
            //};


            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }
    }
}
