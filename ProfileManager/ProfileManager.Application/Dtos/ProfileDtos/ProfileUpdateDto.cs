﻿using ProfileManager.Domain.Enums;

using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileUpdateDto
{
	[Required]
	public long Id { get; set; }
	[Length(2, 30)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public Gender Gender { get; set; }
	[Required]
	public DateTime Birthdate { get; set; }
	public List<Platform> Platforms { get; set; }

	[Required]
	public bool ReceiveAd { get; set; }
	[Required]
	public bool ReceiveEmails { get; set; }

	public ProfileUpdateDto(long id, string name, string email, Gender gender, DateTime birthdate, bool receiveAd, bool receiveEmails, List<Platform>? platforms = null)
	{
		Id = id;
		Name = name;
		Email = email;
		Gender = gender;
		Birthdate = birthdate;
		ReceiveAd = receiveAd;
		ReceiveEmails = receiveEmails;
		Platforms = platforms ?? [];
	}
}
