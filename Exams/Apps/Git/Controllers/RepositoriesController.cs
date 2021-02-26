using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private IRepositoryService repositoryService;

        public RepositoriesController(IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            var repos = repositoryService.GetRepositoryViewModels(GetUserId());
            return this.View(repos);
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (name.Length<3||name.Length>10)
            {
                return this.Error("The name must be between 3 and 10 characters long!");
            }
            repositoryService.AddRepository(name, repositoryType == "Public" ? true : false, GetUserId());
            
            return this.Redirect("/Repositories/All");
        }
    }
}
