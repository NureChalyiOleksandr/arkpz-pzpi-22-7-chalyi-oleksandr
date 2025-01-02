﻿namespace SmartLightSense.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime LastLogin { get; set; }
}