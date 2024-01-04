using AutoMapper;

namespace Cardapio.Application.Profiles
{
    public class BaseProfile<Add,Read,T>:Profile where Add : class where T : class where Read : class
    {
        public BaseProfile()
        {
            CreateMap<Add, T>();
            CreateMap<T,Read>();               

            CreateMap<ICollection<Read>, ICollection<T>>()
                .ReverseMap();
        }
    }
}
