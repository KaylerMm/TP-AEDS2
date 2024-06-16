namespace IO;

class Program{
    static void Main(string[] args){
        //Caminhos dos arquivos não tenho certeza como deixar pra submeter...
        string caminhoArquivoEntrada = "./test.txt";
        string caminhoArquivoSaida = "./saida.txt";
        
        ArquivoIO.LerArquivo(caminhoArquivoEntrada, caminhoArquivoSaida);
    }
}
