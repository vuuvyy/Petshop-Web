using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petland_Shop.Models;
using System.Diagnostics;

using Petland_Shop.ModelViews;
using MimeKit;
using MailKit.Net.Smtp;

namespace Petland_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbMarketsContext _context;
        

        public HomeController(ILogger<HomeController> logger, DbMarketsContext context )
        {
            _logger = logger;
            _context = context;
            
        }


        public IActionResult Index()
        {
            HomeViewVM model = new HomeViewVM();

            var lsProducts = _context.Products.AsNoTracking()
                .Where(x => x.Active == true && x.HomeFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();

            var lsBestSeller = _context.Products.AsNoTracking()
               .Where(x => x.Active == true && x.HomeFlag == true && x.BestSellers == true)
               .OrderByDescending(x => x.DateCreated)
               .ToList();

            List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();
            var lsCats = _context.Categories
                .AsNoTracking()
                .Where(x => x.Published == true)
                .OrderByDescending(x => x.Ordering)
                .ToList();

           

            foreach (var item in lsCats)
            {
                ProductHomeVM productHome = new ProductHomeVM();
                productHome.category = item;
                productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatId).ToList();
                productHome.lsBestSeller = lsBestSeller.Where(x => x.CatId == item.CatId).ToList();
                lsProductViews.Add(productHome);

                var quangcao = _context.QuangCaos
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Active == true);

                var TinTuc = _context.TinDangs
                    .AsNoTracking()
                    .Where(x => x.Published == true && x.IsNewfeed == true)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .ToList();
                model.Products = lsProductViews;
                model.quangcao = quangcao;
                model.TinTucs = TinTuc;
                ViewBag.AllProducts = lsProducts;
            }
            return View(model);
        }

        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(model.Name, model.Email));
                message.To.Add(new MailboxAddress("Trung", "nvtrung19.work@gmail.com"));
                message.Subject = "New Contact Form PetlandShop";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = $"Name: {model.Name}\nEmail: {model.Email}\n\n{model.Note}";
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false); // SMTP server details
                    client.Authenticate("nvtrung19.work@gmail.com", "vhnnwsbmphzwcvrt"); // Your email login credentials

                    client.Send(message);
                    client.Disconnect(true);
                }

                return RedirectToAction("Index", "Home"); // Redirect to a thank you page or something similar
            }

            return View(model);
        }
        [Route("gioi-thieu.html", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}