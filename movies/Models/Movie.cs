namespace movies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(250), Required]
        public string Title { get; set; }

        public int year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoreLine { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }
        public Genre Genre { get; set; }



    }
}
