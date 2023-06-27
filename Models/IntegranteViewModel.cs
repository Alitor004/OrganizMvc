namespace OrganizMvc.Models
{
    public class IntegranteViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<TemaViewModel> Temas { get; set; }
    }
}