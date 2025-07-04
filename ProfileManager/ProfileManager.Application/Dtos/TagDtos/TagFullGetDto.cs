﻿using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Dtos.TagDtos;

public class TagFullGetDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	public static explicit operator TagFullGetDto(Tag tag)
	{
		return new TagFullGetDto()
		{
			Id = tag.Id,
			Name = tag.Name,
			Description = tag.Description,
			Created = tag.CreatedAt,
			Updated = tag.UpdatedAt
		};
	}
}
