using OrganizMvc.Models.Enuns;

namespace OrganizMvc.Models
{
    public class ProjetoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ClasseEnum Materia { get; set; }
        public DateTime Prazo { get; set; }
        public DateTime Inicio { get; set; }
        public List<TemaViewModel> Temas { get; set; }
    }
}