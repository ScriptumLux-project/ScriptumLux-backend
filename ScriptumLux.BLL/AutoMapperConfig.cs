using Microsoft.Extensions.DependencyInjection;
using ScriptumLux.BLL.Mappings;

namespace ScriptumLux.BLL
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile).Assembly,
                typeof(MovieProfile).Assembly,
                typeof(CommentProfile).Assembly,
                typeof(ReviewProfile).Assembly,
                typeof(PlaylistProfile).Assembly,
                typeof(HistoryProfile).Assembly,
                typeof(TimecodeProfile).Assembly,
                typeof(PlaylistMovieProfile).Assembly,
                typeof(GenreProfile).Assembly
            );
        }
    }
}