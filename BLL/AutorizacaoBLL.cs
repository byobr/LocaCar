using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Util.Ferramentas;

namespace BLL
{
    public class AutorizacaoBLL : IAutorizacaoBLL
    {
        private readonly IConfiguration _configuration;
        private readonly IOperadoresRepositorio _operadoresRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;

        public AutorizacaoBLL(
            IConfiguration configuration,
            IOperadoresRepositorio operadoresRepositorio,
            IClientesRepositorio clientesRepositorio
            )
        {
            this._configuration = configuration;
            this._operadoresRepositorio = operadoresRepositorio;
            this._clientesRepositorio = clientesRepositorio;
        }
        public Tuple<string, DateTime> GerarToken(UsuarioDTO Usuario)
        {
            var Validade = DateTime.UtcNow.AddHours(8);
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            byte[] Key = Encoding.ASCII.GetBytes(_configuration.GetSection("Config").GetSection("secret_auth").Value);
            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Usuario.Nome.ToString()),
                    new Claim(ClaimTypes.Role, Usuario.Perfil.ToString())
                }),
                Expires = Validade,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);

            return new Tuple<string, DateTime>(TokenHandler.WriteToken(Token), Validade);
        }

        public TokenDTO Login(LoginDTO Usuario)
        {

            TokenDTO Retorno = new TokenDTO();

            if (Usuario.Operador)
            {
                int Matricula;
                try
                {
                    Matricula = Convert.ToInt32(Usuario.Login);
                }
                catch
                {
                    throw new Exception($"O login informado: {Usuario.Login} não é uma matrícula de operador válida.");
                }
                Operadores Operador = this._operadoresRepositorio.GetByLogin(Matricula);

                if (Operador == null)
                    throw new Exception($"O login informado: {Usuario.Login} não existe.");

                bool ValidaSenha = Operador.Senha.Equals(MD5Hash.GerarHashMd5(Usuario.Senha));

                if (!ValidaSenha)
                    throw new Exception($"A senha para login informado: {Usuario.Login} não corresponde.");

                Tuple<string, DateTime> Token = GerarToken(new UsuarioDTO { Nome = Operador.Nome, Perfil = "Operador" });

                Retorno.Usuario = Operador.Nome;
                Retorno.Perfil = "Operador";
                Retorno.Token = Token.Item1;
                Retorno.Expiracao = Token.Item2;
                Retorno.IdUsuario = Operador.Id;
            }
            else
            {
                if (!ValidaCPF.ValidarCPF(Usuario.Login))
                    throw new Exception($"O login informado: {Usuario.Login} não é um CPF de cliente válido.");

                Clientes Cliente = this._clientesRepositorio.GetByCpf(Usuario.Login);

                if (Cliente == null)
                    throw new Exception($"O cliente informado: {Usuario.Login} não existe.");

                bool ValidaSenha = Cliente.Senha.Equals(MD5Hash.GerarHashMd5(Usuario.Senha));

                if (!ValidaSenha)
                    throw new Exception($"A senha para o cliente informado: {Usuario.Login} não corresponde.");

                Tuple<string, DateTime> Token = GerarToken(new UsuarioDTO { Nome = Cliente.Nome, Perfil = "Cliente" });

                Retorno.Usuario = Cliente.Nome;
                Retorno.Perfil = "Cliente";
                Retorno.Token = Token.Item1;
                Retorno.Expiracao = Token.Item2;
                Retorno.IdUsuario = Cliente.Id;
            }

            return Retorno;
        }
    }
}
