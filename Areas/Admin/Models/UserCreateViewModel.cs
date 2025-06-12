﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Areas.Admin.Models;

public class UserCreateViewModel
{
    public required string Username { get; set; }
    public required string FullName { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public required string PhoneNumber { get; set; }

    [DataType(DataType.Password)]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
    public required string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
    public List<SelectListItem>? Roles { get; set; } = new List<SelectListItem>();
    public List<string?> SelectedRole { get; set; } = [];
}

