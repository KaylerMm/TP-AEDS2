public class Course{
    public int CodigoCurso { get; set; }
    public string NomeCurso { get; set; }
    public int VagasDisponiveis { get; set; }
    public List<Candidate> Selecionados { get; }
    public List<Candidate> ListaDeEspera { get; }
    public List<Candidate> Candidatos { get; }

    public Course(int codigoCurso, string nomeCurso, int vagasDisponiveis){
        CodigoCurso = codigoCurso;
        NomeCurso = nomeCurso;
        VagasDisponiveis = vagasDisponiveis;
        Candidatos = new List<Candidate>(); //Lista com todos os candidatos àquele curso (não ordenada)
        Selecionados = new List<Candidate>(); //Lista dos candidatos selecionados àquele referido curso
        ListaDeEspera = new List<Candidate>(); //Lista dos candidatos na lista de espera daquele referido curso
    }

    //Recebe a lista já ordenada de candidatos daquele curso e os classifica de acordo com as vagas disponíveis
    public void ClassificarCandidatos(){
        foreach (var candidato in Candidatos){
            if(Selecionados.Count < VagasDisponiveis){
                Selecionados.Add(candidato);
        }
            else{
                ListaDeEspera.Add(candidato);
        }
        }
    }

    public void OrdenarCandidatos(){
        QuickSort(Candidatos, 0, Candidatos.Count - 1);
    }
    public static void QuickSort(List<Candidate> candidatos, int menor, int maior)
{
    if (menor < maior)
    {
        // p é o índice de partição, candidatos[p] está agora na posição correta
        int particao = Particao(candidatos, menor, maior);

        //Ordena os elementos antes e depois da partição
        QuickSort(candidatos, menor, particao - 1);
        QuickSort(candidatos, particao + 1, maior);
    }
}

private static int Particao(List<Candidate> candidatos, int menor, int maior)
{
    double pivo = candidatos[maior].Media; //Pivô
    int i = menor - 1; //Índice do menor elemento

    for (int j = menor; j < maior; j++)
    {
        //Se o elemento atual é maior ou igual ao pivô
        if (candidatos[j].Media >= pivo)
        {
            i++;

            //Troca candidatos[i] e candidatos[j]
            var temp = candidatos[i];
            candidatos[i] = candidatos[j];
            candidatos[j] = temp;
        }
    }

    //Troca candidatos[i + 1] e candidatos[maior] (ou pivô)
    var temp1 = candidatos[i + 1];
    candidatos[i + 1] = candidatos[maior];
    candidatos[maior] = temp1;

    return i + 1;
}

}