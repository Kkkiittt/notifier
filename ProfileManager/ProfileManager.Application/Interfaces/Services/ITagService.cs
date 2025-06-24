using ProfileManager.Application.Dtos.TagDtos;

namespace ProfileManager.Application.Interfaces.Services;

public interface ITagService
{
	public Task<bool> CreateTagAsync(TagCreateDto dto);

	public Task<bool> UpdateTagAsync(TagUpdateDto dto);

	public Task<bool> DeleteTagAsync(long id);

	public Task<TagGetDto> GetTagAsync(long id);

	public Task<List<TagGetDto>> GetTagsAsync(int page, int pageSize);
}
