using ProfileManager.Application.Dtos.TagDtos;

namespace ProfileManager.Application.Interfaces.Services;

public interface ITagService
{
	public Task<bool> CreateTagAsync(TagCreateDto dto);

	public Task<bool> UpdateTagAsync(TagUpdateDto dto);

	public Task<bool> DeleteTagAsync(long id);

	public Task<TagFullGetDto> GetTagAsync(long id);

	public Task<List<TagFullGetDto>> GetTagsAsync(int page, int pageSize);
}
