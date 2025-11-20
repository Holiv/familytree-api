using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FamilyTree.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResponseDto> CriarAsync(UsuarioCreateDto dto)
        {
            var pessoa = new Pessoa
            {
                Nome = dto.NomePessoa,
                DataNascimento = dto.DataNascimento,
                CPF = dto.Cpf
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            var usuario = new Usuario
            {
                Email = dto.Email,
                SenhaHash = HashSenha(dto.Senha),
                DataCriacao = DateTime.UtcNow,
                PessoaId = pessoa.Id
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao,
                PessoaId = pessoa.Id,
                NomePessoa = pessoa.Nome
            };
        }

        public async Task<UsuarioResponseDto?> ObterPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario is null) return null;

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao,
                PessoaId = usuario.PessoaId,
                NomePessoa = usuario.Pessoa.Nome
            };
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Pessoa)
                .ToListAsync();

            return usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                DataCriacao = u.DataCriacao,
                PessoaId = u.PessoaId,
                NomePessoa = u.Pessoa.Nome
            });
        }

        private static string HashSenha(string senha)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}
