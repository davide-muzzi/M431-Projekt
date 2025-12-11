using System.Net.Mime;
using API.Models;
using API.Services.Abstract;
using Shared.DTOs.Teacher;
using Supabase;
using Supabase.Postgrest.Responses;
using static Supabase.Postgrest.Constants;

namespace API.Services;

public class LehrerService : ILehrerService
{
    private readonly Client _supabase;
    

    public LehrerService(Client supabase)
    {
        _supabase = supabase;
        
    }

    public async Task<List<TeacherDTO>?> GetAllTeachers()
    {
        ModeledResponse<Teacher> response = await _supabase.From<Teacher>().Get();
        HttpResponseMessage? resp = response.ResponseMessage;

        if (resp.IsSuccessStatusCode)
        {
            List<TeacherDTO> content = await resp.Content.ReadFromJsonAsync<List<TeacherDTO>>() ?? [];
            return content;
        }
        else
        {
            return null;
        }
    }

    public async Task<TeacherDTO?> GetTeacherByEmail(string email)
    {
        ModeledResponse<Teacher> response = await _supabase.From<Teacher>().Filter("lehrperson.e_mail", Operator.Equals, $"{email}").Get();
        HttpResponseMessage? resp = response.ResponseMessage;

        if (resp.IsSuccessStatusCode)
        {
            TeacherDTO teacher = await resp.Content.ReadFromJsonAsync<TeacherDTO>() ?? new();
            return teacher;
        }
        else
        {
            return null;
        }
    }
    

    //public async Task<List<TeacherDTO>?> AddTeacher(Teacher teacher)
    //{
    //    await _supabase.From<Teacher>().Insert(teacher);
    //    ModeledResponse<Teacher> response = await _supabase.From<Teacher>().Get();
    //    HttpResponseMessage? resp = response.ResponseMessage;

    //    if (resp.IsSuccessStatusCode)
    //    {
    //        List<TeacherDTO> content = await resp.Content.ReadFromJsonAsync<List<TeacherDTO>>() ?? [];
    //        return content;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}