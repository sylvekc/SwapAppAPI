﻿using System.ComponentModel.DataAnnotations;

namespace SwapApp.Models
{
    public class UpdateItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ForFree { get; set; }
        public string SwapFor { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
    }
}
