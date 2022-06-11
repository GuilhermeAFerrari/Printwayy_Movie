using System.ComponentModel.DataAnnotations;

namespace PrintWayyMovie_API
{
    public class Filme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Foto é um campo obrigatório")]
        public string Cl_Imagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "Título é um campo obrigatório")]
        public string Cl_Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição é um campo obrigatório")]
        public string Cl_Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duração é um campo obrigatório")]
        public string Cl_Duracao { get; set; } = string.Empty;
    }
}
