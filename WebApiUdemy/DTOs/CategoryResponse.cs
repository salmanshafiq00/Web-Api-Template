﻿namespace WebApiUdemy.DTOs
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
