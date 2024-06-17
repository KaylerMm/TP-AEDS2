public class Candidate{
    //Propriedades (properties) com getters e setters
    public string Nome { get; set; }
    public double NotaRedacao { get; set; }
    public double NotaMatematica { get; set; }
    public double NotaLinguagens { get; set; }
    public int CodigoPrimeiraOpcao { get; set; }
    public int CodigoSegundaOpcao { get; set; }
    public double Media { get; set; }

    //Construtor que recebe todos os dados e atribui os valores para o novo objeto
    public Candidate(string nome, double notaRedacao, double notaMatematica, double notaLinguagens, int codigoPrimeiraOpcao, int codigoSegundaOpcao){
        Nome = nome;
        NotaRedacao = notaRedacao;
        NotaMatematica = notaMatematica;
        NotaLinguagens = notaLinguagens;
        CodigoPrimeiraOpcao = codigoPrimeiraOpcao;
        CodigoSegundaOpcao = codigoSegundaOpcao;
        Media = CalcularMedia();
    }

    public Candidate(){
        //Vazio para que ele seja compatível com a classe célula que inicializa um candidado vazio (equivalente a null)
    }

    public double CalcularMedia(){
        return (NotaRedacao + NotaMatematica + NotaLinguagens) / 3;
    }
}