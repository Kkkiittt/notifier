
using Notifier.Shared.Interfaces;

using ProfileManager.Application.Dtos.TagDtos;
using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Application.Interfaces.Services;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Services;

public class TagService : ITagService
{
	private readonly ITagRepository _tagRepository;

	public TagService(ITagRepository tagRepository, IUserIdentifier userId)
	{
		_tagRepository = tagRepository;
	}

	public async Task<bool> CreateTagAsync(TagCreateDto dto)
	{
		Tag tag = new(dto.Name, dto.Description);
		_tagRepository.Create(tag);

		return await _tagRepository.SaveChangesAsync();
	}

	public async Task<bool> DeleteTagAsync(long id)
	{
		Tag? tag = await _tagRepository.GetAsync(id);
		if(tag is null)
			throw new Exception("Tag not found");

		_tagRepository.Delete(tag);
		return await _tagRepository.SaveChangesAsync();
	}

	public async Task<TagGetDto> GetTagAsync(long id)
	{
		Tag? tag = await _tagRepository.GetAsync(id);
		if(tag is null)
			throw new Exception("Tag not found");

		return (TagGetDto)tag;
	}

	public async Task<List<TagGetDto>> GetTagsAsync(int page, int pageSize)
	{
		if(page < 1 || pageSize < 1)
			throw new Exception("Invalid page or pageSize");

		if(pageSize > 50)
			pageSize = 50;

		int skip = (page - 1) * pageSize;
		var tags = await _tagRepository.GetManyAsync(skip, pageSize);

		return tags.Select(t => (TagGetDto)t).ToList();
	}

	public async Task<bool> UpdateTagAsync(TagUpdateDto dto)
	{
		Tag? tag = await _tagRepository.GetAsync(dto.Id);
		if(tag is null)
			throw new Exception("Tag not found");

		tag.Name = dto.Name;
		tag.Description = dto.Description;
		tag.UpdatedAt = DateTime.UtcNow;

		_tagRepository.Update(tag);
		return await _tagRepository.SaveChangesAsync();
	}
}
