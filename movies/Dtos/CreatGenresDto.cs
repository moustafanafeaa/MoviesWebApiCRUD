namespace movies.Dtos
//dto to transfer data to/from api

{
    public class CreatGenresDto
    {
        [MaxLength(100),Required]
        
        public string Name { get; set; }
    }
}
