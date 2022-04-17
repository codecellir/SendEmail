using Microsoft.AspNetCore.Mvc;
using SendEmail.Infrastructure;

namespace SendEmail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;
        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            await _emailService.SendEmailAsync("destination-email@gmail.com", "visit codecell", "visit codecell on codecell.ir");
            return View();
        }
    }
}