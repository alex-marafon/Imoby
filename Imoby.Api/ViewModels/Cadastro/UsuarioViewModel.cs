using Imoby.Api.Token;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Imoby.Entities.Entitie.Models;
public class UsuarioViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }




    #region Teste Class com construtor validando parametro

    //private string Id { get; set; }
    //private string Name { get; set; }
    //private string Email { get; set; }
    //private string Senha
    //{
    //    get; set;
    //    public UsuarioViewModel(string name, string email, string senha)
    //{

    //    if (string.IsNullOrEmpty(name))
    //        throw new Exception("Nome deve ser informado.");

    //    if (string.IsNullOrEmpty(email))
    //        throw new Exception("Email deve ser informado.");

    //    if (string.IsNullOrEmpty(senha))
    //        throw new Exception("Senha deve ser informado.");


    //    Name = name;
    //    Email = email;
    //    Senha = senha;
    //}

    #endregion


    public TokenJWT CreateToken( UsuarioViewModel item)
    {
        return token(item);
    }


    private static TokenJWT token(UsuarioViewModel item)
    {
        var token = new TokenJWTBuilder()
            //lambari%3D&OlhandoçÇÇçde#baixo#para$cima@ 
            .AddSecurityKey(JwtSecurityKey.Create("bGFtYmFyaSUzRCZPbGhhbmRvw6fDh8OHw6dkZSNiYWl4byNwYXJhJGNpbWFAIA"))
            .AddSubject("Empresa - Save Code")
            .AddIssuer("Teste.Securiry.Bearer")
            .AddAudience("Teste.Securiry.Bearer")
            .AddClaim("idUsuario", item.Id)
            .AddExpiry(5)
            .Builder();

        return token;
    }

    #region Arquivo File Gravar e Apagar

    //public void UploadDocumento()
    //{
    //    var server = new DadoServer();
    //    var pasta = Path.Combine("documentos", this.Id);
    //    var upload = new Upload(pasta);
    //    foreach (var doc in Documentos)
    //    {
    //        if (doc.Arquivo.Contains("data:"))
    //        {
    //            upload.Salvar(doc.Arquivo, String.Format("documentos/{0}/", this.Id));
    //            doc.Arquivo = upload.EnderecoArquivo;
    //            doc.Aprovado = false;
    //            doc.Data = DateTime.Now;
    //            this.AprovarDocumentos = true;
    //        }
    //    }
    //    RemoverArquivo();
    //}

    //public void RemoverArquivo()
    //{
    //    var pasta = Path.Combine("documentos", this.Id);
    //    var diretorio = Path.Combine("Uploads", pasta);
    //    if (Directory.Exists(diretorio))
    //    {
    //        string[] dir = Directory.GetFiles(diretorio);
    //        foreach (var arquivo in dir)
    //        {
    //            var nome = new FileInfo(arquivo).Name;
    //            if (!this.Documentos.Exists(x => x.Arquivo.Contains(nome)))
    //            {
    //                File.Delete(arquivo);
    //            }
    //        }
    //    }
    //}

    #endregion

    public string CriptografaMd5(string senha)
    {
        try
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool ValidaPoliticaSenha(string senha)
    {
        if (senha == null)
            return false;
        if (senha.Length < 6)
            return false;
        if (senha.Length > 16)
            return false;

        return true;
        // bool caracter = Regex.IsMatch(senha, @"[!""#$%&'()*+,-./:;?@[\\\]_`{|}~]");
        //// var caracter = Regex.IsMatch(senha, @"^[a-z0-9\\.@]+$"); // 
        // if (caracter)
        // {
        //     if (senha.ToLower() == senha)
        //         caracter = false;
        // }
        // return caracter;
    }

    public bool ValidaPoliticaLogin(string login)
    {
        return Regex.IsMatch(login, @"^[a-z0-9\\.@]+$");
    }


}
