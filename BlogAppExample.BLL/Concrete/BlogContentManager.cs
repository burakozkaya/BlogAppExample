using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Concrete;

public class BlogContentManager : GenericManager<BlogContent, BlogContentDTO>, IBlogContentService
{
    public BlogContentManager(IMapper mapper, IUOW uow) : base(mapper, uow)
    {
    }

    public override Response<IEnumerable<BlogContentDTO>> GetAll()
    {
        try
        {
            var entities = _uow.BlogContentRepo.GetAll();
            var dtos = _mapper.Map<IEnumerable<BlogContentDTO>>(entities);
            return Response<IEnumerable<BlogContentDTO>>.Success(dtos, "Data retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<BlogContentDTO>>.Failure("Data retrieval failed: " + ex.Message);
        }
    }

    public Response Count(BlogContentDTO contentDto)
    {
        try
        {
            var entity = _mapper.Map<BlogContent>(contentDto);
            entity.MinuteDurationForRead += entity.MinuteDurationForRead + 1;
            _uow.BlogContentRepo.Update(entity);
            _uow.SaveChanges();
            return Response.Success("İşlem başarılı");
        }
        catch (Exception e)
        {
            return Response.Failure("işlem başarısız");
        }

    }

    public Response Insert(BlogContentCreateDto contentCreateDto)
    {
        try
        {
            var entity = _mapper.Map<BlogContent>(contentCreateDto);
            _uow.BlogContentRepo.Insert(entity);
            _uow.SaveChanges();
            return Response.Success("Insert Success");
        }
        catch (Exception e)
        {

            return Response.Failure("Insert Fail");
        }
    }

    public Response<IEnumerable<BlogContentDTO>> GetMostReaded()
    {
        var temp = _mapper.Map<IEnumerable<BlogContentDTO>>(_uow.BlogContentRepo.GetMostReaded());
        return Response<IEnumerable<BlogContentDTO>>.Success(temp, "Data retrieved successfully.");
    }
}