using Microsoft.AspNetCore.Mvc;
using ScanOps.API.Models;
using System.Collections.Generic;
using System.Linq;
//CODIGO DE GPT USADO APENAS PARA TESTES, LOGO SERÁ SUBSTITUIDO POR UM CÓDIGO MAIS ROBUSTO, COM BANCO DE DADOS E VERIFICAÇÃO DE REGISTRO DE MÉDICOS.
namespace ScanOps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Lista estática para simular o banco de dados no trabalho da Unifeso
        // Assim não perde os dados enquanto a API estiver rodando
        private static List<Usuario> tabelaUsuarios = new List<Usuario>();

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario novoUser)
        {
            string reg = novoUser.Registro;

            if (string.IsNullOrEmpty(reg) || string.IsNullOrEmpty(novoUser.Senha))
            {
                return BadRequest("O registro e a senha não podem ficar em branco.");
            }

            // Regra 1: O tamanho total tem que ser 9 (ex: 040123-01)
            if (reg.Length != 9)
            {
                return BadRequest("Formato incorreto. O registro precisa ter 9 caracteres no total.");
            }

            // Regra 2: Tem que começar com 040
            if (!reg.StartsWith("040"))
            {
                return BadRequest("Erro: Todos os registros devem começar com 040.");
            }

            // Regra 3: O caractere na posição 6 (o sétimo caractere) tem que ser um traço
            if (reg[6] != '-')
            {
                return BadRequest("Falta o traço antes de definir o cargo.");
            }

            // Regra 4: Descobrir o cargo pegando os dois últimos dígitos depois do traço
            string codCargo = reg.Substring(7, 2);
            string nomeCargo = "";

            if (codCargo == "01")
            {
                nomeCargo = "Médico";
            }
            else if (codCargo == "02")
            {
                nomeCargo = "Enfermeiro";
            }
            else if (codCargo == "03")
            {
                nomeCargo = "Instrumentista";
            }
            else
            {
                return BadRequest("Final do registro inválido. Use 01 (Médico), 02 (Enfermeiro) ou 03 (Instrumentista).");
            }

            // Regra 5: Impedir cadastrar o mesmo registro duas vezes
            bool jaExiste = tabelaUsuarios.Any(x => x.Registro == reg);
            if (jaExiste)
            {
                return BadRequest("Já existe alguém cadastrado com esse registro no sistema.");
            }

            // Se passou por todos os ifs, salva na nossa lista
            novoUser.Cargo = nomeCargo;
            tabelaUsuarios.Add(novoUser);

            return Ok($"Sucesso! {novoUser.Nome} foi cadastrado como {nomeCargo}.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginData)
        {
            // O Arthur ou o front-end manda pra cá e a gente confere na lista
            var usuarioEncontrado = tabelaUsuarios.FirstOrDefault(
                x => x.Registro == loginData.Registro && x.Senha == loginData.Senha
            );

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Registro ou senha incorretos, tente novamente.");
            }

            return Ok(new { 
                mensagem = "Login liberado!", 
                nome = usuarioEncontrado.Nome, 
                cargo = usuarioEncontrado.Cargo 
            });
        }
    }
}