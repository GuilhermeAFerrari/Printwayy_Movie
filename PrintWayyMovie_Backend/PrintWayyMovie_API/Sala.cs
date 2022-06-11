using System.ComponentModel.DataAnnotations;

namespace PrintWayyMovie_API
{
    public class Sala
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório")]
        public int Cl_Quantidade { get; set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Cl_Nome { get; set; } = String.Empty;
    }
}
