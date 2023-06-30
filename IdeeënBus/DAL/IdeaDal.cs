using Logic.DTO;
using Logic.Interfaces;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class IdeaDal : IIdeaDal
    {
        private Connection configCon;
        private readonly MySqlConnection conn;
        public IdeaDal()
        {
            configCon = new Connection();
            conn = new MySqlConnection(configCon.SqlConnectionString);
        }

        public void InsertIdea(IdeaDTO ideaDTO)
        {
            string ideaEntryQuery = "INSERT INTO `Ideas`( `onderwerp`, `beschrijving`, `userId`, `userName`, `type`, `beginDatum`, `eindDatum`, `categories`, `aanmaakDatum`)" +
                " VALUES (@onderwerp, @beschrijving, @userId, @userName, @type, @beginDatum, @eindDatum, @categories, @aanmaakDatum)";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(ideaEntryQuery, conn);
            cmd.Parameters.AddWithValue("@userId", ideaDTO.Onderwerp);
            cmd.Parameters.AddWithValue("@userName", ideaDTO.Beschrijving);
            cmd.Parameters.AddWithValue("@subject", ideaDTO.UserId);
            cmd.Parameters.AddWithValue("@description", ideaDTO.UserName);
            cmd.Parameters.AddWithValue("@type", ideaDTO.Type);
            cmd.Parameters.AddWithValue("@startDate", ideaDTO.BeginDatum);
            cmd.Parameters.AddWithValue("@endDate", ideaDTO.EindDatum);
            cmd.Parameters.AddWithValue("@categories", ideaDTO.Categories);
            cmd.Parameters.AddWithValue("@aanmaakDatum", DateTime.Now);
            cmd.ExecuteReader();
            conn.Close();
        }

        public List<IdeaDTO> GetIdeas()
        {
            string applyOnJobQuery = "SELECT * FROM Ideas ORDER BY aanmaakDatum";
            var ideas = new List<IdeaDTO>();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(applyOnJobQuery, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    ideas.Add(new IdeaDTO()
                    {
                        Id           = dataReader.GetInt32("id"),
                        Onderwerp    = dataReader.GetString("onderwerp"),
                        Beschrijving = dataReader.GetString("beschrijving"),
                        UserId       = dataReader.GetInt32("userId"),
                        UserName     = dataReader.GetString("userName"),
                        Type         = dataReader.GetString("type"),
                        BeginDatum   = dataReader.GetDateTime("beginDatum"),
                        EindDatum    = dataReader.GetDateTime("eindDatum"),
                        Categories   = dataReader.GetString("categories"),
                        AanmaakDatum = dataReader.GetDateTime("aanmaakDatum"),
                    });
                }
            }
            return ideas;
        }

    }
}
