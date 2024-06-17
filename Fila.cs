using System.Collections; //Para usar o iterator e fazer a classe funcionar com o foreach
public class Fila : IEnumerable<Candidate> //Necessário par implementar o iterador do objeto lá embaixo no código
{
    private Celula primeiro, ultimo;

    public Fila()
    {
        primeiro = new Celula(null); // Célula cabeça inicializada com null (ou pode usar um candidato fictício)
        ultimo = primeiro;
    }

    public void Add(Candidate candidato)
    {
        ultimo.Prox = new Celula(candidato);
        ultimo = ultimo.Prox;
    }

    public Candidate Remover()
    {
        if (primeiro == ultimo)
        {
            throw new Exception("Fila vazia!");
        }

        Celula tmp = primeiro;
        primeiro = primeiro.Prox;
        Candidate candidato = primeiro.Candidato;
        tmp.Prox = null;
        tmp = null;
        return candidato;
    }

    public void Mostrar()
    {
        Console.Write("[ ");
        for (Celula i = primeiro.Prox; i != null; i = i.Prox)
        {
            Console.Write(i.Candidato.Nome + " "); // Exemplo de como mostrar o nome do candidato
        }
        Console.WriteLine("]");
    }

    // Implementação do GetEnumerator para permitir iteração sobre a fila de objetos
    //O c# não consegue caminhar pela fila de objetos com foreach, assim precisamos usar do iterador pra  fazer acontecer
    public IEnumerator<Candidate> GetEnumerator()
    {
        // Percorre a fila começando da primeira célula depois da célula cabeça (primeiro.Prox)
        for (Celula i = primeiro.Prox; i != null; i = i.Prox)
        {
            // Utiliza yield return para retornar cada elemento (candidato) da célula atual
            yield return i.Candidato; //yield return retorna o primeiro elemento e suspende a execução até que o próximo elemento seja requisitado.
        }
    }

    // Implementação explícita do método GetEnumerator da interface IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        // Chama o método GetEnumerator que foi implementado acima
        return GetEnumerator();
    }

}
