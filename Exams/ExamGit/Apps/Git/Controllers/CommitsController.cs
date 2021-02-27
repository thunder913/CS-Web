using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IRepositoryService repositoryService;
        private readonly ICommitService commitService;

        public CommitsController(IRepositoryService repositoryService, ICommitService commitService)
        {
            this.repositoryService = repositoryService;
            this.commitService = commitService;
        }
        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var commits = commitService.GetCommitsByUserId(GetUserId());
            return this.View(commits);
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var model = this.repositoryService.GetRepoCommitById(id);
            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (description.Length<5)
            {
                return this.Error("The description must be at least 5 characters long!");
            }
            commitService.AddCommit(description, id, GetUserId());
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            commitService.DeleteCommit(id);
            return this.Redirect("/Commits/All");
        }
    }
}
