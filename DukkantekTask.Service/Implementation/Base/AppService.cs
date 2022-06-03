using AutoMapper;
using DukkantekTask.Domain.UoW;

namespace DukkantekTask.Service.Implementation.Base
{
    /// <summary>
    /// Base App Service, all other application services should inherit from this class
    /// Containing references to UnitOfWork and Mapper objects
    /// </summary>
    public class AppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        protected internal IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork; ;
            }
        }
        protected internal IMapper ObjectMapper
        {
            get
            {
                return _mapper; ;
            }
        }
    }
}
