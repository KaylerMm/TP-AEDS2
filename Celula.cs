public class Celula
{
    private Candidate candidato;
    private Celula prox;

    public Celula(Candidate candidato)
    {
        this.candidato = candidato;
        this.prox = null;
    }

    public Celula Prox
    {
        get { return prox; }
        set { prox = value; }
    }

    public Candidate Candidato
    {
        get { return candidato; }
        set { candidato = value; }
    }
}
