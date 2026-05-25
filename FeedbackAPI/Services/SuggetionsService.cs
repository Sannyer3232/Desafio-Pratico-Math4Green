using FeedbackAPI.Data;
using FeedbackAPI.Models;
using FeedbackAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAPI.Services;

public class SuggestionService
{
    private readonly AppDbContext _context;

    public SuggestionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Suggestion> createSuggestionAsync(CreateSuggetionsDto dto)
    {
        var userExists = await _context.Users.AnyAsync( u => u.id == dto.userId);

        if(!userExists) throw new Exception("Usuário não encontrado");

        var suggestion = new Suggestion
        {
            title = dto.title,
            description = dto.description,
            userId = dto.userId,
            createdAt = DateTime.Now
        };

        _context.Suggestions.Add(suggestion);
        await _context.SaveChangesAsync();
        return suggestion;
    }

    public async Task<List<SuggestionResponseDto>> getAllSuggestionsAsync()
    {

        var oneHourAgo = DateTime.Now.AddHours(-1);

        return await _context.Suggestions
                    .Include(s => s.user)
                    .Include(s => s.votes)
                    .Select(s => new SuggestionResponseDto
                    {
                        id = s.id,
                        title = s.title,
                        description = s.description,
                        createdAt = s.createdAt,
                        username = s.user != null ? s.user.username: "Anônimo",

                        voteCount = s.votes.Count(),

                        isTrending = s.votes.Any(v => v.voteAt >= oneHourAgo)
            
                    })
                    .OrderByDescending(dto => dto.voteCount)
                    .ToListAsync();

    }

    public async Task addVoteAsync(CreateVoteDto dto)
    {
        var suggestion = await _context.Suggestions.FindAsync(dto.suggestionId);

        if(suggestion == null) throw new Exception("Sugestão não Encontrada. ");

        var userExists = await _context.Users.AnyAsync( u => u.id == dto.userId);
        
        if(!userExists) throw new Exception("Usuário não encontrado.");

        if (suggestion.userId == dto.userId)
        {
            throw new Exception("Você não pode votar na sua própria sugestão");
        }

        var today = DateTime.Today;
        var votosHoje = await _context.Votes
                        .CountAsync(v => v.userId == dto.userId && v.voteAt >=today);
        
        if(votosHoje >= 3)
        {
            throw new Exception("Você atingiu seu limite diário de 3 votos.");
        }

        var jaVotouNessa = await _context.Votes
                                .AnyAsync(v => v.userId == dto.userId && v.suggestionId == dto.suggestionId);

        if (jaVotouNessa)
        {
            throw new Exception("Você já voltou nesta sugestão.");
        }                     

        var vote = new Vote
        {
            userId = dto.userId,
            suggestionId = dto.suggestionId,
            voteAt = DateTime.Now
        };

        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();
    }
}