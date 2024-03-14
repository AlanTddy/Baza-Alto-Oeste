using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class Usuario
{
    public int Id { get; private set; }
    public string NomeCompleto { get; set; }
    public int Idade { get; set; }
    public string CPF { get; set; }
    public string Senha { get; set; }
    public Endereco Endereco { get; set; }
    public Contato Contato { get; set; }

    public Usuario(int id, string nomeCompleto, int idade, string cpf, string senha, Endereco endereco, Contato contato)
    {
        Contract.Requires(id > 0, "ID do usuário deve ser maior que zero.");
        Contract.Requires(!string.IsNullOrEmpty(nomeCompleto), "Nome completo do usuário não pode ser nulo ou vazio.");
        Contract.Requires(!string.IsNullOrEmpty(cpf), "CPF do usuário não pode ser nulo ou vazio.");
        Contract.Requires(!string.IsNullOrEmpty(senha), "Senha do usuário não pode ser nula ou vazia.");

        Id = id;
        NomeCompleto = nomeCompleto;
        Idade = idade;
        CPF = cpf;
        Senha = senha;
        Endereco = endereco;
        Contato = contato;
    }
}

public class Endereco
{
    public int Id { get; private set; }
    public string Cidade { get; set; }
    public string CEP { get; set; }
    public string Rua { get; set; }
    public string NumeroResidencia { get; set; }

    public Endereco(int id, string cidade, string cep, string rua, string numeroResidencia)
    {
        Contract.Requires(id > 0, "ID do endereço deve ser maior que zero.");
        Contract.Requires(!string.IsNullOrEmpty(cidade), "Cidade não pode ser nula ou vazia.");
        Contract.Requires(!string.IsNullOrEmpty(cep), "CEP não pode ser nulo ou vazio.");
        Contract.Requires(!string.IsNullOrEmpty(rua), "Rua não pode ser nula ou vazia.");
        Contract.Requires(!string.IsNullOrEmpty(numeroResidencia), "Número da residência não pode ser nulo ou vazio.");

        Id = id;
        Cidade = cidade;
        CEP = cep;
        Rua = rua;
        NumeroResidencia = numeroResidencia;
    }
}

public class Contato
{
    public int Id { get; private set; }
    public string Telefone { get; set; }
    public string Email { get; set; }

    public Contato(int id, string telefone, string email)
    {
        Contract.Requires(id > 0, "ID do contato deve ser maior que zero.");
        Contract.Requires(!string.IsNullOrEmpty(telefone), "Telefone não pode ser nulo ou vazio.");
        Contract.Requires(!string.IsNullOrEmpty(email), "E-mail não pode ser nulo ou vazio.");

        Id = id;
        Telefone = telefone;
        Email = email;
    }
}

public class Produto
{
    public int Id { get; private set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public List<string> Comentarios { get; set; }

    public Produto(int id, string descricao, double preco)
    {
        Contract.Requires(id > 0, "ID do produto deve ser maior que zero.");
        Contract.Requires(!string.IsNullOrEmpty(descricao), "Descrição do produto não pode ser nula ou vazia.");
        Contract.Requires(preco > 0, "Preço do produto deve ser maior que zero.");

        Id = id;
        Descricao = descricao;
        Preco = preco;
        Comentarios = new List<string>();
    }
}

public class Sistema
{
    private List<Usuario> usuarios;
    private List<Produto> produtos;

    public Sistema()
    {
        usuarios = new List<Usuario>();
        produtos = new List<Produto>();
    }

    public void CadastrarUsuario(Usuario usuario)
    {
        Contract.Requires(usuario != null, "Usuário não pode ser nulo.");
        Contract.Requires(!string.IsNullOrEmpty(usuario.CPF), "CPF do usuário não pode ser nulo ou vazio.");
        Contract.Requires(!UsuariosContemCPF(usuario.CPF), "Usuário com o mesmo CPF já cadastrado.");

        usuarios.Add(usuario);
        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

    public void AdicionarProduto(Produto produto)
    {
        Contract.Requires(produto != null, "Produto não pode ser nulo.");
        produtos.Add(produto);
        Console.WriteLine("Produto adicionado com sucesso!");
    }

    public void VisualizarProdutos()
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Não há produtos disponíveis.");
        }
        else
        {
            Console.WriteLine("Produtos disponíveis:");
            foreach (Produto produto in produtos)
            {
                Console.WriteLine($"ID: {produto.Id}, Descrição: {produto.Descricao}, Preço: {produto.Preco}");
            }
        }
    }

    public void RemoverProduto(int id)
    {
        Contract.Requires(id > 0, "ID do produto deve ser maior que zero.");
        Contract.Requires(ProdutosContemId(id), "Produto com o ID especificado não encontrado.");
        Produto produto = produtos.Find(p => p.Id == id);
        produtos.Remove(produto);
        Console.WriteLine("Produto removido com sucesso!");
    }

    private bool ProdutosContemId(int id)
    {
        return produtos.Exists(p => p.Id == id);
    }

    private bool UsuariosContemCPF(string cpf)
    {
        return usuarios.Exists(u => u.CPF == cpf);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exemplo de uso do sistema
        Sistema sistema = new Sistema();

        Usuario usuario1 = new Usuario(1, "Fulano de Tal", 30, "12345678900", "senha123",
                                        new Endereco(1, "Cidade A", "12345-678", "Rua Principal", "123"),
                                        new Contato(1, "123456789", "fulano@example.com"));

        Usuario usuario2 = new Usuario(2, "Ciclano de Tal", 25, "98765432100", "outrasenha",
                                        new Endereco(2, "Cidade B", "98765-432", "Avenida Secundária", "456"),
                                        new Contato(2, "987654321", "ciclano@example.com"));

        Produto produto1 = new Produto(1, "Produto A", 99.99);
        Produto produto2 = new Produto(2, "Produto B", 49.99);

        sistema.CadastrarUsuario(usuario1);
        sistema.CadastrarUsuario(usuario2);
        sistema.AdicionarProduto(produto1);
        sistema.AdicionarProduto(produto2);

        sistema.VisualizarProdutos();
        sistema.RemoverProduto(1);
        sistema.VisualizarProdutos();
    }
}