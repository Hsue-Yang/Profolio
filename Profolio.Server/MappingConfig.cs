using Mapster;
using Profolio.Server.Dto.Article;
using Profolio.Server.Dto.Profile;
using Profolio.Server.Models.Entities;

namespace Profolio.Server
{
	public class MappingConfig
    {
        public static void RegisterMapping()
        {
            TypeAdapterConfig<Tag, TagDto>.NewConfig();
            TypeAdapterConfig<ProfileDto.Timeline, Timeline>.NewConfig();
            TypeAdapterConfig<Timeline, ProfileDto.Timeline>.NewConfig();
            TypeAdapterConfig<HackMDNoteTag, HackMDNoteTagDto>.NewConfig();
            TypeAdapterConfig<HackMDNote, HackMDNoteDto>.NewConfig()
                .Map(dest => dest.Tags, src => src.Tags != null ? src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>())
                .Map(dest => dest.CreatedAtString, src => src.CreatedAt.ToString("yyyy-MM-dd"))
                .Map(dest => dest.UpdatedAtString, src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("yyyy-MM-dd") : null);
            TypeAdapterConfig<ProfileDto.Timeline, Timeline>.NewConfig().Map(dest => dest.TimePoint, src => src.TimePoint.ToDateTime(TimeOnly.MinValue));
            TypeAdapterConfig<Timeline, ProfileDto.Timeline>.NewConfig().Map(dest => dest.TimePoint, src => DateOnly.FromDateTime(src.TimePoint));

            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true); //prevent infinite loop for nested object
        }
    }
}