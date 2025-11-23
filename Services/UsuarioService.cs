using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResponseDto?> CriarAsync(UsuarioCreateDto dto)
        {
            // 1) Criar e salvar o Usuário primeiro (para garantir usuario.Id)
            var usuario = new Usuario
            {
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DataCriacao = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(); // agora temos usuario.Id

            Pessoa pessoa;

            // 2) Se veio token, validar e vincular à Pessoa pré-cadastrada
            if (!string.IsNullOrEmpty(dto.ValidationToken))
            {
                var token = await _context.ValidationTokens
                    .FirstOrDefaultAsync(t => t.Token == dto.ValidationToken && !t.Utilizado);

                if (token == null) throw new Exception("Token inválido ou já utilizado.");

                pessoa = await _context.Pessoas.FindAsync(token.PessoaId)
                    ?? throw new Exception("Pessoa não encontrada.");

                // Vincular pessoa ao novo usuário
                pessoa.UsuarioId = usuario.Id;
                pessoa.CriadorUsuarioId ??= usuario.Id; // mantém quem criou; se não houver, define o próprio

                // Marcar token como utilizado
                token.Utilizado = true;

                // Atualizar usuário com PessoaId
                usuario.PessoaId = pessoa.Id;

                await _context.SaveChangesAsync();
            }
            else
            {
                // 3) Sem token: criar Pessoa vinculada ao usuário recém-criado
                pessoa = new Pessoa
                {
                    Nome = dto.NomePessoa,
                    DataNascimento = dto.DataNascimento,
                    CPF = dto.Cpf,
                    UsuarioId = usuario.Id,          // vínculo direto
                    CriadorUsuarioId = usuario.Id     // quem criou a pessoa
                };

                _context.Pessoas.Add(pessoa);
                await _context.SaveChangesAsync();

                // Atualizar usuário com PessoaId
                usuario.PessoaId = pessoa.Id;
                await _context.SaveChangesAsync();
            }

            // 4) Resposta
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao,
                PessoaId = usuario.PessoaId,
                NomePessoa = pessoa.Nome
            };
        }

        public async Task<bool> VincularTokenAsync(int usuarioId, string tokenValue)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return false;

            var token = await _context.ValidationTokens
                .FirstOrDefaultAsync(t => t.Token == tokenValue && !t.Utilizado);

            if (token == null) return false;

            var pessoa = await _context.Pessoas.FindAsync(token.PessoaId);
            if (pessoa == null) return false;

            // Vincular pessoa ao usuario e consumir token
            pessoa.UsuarioId = usuarioId;
            pessoa.CriadorUsuarioId ??= usuarioId;
            usuario.PessoaId = pessoa.Id;
            token.Utilizado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioResponseDto?> ObterPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.RegistroPessoas)
                        .ThenInclude(rp => rp.Registro)
                            .ThenInclude(r => r.RegistroPessoas)
                                .ThenInclude(rp2 => rp2.Pessoa)
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.RegistroPessoas)
                        .ThenInclude(rp => rp.Registro)
                            .ThenInclude(r => r.Criador)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return null;

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao,
                PessoaId = usuario.PessoaId,
                NomePessoa = usuario.Pessoa?.Nome,
                RegistrosCriados = usuario.Pessoa?.RegistroPessoas
                    .Where(rp => rp.Registro is not null)
                    .Select(rp => new RegistroResponseDto
                    {
                        Id = rp.Registro.Id,
                        Data = rp.Registro.Data,
                        Legenda = rp.Registro.Legenda,
                        FotoPath = rp.Registro.FotoPath,
                        PessoasEnvolvidas = rp.Registro.RegistroPessoas?
                            .Where(rp2 => rp2.Pessoa is not null)
                            .Select(rp2 => new PessoaResumoDto
                            {
                                Id = rp2.Pessoa.Id,
                                Nome = rp2.Pessoa.Nome
                            }).ToList() ?? new List<PessoaResumoDto>(),
                        CriadorId = rp.Registro.CriadorId,
                        CriadorEmail = rp.Registro.Criador.Email
                    }).ToList() ?? new List<RegistroResponseDto>()
            };
        }

        public async Task<IEnumerable<UsuarioDto>> ObterTodosAsync()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.RegistroPessoas)
                        .ThenInclude(rp => rp.Registro)
                .ToListAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nickname = u.Pessoa?.Nome ?? "", // ou outro campo se houver
                Email = u.Email,
                DataDeCriacao = u.DataCriacao,
                PessoaId = u.PessoaId,
                PessoaNome = u.Pessoa?.Nome ?? ""
            });
        }
    }
}
