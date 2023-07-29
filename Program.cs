using System;
using MySql.Data.MySqlClient;

class Program
{
    static string connectionStr = "server=localhost;user=root;password=123456;database=biblioteca";
     static void Main()
    {
        while (true) //menu em looping até escolher a op 3
        {
            Console.WriteLine("1 - Adicionar Jogo");
            Console.WriteLine("2 - Visualizar jogo");
            Console.WriteLine("3 - Remover Jogo");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma das opções 1, 2, 3 ou 4: ");
            string choice = Console.ReadLine();
            switch (choice) //switch para ir em cada função 
            {
                case "1":
                    AddJogo();
                    break;
                case "2":
                    VisJogo();
                    break;
                case "3":
                    RemJogo();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Incorreto, digite apenas uma das opções: '1' '2' '3' ou '4' ");
                    break;
            }
        }
    }
    static void AddJogo()
    {
        Console.WriteLine("Digite o titulo ");
        string titulo = Console.ReadLine();

        Console.WriteLine("Digite a desenvolvedora ");
        string dev = Console.ReadLine();

        Console.WriteLine("Digite o criador ");
        string criador = Console.ReadLine();

        Console.WriteLine("Digite o genero ");
        string genero = Console.ReadLine();

        Console.WriteLine("Digite a preço ");
        string preço = Console.ReadLine();

        Console.WriteLine("Digite a data de lançamento (YYYY-MM-DD) ");
        string data_l = Console.ReadLine();

        //inserir os dados na tabela 
        using (MySqlConnection connection = new MySqlConnection(connectionStr))
        {
            connection.Open();

            string insertQuery = "INSERT INTO jogos (titulo, desenvolvedora, criador, genero, preço, data_l) VALUES (@titulo, @desenvolvedora, @criador, @genero, @preço, @data_l) ";
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@titulo", titulo);
                command.Parameters.AddWithValue("@desenvolvedora", dev);
                command.Parameters.AddWithValue("@criador", criador);
                command.Parameters.AddWithValue("@genero", genero);
                command.Parameters.AddWithValue("@preço", preço);
                command.Parameters.AddWithValue("@data_l", data_l);

                //para checar se o código rodou normalmente checa se a tabela teve algo mudado
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Book added successfully!");
                else
                    Console.WriteLine("Failed to add the book.");
            }
        }
    }
static void VisJogo()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionStr))
        {
            connection.Open();

            string selectQuery = "SELECT titulo, desenvolvedora, criador, genero, preço, data_l FROM jogos";
            using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Jogos na base de dados: ");
                    while (reader.Read())
                    {
                        string titulo = reader["titulo"].ToString();
                        string dev = reader["desenvolvedora"].ToString();
                        string criador = reader["criador"].ToString();
                        string genero = reader["genero"].ToString();
                        string preço = reader["preço"].ToString();
                        string data_l = reader["data_l"].ToString();
                        Console.WriteLine($"{titulo} criado por {criador} e publicado por {dev} - Jogo do genero: {genero} Custando R${preço} lançado em {data_l}");
                    }
                }
            }
        }
    }
static void RemJogo()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionStr))
        {
            connection.Open();
            Console.WriteLine("Digite o titulo que deseja excluir");
            string titulo = Console.ReadLine();
            string deleteQuery = "DELETE FROM jogos WHERE titulo=@titulo";
             using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
             {
                command.Parameters.AddWithValue("@titulo", titulo);
                int linhas_del = command.ExecuteNonQuery();
                if (linhas_del > 0)
                {
                    Console.WriteLine($"Jogo '{titulo}' removido com sucesso.");
                }
                else
                {
                    Console.WriteLine($"Jogo '{titulo}' não encontrado ou não foi removido.");
                }

             }
        }
    }
}

