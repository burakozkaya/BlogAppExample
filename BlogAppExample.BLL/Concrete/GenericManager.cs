using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.DTO.Base;
using BlogAppExample.Entity.Abstract;
using System.Linq.Expressions;

namespace BlogAppExample.BLL.Concrete;

public class GenericManager<T, TDto> : IGenericService<T, TDto>
    where T : class, IBaseEntity
    where TDto : class, IBaseDTO
{
    protected readonly IMapper _mapper;
    protected readonly IUOW _uow;

    public GenericManager(IMapper mapper, IUOW uow)
    {
        _mapper = mapper;
        _uow = uow;
    }
    public Response Insert(TDto dto)
    {
        try
        {
            var Tentity = _mapper.Map<T>(dto);
            _uow.GetRepository<T>().Insert(Tentity);
            _uow.SaveChanges();
            return Response.Success("Insert success");
        }
        catch (Exception ex)
        {
            return Response.Failure("Insert failed");
        }
    }

    public Response Update(TDto dto)
    {
        try
        {
            var Tentity = _mapper.Map<T>(dto);

            _uow.GetRepository<T>().Update(Tentity);
            _uow.SaveChanges();
            return Response.Success("Update success");
        }
        catch (Exception ex)
        {
            return Response.Failure("Update failed");
        }
    }

    public Response Delete(TDto dto)
    {
        try
        {
            var Tentity = _mapper.Map<T>(dto);
            _uow.GetRepository<T>().Delete(Tentity);
            _uow.SaveChanges();
            return Response.Success("Delete success");
        }
        catch (Exception ex)
        {
            return Response.Failure("Delete failed");
        }
    }

    public Response<TDto> GetById(int id)
    {
        try
        {
            var Tentity = _uow.GetRepository<T>().GetById(id);
            var DtoEntity = _mapper.Map<TDto>(Tentity);
            return Response<TDto>.Success(DtoEntity, "Data retrieved successfully");
        }
        catch (Exception ex)
        {
            return Response<TDto>.Failure("Data retrieval failed");
        }
    }

    public virtual Response<IEnumerable<TDto>> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var entities = _uow.GetRepository<T>().GetByPredicate(predicate);
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, "Data retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<TDto>>.Failure("Data retrieval failed");
        }
    }

    public virtual Response<IEnumerable<TDto>> GetAll()
    {
        try
        {
            var entities = _uow.GetRepository<T>().GetAll();
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, "Data retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<TDto>>.Failure("Data retrieval failed");
        }
    }
}