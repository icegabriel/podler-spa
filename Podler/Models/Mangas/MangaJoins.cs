using System.Runtime.Serialization;

namespace Podler.Models.Mangas
{

    [DataContract]
    public class MangaChapter : ModelBase
    {
        [DataMember]
        public int MangaId { get; set; }
        [DataMember]
        public int ChapterId { get; set; }

        [DataMember]
        public Manga Manga { get; set; }
        [DataMember]
        public Chapter Chapter { get; set; }
    }

    [DataContract]
    public class MangaGenre : ModelBase
    {
        [DataMember]
        public int MangaId { get; set; }
        [DataMember]
        public int GenreId { get; set; }

        [DataMember]
        public Manga Manga { get; set; }
        [DataMember]
        public Genre Genre { get; set; }
    }

    [DataContract]
    public class MangaTheme : ModelBase
    {
        [DataMember]
        public int MangaId { get; set; }
        [DataMember]
        public int ThemeId { get; set; }

        [DataMember]
        public Manga Manga { get; set; }
        [DataMember]
        public Theme Theme { get; set; }
    }

    [DataContract]
    public class MangaStaff : ModelBase
    {
        [DataMember]
        public int MangaId { get; set; }
        [DataMember]
        public int StaffId { get; set; }

        [DataMember]
        public Manga Manga { get; set; }
        [DataMember]
        public Staff Staff { get; set; }
    }
}
