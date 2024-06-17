public class ArquivoIO{
    public static void LerArquivo(string caminhoArquivoEntrada, string caminhoArquivoSaida){
        try{
            //Lê as linhas do arquivo e separa as informações
            var linhas = File.ReadAllLines(caminhoArquivoEntrada);
            var primeiraLinha = linhas[0].Split(' '); //A primeira linha (0) precisa de tratamento especial (n cursos e candidatos)

            int numCursos = int.Parse(primeiraLinha[0]);
            int numCandidatos = int.Parse(primeiraLinha[1]);

            //Duas listas pra cada classe
            var listaDeCursos = new List<Course>();
            var listaDeCandidatos = new List<Candidate>();

            //Cria cursos lidos e adiciona na lista
            for (int i = 1; i <= numCursos; i++){ //Começa em 1 para pular a primeira linha (a 0)
                var dadosCurso = linhas[i].Split(';');
                int codigoCurso = int.Parse(dadosCurso[0]);
                string nomeCurso = dadosCurso[1];
                int vagasDisponiveis = int.Parse(dadosCurso[2]);

                listaDeCursos.Add(new Course(codigoCurso, nomeCurso, vagasDisponiveis)); //Coloca todos os cursos numa lista de cursos
            }

            //Faz a mesma coisa de cima só que com candidatos
            for (int i = numCursos + 1; i < linhas.Length; i++){ // numCursos + 1 para pulas as informações dos cursos do arquivo
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

            //Imprimindo dados para teste
            Console.WriteLine("Cursos lidos:");
            foreach (var curso in listaDeCursos){
                Console.WriteLine($"{curso.CodigoCurso}; {curso.NomeCurso}; {curso.VagasDisponiveis}");
            }
            Console.WriteLine("Candidados lidos:");
            foreach (var candidate in listaDeCandidatos){
                Console.WriteLine($"{candidate.Nome}; {candidate.NotaRedacao}; {candidate.NotaMatematica}; {candidate.NotaLinguagens}; {candidate.CodigoPrimeiraOpcao}; {candidate.CodigoSegundaOpcao}");
            }

            /*Percorre toda a lista de candidatos e cursos e adiciona os candidatos na lista de cadidatos daquele curso 
            caso o candidato tenha selecionado em suas opções aquele curso*/
            foreach (var candidato in listaDeCandidatos)
            {
                foreach (var curso in listaDeCursos)
                {
                    if(candidato.CodigoPrimeiraOpcao == curso.CodigoCurso || candidato.CodigoSegundaOpcao == curso.CodigoCurso){
                        curso.Candidatos.Add(candidato);
                    }
                }
                
            }

            //Percorre todos os cursos na lista de cursos lidos do arquivo
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
            EscreverArquivoSaida(caminhoArquivoSaida, dicionarioDeCursos);
        }

        catch (Exception ex){
            Console.WriteLine($"Erro ao tentar ler o arquivo: {ex.Message}");
        }
    }
     private static void EscreverArquivoSaida(string outputFilePath, Dictionary<int, Course> dicionarioDeCursos){
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {   
            //Aqui estão sendo escritos os selecionados
            foreach (var curso in dicionarioDeCursos.Values){
                writer.WriteLine("-------------");
                writer.WriteLine($"{curso.NomeCurso}");
                writer.WriteLine("\nSelecionados:");
                foreach (var candidato in curso.Selecionados){
                    writer.WriteLine($"{candidato.Nome} {Math.Round(candidato.Media, 2)}");
                }

                //Aqui estão sendo escritos os da lista de espera
                writer.WriteLine("\nFila de Espera:");
                foreach (var candidato in curso.FilaDeEspera){
                    writer.WriteLine($"{candidato.Nome} {Math.Round(candidato.Media, 2)}");
                }
            }
            writer.WriteLine("-------------");
        }
    }
}