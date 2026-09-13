using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Application.DTOs;

namespace LmsProject.Application.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceSessionDto?> GetAttendanceSessionAsync(int courseId, DateTime date);
        Task SaveAttendanceAsync(int courseId, DateTime date, List<int> presentUserProfileIds);
    }
}