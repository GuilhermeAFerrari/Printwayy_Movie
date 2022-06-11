using System.ComponentModel.DataAnnotations;

namespace PrintWayyMovie_API
{
    public class Sessao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data é um campo obrigatório")]
        public string Cl_Data { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hora de início é um campo obrigatório")]
        public string Cl_HoraInicio { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hora de término é um campo obrigatório")]
        public string Cl_HoraFim { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor é um campo obrigatório")]
        public double Cl_Valor { get; set; }

        [Required(ErrorMessage = "Animação é um campo obrigatório")]
        public string Cl_Animacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Áudio é um campo obrigatório")]
        public string Cl_Audio { get; set; } = string.Empty;

        public int Fk_IdFilme { get; set; }

        public int Fk_IdSala { get; set; }

        public string NomeFilme { get; set; } = string.Empty;

        public string NomeSala { get; set; } = string.Empty;
    }
}
