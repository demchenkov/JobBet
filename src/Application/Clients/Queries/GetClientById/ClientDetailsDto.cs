using AutoMapper;
using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;

namespace JobBet.Application.Clients.Queries.GetClientById;

public class ClientDetailsDto : IMapFrom<Client>
{
    public int Id { get; set; }
    
    public string Title { get; set; } = default!;

    public double? Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientDetailsDto>()
            .ForMember(x => x.Score, opt => opt.MapFrom(s => s.Rating!.Score));
    }
}