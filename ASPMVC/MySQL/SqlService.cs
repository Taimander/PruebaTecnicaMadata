using ASPMVC.DTO;
using ASPMVC.Models;
using MySql.Data.MySqlClient;

namespace ASPMVC.MySQL
{

    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            var results = new List<Dictionary<string, object>>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw ex;
            }

            return results;
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        // Execute the command and get the number of affected rows
                        int affectedRows = command.ExecuteNonQuery();

                        // If affectedRows > 0, it means the operation was successful
                        return affectedRows;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw ex;
            }
        }


        public Producto[] GetProducts()
        {
            var queryResults = ExecuteQuery("SELECT * FROM Producto");
            List<Producto> productos = new List<Producto>();

            foreach (var item in queryResults) {
                productos.Add(new Producto(
                    (int)item["id"],
                    (string)item["nombre"],
                    (string)item["descripcion"],
                    (decimal)item["precio"],
                    (string)item["categoria"]
                    ));
            }
            return productos.ToArray();
        }

        public Producto? GetProduct(int id)
        {
            var queryResults = ExecuteQuery("SELECT * FROM Producto WHERE id = @id LIMIT 1", new Dictionary<string, object> { { "@id", id } });
            if (queryResults.Count == 0) 
            {
                return null;
            }
            var result = queryResults.First();
            return new Producto(
                (int)result["id"],
                (string)result["nombre"],
                (string)result["descripcion"],
                (decimal)result["precio"],
                (string)result["categoria"]
                );
        }

        public bool CreateProduct(CreateProductoDto dto)
        {
            var queryResults = ExecuteNonQuery("INSERT INTO Producto(nombre, descripcion, precio, categoria) VALUES(@nombre, @descripcion, @precio, @categoria)",
                new Dictionary<string, object> { { "@nombre", dto.Nombre }, { "@descripcion", dto.Descripcion }, { "@precio", dto.Precio }, { "@categoria", dto.Categoria } }
                );
            
            return queryResults > 0;
        }

        public bool UpdateProduct(int id, CreateProductoDto dto)
        {
            var queryResults = ExecuteNonQuery("UPDATE Producto SET nombre = @nombre, descripcion = @descripcion, precio = @precio, categoria = @categoria WHERE id = @id",
                new Dictionary<string, object> { { "@id", id}, { "@nombre", dto.Nombre }, { "@descripcion", dto.Descripcion }, { "@precio", dto.Precio }, { "@categoria", dto.Categoria } }
                );
            return queryResults > 0;
        }

        public bool DeleteProduct(int id)
        {
            var queryResults = ExecuteNonQuery("DELETE FROM Producto WHERE id = @id", new Dictionary<string, object> { { "@id", id} });

            return queryResults > 0;
        }

        public bool CreateUser(UserModel model)
        {
            var queryResults = ExecuteNonQuery("INSERT INTO Usuario(usuario,hash_contrasena,salt_contrasena) VALUES(@usuario,@hash_contrasena,@salt_contrasena)",
                new Dictionary<string, object> { { "@usuario", model.Usuario}, { "@hash_contrasena", model.HashContrasena}, { "@salt_contrasena", model.SaltContrasena } }
                );
            return queryResults > 0;
        }
        
        public bool UserExists(string Username)
        {
            var queryResults = ExecuteQuery("SELECT COUNT(*) AS exist FROM Usuario WHERE usuario=@usuario",
                new Dictionary<string, object> { { "@usuario", Username} }
                );
            return (long)queryResults.First()["exist"] > 0;
        }

        public UserModel? GetUser(string Username)
        {
            var queryResults = ExecuteQuery("SELECT * FROM Usuario WHERE usuario=@usuario",
                new Dictionary<string, object> { { "@usuario", Username } }
                );
            if(queryResults.Count == 0)
            {
                return null;
            }
            return new UserModel(
                (int)queryResults.First()["id"],
                (string)queryResults.First()["usuario"],
                (string)queryResults.First()["hash_contrasena"],
                (string)queryResults.First()["salt_contrasena"]
                );
        }

    }
}

