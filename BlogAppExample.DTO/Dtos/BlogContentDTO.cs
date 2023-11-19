﻿using BlogAppExample.DTO.Base;

namespace BlogAppExample.DTO.Dtos;

public class BlogContentDTO : IBaseDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CategoryId { get; set; }
    public CategoryDTO CategoryDto { get; set; }
}