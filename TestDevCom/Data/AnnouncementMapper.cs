using System.Data;
using TestDevCom.Models;

namespace TestDevCom.Data
{
    public static class AnnouncementMapper
    {
        public static Announcement Map(IDataReader reader)
        {
            return new Announcement
            {
                Id = (int)reader["Id"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                CreatedDate = (DateTime)reader["CreatedDate"],
                Status = (bool)reader["Status"],
                Category = reader["Category"].ToString()!,
                SubCategory = reader["SubCategory"].ToString()!
            };
        }
    }
}
