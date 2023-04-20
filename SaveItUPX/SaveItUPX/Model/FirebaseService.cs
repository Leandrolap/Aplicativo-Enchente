using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using SaveItUPX.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveItUPX.Model
{
    public class FirebaseService
    {
        FirebaseClient client;
        FirebaseStorage firebaseStorage = new FirebaseStorage("Seu link do banco de dados");

        Usuario users = new Usuario();

        public FirebaseService()
        {
            client = new FirebaseClient("conexão com o banco de dados");
        }

        public async Task<List<Usuario>> ObterInfoUser()
        {

            return (await client
              .Child("Users")
              .OnceAsync<Usuario>()).Select(item => new Usuario
              {
                  Nome = item.Object.Nome,
                  Email = item.Object.Email,
                  Telefone = item.Object.Telefone,
                  Senha = item.Object.Senha,
                  Id = item.Key,
                  CPF = item.Object.CPF,
                  Sexo = item.Object.Sexo,
                  Favorito = item.Object.Favorito,
                  Cidade = item.Object.Cidade,
                  NImagemPerfil = item.Object.NImagemPerfil,
                  Contato1 = item.Object.Contato1,
                  Contato2 = item.Object.Contato2,
                  Contato3 = item.Object.Contato3,
              }).ToList();
        }

        public async Task<List<Ocorrencia>> ObterInfoOcorrencia()
        {

            return (await client
              .Child("Chamado")
              .OnceAsync<Ocorrencia>()).Select(item => new Ocorrencia
              {
                  Id = item.Key,
                  Tipo = item.Object.Tipo,
                  Rota = item.Object.Rota,
                  Latitude = item.Object.Latitude,
                  Longitude = item.Object.Longitude,
                  Data = item.Object.Data,
                  UsuarioPost = item.Object.UsuarioPost
              }).ToList();
        }

        public async Task<bool> IsUserExists(string name)
        {
            var user = (await client.Child("Users")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.CPF == name)
                .FirstOrDefault();
            return (user != null);
        }

        public async Task<bool> RegisterUser(string name, string email, string cpf, string telefone, string sexo, string cidade, string senha)
        {
            if (await IsUserExists(cpf) == false)
            {
                await client.Child("Users")
                    .PostAsync(new Usuario()
                    {
                        Nome = name,
                        Email = email,
                        CPF = cpf,
                        Telefone = telefone,
                        Sexo = sexo,
                        Cidade = cidade,
                        Senha = senha
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RegisterOcorrencia(string tipo, string lat, string longi, string rotas, string data, string nome)
        {
            await client.Child("Chamado")
                .PostAsync(new Ocorrencia()
                {
                    Tipo = tipo,
                    Latitude = lat,
                    Longitude = longi,
                    Rota = rotas,
                    Data = data,
                    UsuarioPost = nome
                });
            return true;
        }
        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await client.Child("Users")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.Email == name)
                .Where(u => u.Object.Senha == passwd)
                .FirstOrDefault();
            return (user != null);
        }

        public async Task<Usuario> ObterUserReset(string email)
        {

            var allUsers = await ObterInfoUser();
            await client
            .Child("Users")
            .OnceAsync<Usuario>();
            return allUsers.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task<List<Ocorrencia>> ObterOcorrencia(string data)
        {

            var allOcorrencia = await ObterInfoOcorrencia();
            await client
            .Child("Chamado")
            .OnceAsync<Ocorrencia>();
            return allOcorrencia.Where(a => a.Data == data).ToList();
        }

        public async Task ResetSenha(string nome, string email, string cpf, string endereco, string telefone, string sexo, string favorito, string cidade, string Nsenha)
        {

            var toUpdateContato = (await client
              .Child("Users")
                .OnceAsync<Usuario>())
                   .Where(a => a.Object.Email == email).FirstOrDefault();

            await client
              .Child("Users")
                .Child(toUpdateContato.Key)
                  .PutAsync(new Usuario()
                  { Nome = nome, Email = email, CPF = cpf, Endereco = endereco, Telefone = telefone, Sexo = sexo, Favorito = favorito, Cidade = cidade, Senha = Nsenha });
        }

        public async Task AttUsuario(string nome, string email, string cpf, string endereco, string telefone, string sexo, string favorito, string cidade, string Nsenha, string NImagem)
        {

            var toUpdateContato = (await client
              .Child("Users")
                .OnceAsync<Usuario>())
                   .Where(a => a.Object.Email == email).FirstOrDefault();

            await client
              .Child("Users")
                .Child(toUpdateContato.Key)
                  .PutAsync(new Usuario()
                  { Nome = nome, Email = email, CPF = cpf, Endereco = endereco, Telefone = telefone, Sexo = sexo, Favorito = favorito, Cidade = cidade, Senha = Nsenha, NImagemPerfil = NImagem });
        }

        public async Task<string> StoreImages(Stream fileStream, string fileName)
        {
            var stroageImage = await firebaseStorage
                .Child("Perfil")
                .Child(fileName)
                .PutAsync(fileStream);

            return stroageImage;
        }

        public async Task<string> GetFile(string fileName)
        {
            try
            {
                return await firebaseStorage
                    .Child("Perfil")
                    .Child(fileName)
                    .GetDownloadUrlAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            try
            {
                var imageUrl = await firebaseStorage
                    .Child("Perfil")
                    .Child(fileName)
                    .PutAsync(fileStream);

                DBSAveIt.NomeImagem = fileName;
                return imageUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUser(string email)
        {
            var toDeleteContato = (await client
               .Child("Users")
               .OnceAsync<Usuario>())
                 .Where(a => a.Object.Email == email).FirstOrDefault();
            await client.Child("Users")
                    .Child(toDeleteContato.Key)
                        .DeleteAsync();
        }

        public async Task DeleteFile(string fileName)
        {
            try
            {
                await firebaseStorage
                     .Child("Perfil")
                     .Child(fileName)
                     .DeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
