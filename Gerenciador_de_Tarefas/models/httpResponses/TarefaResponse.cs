namespace Gerenciador_de_Tarefas.httpResponse
{

    public class TarefaResponse
    {
        public int Id { get; set; }

        public required string Titulo { get; set; }

        public string Descricao { get; set; }

        public required StatusTarefa Status { get; set; }

        public required DateTime DataCriacao { get; set; }

        public DateTime DataConclusao { get; set; }

    }

}