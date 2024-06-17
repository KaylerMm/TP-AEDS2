namespace IO;

class Program
{
    static void Main(string[] args)
    {

        string caminhoArquivoEntrada = "./test.txt";
        string caminhoArquivoSaida = "./saida.txt";

        var listaDeCursos = new List<Course>(); //Lista pra armazenar os cursos com seus candidatos e resultados

        ArquivoIO.ProcessarArquivoEntrada(caminhoArquivoEntrada, listaDeCursos);

        //Percorre todos os cursos na lista de cursos lidos do arquivo para ordenar e classificar os seus candidatos
        foreach (var curso in listaDeCursos)
        {
            curso.OrdenarCandidatos(); //Ordena a classificação de candidatos
            curso.ClassificarCandidatos(); //Classifica os candidatos nos cursos (Selecionados e Lista de Espera)
        }

        var dicionarioDeCursos = listaDeCursos.ToDictionary(c => c.CodigoCurso); /*Cria dicionário a partir da lista. Para cada elemento da lista (chamado de c (lambda) a chave do dicionário será o seu código do curso (c.CodigoCurso))*/
        /*
            Chave (TKey): CodigoCurso de cada curso
            Valor (TValue): O próprio objeto Curso
        */

        //Recebe o dicionária criado a partir da lista anterior e escreve o arquivo saida.txt
        ArquivoIO.EscreverArquivoSaida(caminhoArquivoSaida, dicionarioDeCursos);


        //Mensagem para ser exibida no console confirmando o sucesso da operação
        System.Console.WriteLine("\a\nProcessamento realizado com sucesso!");
        System.Console.WriteLine("Arquivo saida.txt atualizado");

    }
}
