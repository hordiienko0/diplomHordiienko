using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Projects.Commands;

public class DeleteProjectPhotoByIdResponseDto: IMapFrom<ProjectPhoto>
{
    public long Id { get; set; }
    public long ProjectId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectPhoto, DeleteProjectPhotoByIdResponseDto>().ReverseMap();
    }
}