﻿namespace Application.Photos.DTOs;

public class PhotoDto
{
    public int PhotoId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}
