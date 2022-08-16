using AgendaDeContatos.Core.Interfaces;
using Dapper;
using System.Data;

namespace AgendaDeContatos.Categorias.Models
{
    //Classe que realiza as operações com o banco de dados
    public class CategoriasDatabase : IDatabaseCategoria
    {
        private readonly IConnectionFactory _connectionFactory;

        public CategoriasDatabase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }        

        private const string Select_Categoria = "SELECT Id, Descricao FROM Categorias";
        private const string Insert_Categoria = "INSERT INTO Categorias (Descricao) VALUES (@Descricao); SELECT CAST (SCOPE_IDENTITY() as int);";
        private const string Update_Categoria = "UPDATE Categorias SET Descricao = @Descricao WHERE Id = @Id";
        private const string Delete_Categoria = "DELETE FROM Categorias WHERE Id = @Id";        
        private const string Obter_Identity = "SELECT IDENT_CURRENT('Categorias')";

        public List<Categoria> ObterLista()
        {
            using IDbConnection connection = _connectionFactory.ObterConexao();
            return connection.Query<Categoria>(Select_Categoria).ToList();                        
        }

        public List<Categoria> ObterLista(string filtro)
        {
            string sql = Select_Categoria + " WHERE Id = @id or Descricao like @descricao";
            int.TryParse(filtro, out int id);
            using IDbConnection connection = _connectionFactory.ObterConexao(); 
            return connection.Query<Categoria>(sql, new { id, descricao = $"%{filtro}%" }).ToList();            
        }

        public async Task InsertCategoria(Categoria categoria)
        {
            using IDbConnection connection = _connectionFactory.ObterConexao();
            int id = await connection.ExecuteScalarAsync<int>(Insert_Categoria, categoria);
            categoria.Id = id;
        }
        public async Task UpdateCategoria(Categoria categoria)
        {
            using IDbConnection connection = _connectionFactory.ObterConexao();
            await connection.ExecuteAsync(Update_Categoria, categoria);
        }

        public async Task DeleteCategoria(int idCategoria)
        {
            using IDbConnection connection = _connectionFactory.ObterConexao();
            await connection.ExecuteAsync(Delete_Categoria, new { Id = idCategoria });
        }  
        
        public int? ObterIdentidade()
        {
            using IDbConnection connection = _connectionFactory.ObterConexao();                        
            return connection.QueryFirstOrDefault<int>(Obter_Identity); 
        }
    }
}
