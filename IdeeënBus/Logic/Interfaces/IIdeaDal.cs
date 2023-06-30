using Logic.DTO;

namespace Logic.Interfaces
{
    public interface IIdeaDal
    {
        void InsertIdea(IdeaDTO ideaDTO);
        List<IdeaDTO> GetIdeas();
    }
}
