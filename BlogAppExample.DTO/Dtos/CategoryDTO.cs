using BlogAppExample.DTO.Base;

namespace BlogAppExample.DTO.Dtos;

public class CategoryDTO : IBaseDTO
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public List<BlogContentDTO> BlogContents { get; set; }
}