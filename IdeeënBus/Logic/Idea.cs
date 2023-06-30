using LanguageExt;

namespace Logic
{
    public class Idea
    {
        public int Id { get; set; }
        public string Onderwerp { get; set; }
        public string Beschrijving { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public List<Categories> Categories { get; set; }
        public DateTime AanmaakDatum { get; set; }


        public Idea(int id ,string onderwerp, string beschrijving, int userId, string userName, string type, DateTime beginDatum, DateTime eindDatum, List<Categories> categories, DateTime aanmaakDatum)
        {
            Id           = id;
            Onderwerp    = onderwerp;
            Beschrijving = beschrijving;
            UserId       = userId;
            UserName     = userName;
            Type         = type;
            BeginDatum   = beginDatum;
            EindDatum    = eindDatum;
            Categories   = categories;
            AanmaakDatum = aanmaakDatum;
        }

        public Either<IdeaErrors, bool> IsValid()
        {
            if (string.IsNullOrEmpty(Onderwerp))
            {
                return Either<IdeaErrors, bool>.Left(IdeaErrors.OnderwerpOntbreekt);
            }

            if (Onderwerp.Length > 512)
            {
                return Either<IdeaErrors, bool>.Left(IdeaErrors.OnderwerpTeLang);
            }

            if (string.IsNullOrEmpty(Beschrijving))
            {
                return (Either<IdeaErrors, bool>)IdeaErrors.BeschrijvingOntbreekt;
            }

            if (string.IsNullOrEmpty(Type))
            {
                return Either<IdeaErrors, bool>.Left(IdeaErrors.TypeOntbreekt);
            }

            if (Type == "Uitje")
            {
                if (BeginDatum == DateTime.MinValue)
                {
                    return Either<IdeaErrors, bool>.Left(IdeaErrors.BeginDatumOntbreekt);
                }

                if (EindDatum == DateTime.MinValue)
                {
                    return Either<IdeaErrors, bool>.Left(IdeaErrors.EindDatumOntbreekt);
                }
            }

            if (UserName.Length > 512)
            {
                return Either<IdeaErrors, bool>.Left(IdeaErrors.UserNameTeLang);
            }

            return Either<IdeaErrors, bool>.Right(true);
        }

    }

    public enum IdeaErrors
    {
        OnderwerpOntbreekt,
        BeschrijvingOntbreekt,
        TypeOntbreekt,
        BeginDatumOntbreekt,
        EindDatumOntbreekt,
        UserNameTeLang,
        CategorieTeLang,
        OnderwerpTeLang
    }

    public enum Categories
    {
        Lunch,
        Borrel,
        Sportief,
        Intern,
        Fun
    }

}
