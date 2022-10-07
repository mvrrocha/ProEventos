using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
    // [Table("EventosDetalhes")] // Nome da tabela que será criada no banco de dados
    public class Evento
    {                
        // [Key] é preciso declarar como Key, caso seja utilizado um nome de atributo diferente de "Id"
        // e na classe onde recebe a Foreign Key declarar [ForeignKey("EventosDetalhes")] no atributo
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        // [Required]
        // [MaxLength(50)]
        public string Tema { get; set; }
        // [NotMapped] não será criado no banco de dados
        public int QtdPessoas { get; set; }        
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}