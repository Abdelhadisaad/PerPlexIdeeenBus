using LanguageExt;
using Logic.DTO;
using Logic.Interfaces;
using Newtonsoft.Json;

namespace Logic.Services
{
    public class IdeaServices
    {
        private readonly IIdeaDal ideaDal;

        public IdeaServices(IIdeaDal ideaDal)
        {
            this.ideaDal = ideaDal;
        }

        public List<Idea> GetIdeas()
        {
            List<IdeaDTO> IdeasAsDTO =  ideaDal.GetIdeas();
            List<Idea> ideas         = ConvertDTOToIdeas(IdeasAsDTO);

            return ideas;
        }

        public Either<IdeaErrors, bool> InsertIdea(Idea idea)
        {
            return idea.IsValid().Match(
                succeed =>
                {
                    IdeaDTO ideaDTO = this.ConvertIdeaToDTO(idea);
                    ideaDal.InsertIdea(ideaDTO);
                    return Either<IdeaErrors, bool>.Right(true);
                },
                error => {
                    return (Either<IdeaErrors, bool>)error;
                } 
            );
        }

        private List<Idea> ConvertDTOToIdeas(List<IdeaDTO> ideaDTOs)
        {
            List<Idea> ideas = new List<Idea>();
            foreach (IdeaDTO ideaDTO in ideaDTOs)
            {
                List<Categories> categories = ConvertCategoriesJsonToList(ideaDTO.Categories);

                ideas.Add(
                    new Idea(ideaDTO.Id, ideaDTO.Onderwerp, ideaDTO.Beschrijving, ideaDTO.UserId, ideaDTO.UserName, ideaDTO.Type, ideaDTO.BeginDatum, ideaDTO.EindDatum, categories, ideaDTO.AanmaakDatum)
                    );
            }
            
            return ideas;
        }

        private IdeaDTO ConvertIdeaToDTO(Idea idea)
        {
            var dto          = new IdeaDTO();
            dto.Onderwerp    = idea.Onderwerp;
            dto.Beschrijving = idea.Beschrijving;
            dto.Type         = idea.Type;
            dto.UserId       = idea.UserId;
            dto.UserName     = idea.UserName;
            dto.BeginDatum   = idea.BeginDatum;
            dto.EindDatum    = idea.EindDatum;

            return dto;
        }

        private List<Categories> ConvertCategoriesJsonToList(string json)
        {
            var categoryStrings = JsonConvert.DeserializeObject<List<string>>(json);
            var categories = new List<Categories>();

            foreach (var categoryString in categoryStrings)
            {
                if (Enum.TryParse(categoryString, out Categories category))
                {
                    categories.Add(category);
                }
            }

            return categories;
        }
    }
}
