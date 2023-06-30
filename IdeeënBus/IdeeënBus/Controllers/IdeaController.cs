using LanguageExt;
using Logic;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdeeënBus.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IdeaServices ideaServices;

        public IdeaController(IdeaServices services)
        {
            this.ideaServices = services;
        }

        // GET: IdeaController
        public ActionResult Index()
        {
            List<Idea> ideaList = ideaServices.GetIdeas();
            return View(ideaList);
        }

        // POST: IdeaController/Create
        [HttpPost]
        public Either<IdeaErrors, bool> Create([FromBody] Idea idea)
        {
            return ideaServices.InsertIdea(idea).Match(
                succeed => { return Either<IdeaErrors, bool>.Right(true); },
                error =>   { return Either<IdeaErrors, bool>.Left(error); }
            );
        }

    }
}
