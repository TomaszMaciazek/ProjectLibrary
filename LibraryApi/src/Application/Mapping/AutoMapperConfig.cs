using AutoMapper;

namespace Application.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {

                config.AddEntityToDtoMaps();
                config.AddViewModelToEntityMaps();

            })
            .CreateMapper();
    }
}
