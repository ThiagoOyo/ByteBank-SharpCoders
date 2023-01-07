using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        static void ManipularConta(List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            int validaçãoLogin = 1;
            int indexParaLogar = -1;

            Console.WriteLine("Login obrigatório!");

            while (validaçãoLogin == 1)
            {
                Console.WriteLine("Digite o seu CPF: ");
                string cpfLogin = Console.ReadLine();
                Console.WriteLine("Digite a sua senha: ");
                string senhaLogin = Console.ReadLine();
                indexParaLogar = cpfs.FindIndex(cpf => cpf == cpfLogin);

                if (indexParaLogar < 0)
                {
                    Console.WriteLine("CPF incorreto! Tente novamente!");
                    Console.WriteLine("-----------------");
                }
                else
                {
                    if (senhaLogin == senhas[indexParaLogar] && cpfLogin == cpfs[indexParaLogar])
                    {
                        Console.WriteLine("Acesso autorizado! Seja bem vindo!");
                        Console.WriteLine("-----------------");
                        validaçãoLogin = 0;
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta! Tente novamente!");
                        Console.WriteLine("-----------------");
                    }
                }
            }

            int escolhaUsuario = 0;

            while (escolhaUsuario != 4)
            {
                Console.WriteLine("1 - Saque");
                Console.WriteLine("2 - Depósito");
                Console.WriteLine("3 - Transferência");
                Console.WriteLine("4 - Logout");
                Console.WriteLine("Digite a opção desejada: ");
                escolhaUsuario = int.Parse(Console.ReadLine());
                Console.WriteLine("-----------------");

                switch (escolhaUsuario)
                {
                    case 1:
                        Console.WriteLine("Qual o valor que você deseja sacar? ");
                        double valorSaque = double.Parse(Console.ReadLine());

                        if (valorSaque <= saldos[indexParaLogar])
                        {
                            saldos[indexParaLogar] -= valorSaque;
                            Console.WriteLine($"Saque no valor de R$ {valorSaque} autorizado! ");
                            Console.WriteLine($"Saldo remanescente é de R$ {saldos[indexParaLogar]}!");
                            Console.WriteLine("-----------------");

                        }
                        else
                        {
                            Console.WriteLine($"Saldo de R${saldos[indexParaLogar]} insuficiente para realizar saque de R${valorSaque}. Tente novamente!");
                            Console.WriteLine("-----------------");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Qual o valor que você deseja depositar? ");
                        double valorDeposito = double.Parse(Console.ReadLine());
                        if (valorDeposito > 0)
                        {
                            saldos[indexParaLogar] += valorDeposito;
                            Console.WriteLine($"Depósito no valor de R${valorDeposito} realizado com sucesso!");
                            Console.WriteLine($"Saldo atual é de R$ {saldos[indexParaLogar]}!");
                            Console.WriteLine("-----------------");
                        }
                        else
                        {
                            Console.WriteLine("Valor informado inválido!");
                            Console.WriteLine("-----------------");
                        }

                        break;

                    case 3:
                        int validacaoCPFTransferencia = 1;
                        string cpfTransferencia = "";
                        int indexParaTransferir = -1;


                        while (validacaoCPFTransferencia == 1)
                        {
                            Console.WriteLine("Informe o CPF da pessoa que você deseja transferir: ");
                            cpfTransferencia = Console.ReadLine();
                            indexParaTransferir = cpfs.FindIndex(cpf => cpf == cpfTransferencia);

                            if (indexParaTransferir == -1)
                            {
                                Console.WriteLine("O CPF do usuário que você informou é inválido! Tente novamente!");
                                validacaoCPFTransferencia = 1;
                                Console.WriteLine("-----------------");
                            }
                            else
                            {
                                Console.WriteLine($"Usuário com CPF {cpfTransferencia} identificado para transferência! ");
                                validacaoCPFTransferencia = 0;
                            }
                        }

                        Console.WriteLine("Qual o valor que você deseja transferir? ");
                        double valorTransferencia = double.Parse(Console.ReadLine());

                        if (valorTransferencia > saldos[indexParaLogar])
                        {
                            Console.WriteLine($"Saldo de R${saldos[indexParaLogar]} insuficiente para realizar transferência de R${valorTransferencia}. Tente novamente!");
                            Console.WriteLine("-----------------");
                        }

                        if (valorTransferencia <= saldos[indexParaLogar] && validacaoCPFTransferencia == 0)
                        {
                            saldos[indexParaLogar] -= valorTransferencia;
                            saldos[indexParaTransferir] += valorTransferencia;

                            Console.WriteLine($"Transferência de R${valorTransferencia} para o usuário com cpf {cpfTransferencia} realizada com sucesso!");
                            Console.WriteLine($"Saldo remanescente é de R${saldos[indexParaLogar]}!");
                            Console.WriteLine("-----------------");
                        }



                        break;
                }
            }


        }

        public static void Main(string[] args)

        {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        ManipularConta(cpfs, senhas, saldos);
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);



        }

    }

}