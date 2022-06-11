using System.ComponentModel.DataAnnotations;

namespace PrintWayyMovie_API
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "E-mail é um campo obrigatório")]
        public string Cl_Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        public string Cl_Senha { get; set; } = String.Empty;
    }
}
