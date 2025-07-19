using System.Data;
using TestDevCom.Enums;
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
                Category = Enum.Parse<Category>(reader["Category"].ToString()!),
                SubCategory = Enum.Parse<SubCategory>(reader["SubCategory"].ToString()!)
            };
        }
    }

}
