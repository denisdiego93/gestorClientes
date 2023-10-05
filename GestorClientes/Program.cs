using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestorClientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1, Adiconar, Remover, Sair }

        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;
            while (!escolheuSair) { 
                Console.WriteLine("Seja bem vindo ao Sistema de clientes!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listar();
                        break;
                    case Menu.Adiconar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair |= true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de clientes!");
            Console.WriteLine("Digite o nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Digite o email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("Digite o cpf do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);

            Console.WriteLine("Cadastro de clientes concluido! Aperte ENTER para sair");
            Console.ReadLine();
        }

        static void Listar()
        {
            Console.WriteLine("Listagem de clientes!");
            
            if(clientes.Count > 0)
            {
                int id = 1;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine("ID: " + id);
                    Console.WriteLine("Nome: " + cliente.nome);
                    Console.WriteLine("Email: " + cliente.email);
                    Console.WriteLine("CPF: " + cliente.cpf);
                    Console.WriteLine("*****************************************");
                    id++;
                }
            }
            else
            {
                Console.WriteLine("Não há cadastro no sistema");
            }

            Console.WriteLine("Aperte ENTER para sair");
            Console.ReadLine();
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try 
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes=new List<Cliente>();
                }
            
            }catch (Exception ex) 
            { 
                clientes = new List<Cliente>();
            }
            stream.Close();
        }

        static void Remover ()
        {
            Listar();
            Console.WriteLine("Digite o ID do cliente a ser removido: ");
            int idRemove = int.Parse(Console.ReadLine());

            if(idRemove > 0 && idRemove < clientes.Count) {
                clientes.RemoveAt(idRemove);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id inválido, tente novamente");
            }
        }
    }
} 
