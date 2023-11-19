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
    private readonly IMapper _mapper;
    private readonly IUOW _uow;

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
            return Response.Success("Ekleme işlemi başarıyla tamamlandı");
        }
        catch (Exception ex)
        {
            return Response.Failure("Ekleme işlemi yapılamadı");
        }
    }

    public Response Update(TDto dto)
    {
        try
        {
            var Tentity = _mapper.Map<T>(dto);
            _uow.GetRepository<T>().Update(Tentity);
            _uow.SaveChanges();
            return Response.Success("Güncelleme işlemi başarıyla tamamlandı");
        }
        catch (Exception ex)
        {
            return Response.Failure("Güncelleme işlemi yapılamadı");
        }
    }

    public Response Delete(TDto dto)
    {
        try
        {
            var Tentity = _mapper.Map<T>(dto);
            _uow.GetRepository<T>().Delete(Tentity);
            _uow.SaveChanges();
            return Response.Success("Silme işlemi başarıyla tamamlandı");
        }
        catch (Exception ex)
        {
            return Response.Failure("Silme işlemi yapılamadı");
        }
    }

    public Response<TDto> GetById(int id)
    {
        try
        {
            var Tentity = _uow.GetRepository<T>().GetById(id);
            var DtoEntity = _mapper.Map<TDto>(Tentity);
            return Response<TDto>.Success(DtoEntity, "Silme işlemi başarıyla tamamlandı");
        }
        catch (Exception ex)
        {
            return Response<TDto>.Failure("Silme işlemi başarılı olmadı");
        }
    }

    public Response<IEnumerable<TDto>> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var entities = _uow.GetRepository<T>().GetByPredicate(predicate);
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, "Data retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<TDto>>.Failure("Data retrieval failed: " + ex.Message);
        }
    }

    public Response<IEnumerable<TDto>> GetAll()
    {
        try
        {
            var entities = _uow.GetRepository<T>().GetAll();
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, "Data retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<TDto>>.Failure("Data retrieval failed: " + ex.Message);
        }
    }
}