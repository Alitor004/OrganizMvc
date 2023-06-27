namespace OrganizMvc.Models
{
    public class TemaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ProjetoViewModel Projeto { get; set; }
        public int ProjetoId { get; set; }
        public IntegranteViewModel Responsavel { get; set; }
        public int IntegranteId { get; set; }
    }
}