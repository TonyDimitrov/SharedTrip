namespace SharedTrip.App.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            return this.Redirect("/Trips/All");          
        }

        [HttpGet("/Home/Index")]
        public HttpResponse HomeIndex()
        {
            return this.Index();
        }
    }
}