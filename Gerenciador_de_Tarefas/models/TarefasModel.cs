using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class Tarefa
{

    [Key]
    [Column("id_tarefa")]
    public int Id { get; set; }

    [Required]
    [Column("titulo")]
    public string Titulo { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; }

    [Required]
    [Column("status")]
    public StatusTarefa Status { get; set; }

    [Required]
    [Column("data_criacao")]
    public DateTime DataCriacao { get; set; }

    [Column("data_conclusao")]
    public DateTime DataConclusao { get; set; }

}

public enum StatusTarefa
{
    Pendente,
    Concluida
}

