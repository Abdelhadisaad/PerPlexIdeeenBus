namespace Logic.DTO
{
    public class IdeaDTO
    {
        public int Id { get; set; }
        public string Onderwerp { get; set; }
        public string Beschrijving { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public string Categories { get; set; }
        public DateTime AanmaakDatum { get; set; }
    }
}
