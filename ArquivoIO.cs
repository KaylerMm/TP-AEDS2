public class ArquivoIO
{
    public static void ProcessarArquivoEntrada(string caminhoArquivoEntrada, List<Course> listaDeCursos)
    {
        try
        {
            //Lê as linhas do arquivo e separa as informações
            var linhas = File.ReadAllLines(caminhoArquivoEntrada);
            var primeiraLinha = linhas[0].Split(' '); //A primeira linha (0) precisa de tratamento especial (n cursos e candidatos)

            int numCursos = int.Parse(primeiraLinha[0]);
            int numCandidatos = int.Parse(primeiraLinha[1]);

            //Duas listas pra cada classe
            var listaDeCandidatos = new List<Candidate>();

            //Cria cursos lidos e adiciona na lista
            for (int i = 1; i <= numCursos; i++)
            { //Começa em 1 para pular a primeira linha (a 0)
                var dadosCurso = linhas[i].Split(';');
                int codigoCurso = int.Parse(dadosCurso[0]);
                string nomeCurso = dadosCurso[1];
                int vagasDisponiveis = int.Parse(dadosCurso[2]);

                listaDeCursos.Add(new Course(codigoCurso, nomeCurso, vagasDisponiveis)); //Coloca todos os cursos numa lista de cursos
            }

            //Faz a mesma coisa de cima só que com candidatos
            for (int i = numCursos + 1; i < linhas.Length; i++)
            { // numCursos + 1 para pulas as informações dos cursos do arquivo
                var dadosCandidato = linhas[i].Split(';'); //Separa as informações em um vetor de dados
                //Atribui os dados pra variáveis
                string nomeCandidato = dadosCandidato[0];
                double notaRedacao = double.Parse(dadosCandidato[1]);
                double notaMatematica = double.Parse(dadosCandidato[2]);
                double notaLinguagens = double.Parse(dadosCandidato[3]);
                int codigoPrimeiraOpcao = int.Parse(dadosCandidato[4]);
                int codigoSegundaOpcao = int.Parse(dadosCandidato[5]);

                //Instancia novo candidato com os dados e adiciona em uma lista
                listaDeCandidatos.Add(new Candidate(nomeCandidato, notaRedacao, notaMatematica, notaLinguagens, codigoPrimeiraOpcao, codigoSegundaOpcao));
            }

            /*Percorre toda a lista de candidatos e cursos e adiciona os candidatos na lista de cadidatos daquele curso 
            caso o candidato tenha selecionado em suas opções aquele curso*/
            foreach (var candidato in listaDeCandidatos)
            {
                foreach (var curso in listaDeCursos)
                {
                    if (candidato.CodigoPrimeiraOpcao == curso.CodigoCurso || candidato.CodigoSegundaOpcao == curso.CodigoCurso)
                    {
                        curso.Candidatos.Add(candidato);
                    }
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar ler o arquivo: {ex.Message}");
        }
    }

    //Pega resultado da lista de selecionados e da fila flexível de espera e escreve o resultado no arquivo saida.txt
    public static void EscreverArquivoSaida(string outputFilePath, Dictionary<int, Course> dicionarioDeCursos)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            //Aqui estão sendo escritos os selecionados
            foreach (var curso in dicionarioDeCursos.Values)
            {
                writer.WriteLine("-------------");
                double mediaDoCurso = curso.Selecionados.Last().Media;  //Acessa a média do último aprovado na lista de aprovados do curso atual
                writer.WriteLine($"{curso.NomeCurso} {Math.Round(mediaDoCurso, 2)}");
                writer.WriteLine("\nSelecionados:");
                foreach (var candidato in curso.Selecionados)
                {
                    writer.WriteLine($"{candidato.Nome} {Math.Round(candidato.Media, 2)}");
                }

                //Aqui estão sendo escritos os da lista de espera
                writer.WriteLine("\nFila de Espera:");
                foreach (var candidato in curso.FilaDeEspera)
                {
                    writer.WriteLine($"{candidato.Nome} {Math.Round(candidato.Media, 2)}");
                }
            }
            writer.WriteLine("-------------");
        }
    }
}