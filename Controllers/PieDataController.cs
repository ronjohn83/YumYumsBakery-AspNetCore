using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDataController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public PieDataController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IEnumerable<PieViewModel> LoadMorePies()
        {
            IEnumerable<Pie> dbPies = null;
            var category = "Cheese cakes";

            if (string.IsNullOrEmpty(category))
                dbPies = _pieRepository.Pies.OrderBy(p => p.PieId).Take(10);
            else
                dbPies = _pieRepository.Pies.OrderBy(p => p.PieId)
                                            .Where(p => p.Category.CategoryName == category);

            List<PieViewModel> pies = new List<PieViewModel>();

            foreach (var pie in dbPies)
            {
                pies.Add(MapDbPieToPieViewModel(pie));
            }

            return pies;
        }

        private PieViewModel MapDbPieToPieViewModel(Pie dbPie)
        {
            return new PieViewModel()
            {
                PieId = dbPie.PieId,
                Name = dbPie.Name,
                Price = dbPie.Price,
                ShortDescription = dbPie.ShortDescription,
                ImageThumbnailUrl = dbPie.ImageThumbnailUrl
            };
        }
    }
}
