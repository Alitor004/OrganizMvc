namespace OrganizMvc.Models
{
    public class IntegranteViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<TemaViewModel> Temas { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}