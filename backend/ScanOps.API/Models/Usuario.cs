namespace ScanOps.API.Models
{
    public class Usuario
    {
        public string Nome { get; set; } = string.Empty;
        public string Registro { get; set; } = string.Empty; // Ex: 040123-01
        public string Senha { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
    }
}

/*Usei o seguinte modelo pra definir cargo dos usuarios, "040" pror padrão q nem o 060 da faculdade
e coloquei 3 numeros aleatorios pq hospital nao tem tanto funcionario, e 01 (Médico), 02 (Enfermeiro) ou 03 (Instrumentista).
    imagino que na av2 vamos ter nomes e registros ficticios de medicos no nosso banco de dados então farei apenas algo simples por hora,
na av2 eu faço um negocio pra verificar se o modelo da matricula está correto e verificar o medico pelo banco de dado, o codigo que colei no autocontrolerlogin.cs  
é usado apenas para testes, fiz pelo "gpt", nao verifiquei nem poli ainda só queria testar se tudo estava "conversando corretamente. */