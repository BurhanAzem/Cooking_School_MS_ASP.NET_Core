﻿using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos;
using Cooking_School.Services.Dtos.CookClassDto;
using Cooking_School.Services.Dtos.ProjectDto;


namespace Cooking_School.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ResponsDto<ProjectDTO>> CreateProject(CreateProjectDto createProjectDto, int cookClassId);
        Task<ResponsDto<ProjectDTO>> DeleteProject(int projectId);
        Task<ResponsDto<ProjectDTO>> UpdateProject(int projectId, UpdateProjectDto updateProjectDto);
        Task<ResponsDto<ProjectDTO>> GetProjectById(int projectId);
        Task<ResponsDto<ProjectDTO>> GetAllProjectOfCookClass(RequestParam requestParams, int cookClassId);
        Task<ResponsDto<string>> EvaluateTraineeProject(decimal mark, int prjectId, int traineeId);

    }
}
