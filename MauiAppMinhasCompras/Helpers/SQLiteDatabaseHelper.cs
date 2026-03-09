using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // INSERIR PRODUTO
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // ATUALIZAR PRODUTO
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, " +
                         "Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // DELETAR PRODUTO POR ID
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // BUSCAR TODOS OS PRODUTOS
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // PESQUISAR PRODUTOS
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto " +
                         "WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

        // APAGAR TODOS OS PRODUTOS
        public Task<int> DeleteAll()
        {
            return _conn.DeleteAllAsync<Produto>();
        }

    } // Fecha classe SQLiteDatabaseHelper
} // Fecha namespace