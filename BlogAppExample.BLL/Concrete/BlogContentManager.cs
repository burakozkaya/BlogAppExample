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
}