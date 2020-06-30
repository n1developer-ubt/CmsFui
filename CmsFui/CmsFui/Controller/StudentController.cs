using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CmsFui.Models;
using CmsFui.Models.Data;
using Newtonsoft.Json;

namespace CmsFui.Controller
{
    public class StudentController
    {
        private const string RestApiAddress = "http://192.168.8.102:5000";
        private const string RestApiVersion = "v1";
        private const string Controller = "student";

        private static readonly string RestFullAddress = $"{RestApiAddress}/api/{RestApiVersion}/{Controller}";

        public static string StudentApi = $"{RestFullAddress}/GetStudent";
        public static string AuthenticateStudent = $"{RestFullAddress}/authenticate";
        public static string SemesterCoursesResult = $"{RestFullAddress}/GetSemesterCoursesResult";
        public static string UnRegisteredCourses = $"{RestFullAddress}/GetUnRegisteredCourses";
        public static string RegisteredCourses = $"{RestFullAddress}/GetRegisteredCourses";
        public static string RegisterCoursesApi = $"{RestFullAddress}/RegisterCourses";
        public static string AttendanceApi = $"{RestFullAddress}/GetAttendance";
        public static string CourseExams = $"{RestFullAddress}/GetCourseExams";
        public static string UpdateStudentApi = $"{RestFullAddress}/UpdateStudent";

        private readonly HttpClient _httpClient;

        public StudentController()
        {
            _httpClient = new HttpClient();
        }

        public enum UpdateResult
        {
            Updated,
            UpdatedFailed
        }

        public async Task<UpdateResult> UpdateStudent(Student newStudent)
        {
            var url = UpdateStudentApi;
            var res = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(newStudent), Encoding.UTF8, "application/json"));

            if (!res.IsSuccessStatusCode)
                return UpdateResult.UpdatedFailed;

            return UpdateResult.Updated;
        }

        public async Task<List<StudentExam>> GetCourseExams(int studentId, string courseCode)
        {
            var url = CourseExams + "?" + GetQuery(("studentId", studentId.ToString()), ("courseCode", courseCode));

            var res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<StudentExam>>(result);
        }

        public async Task<List<Attendance>> GetAttendance(int studentId, string courseCode)
        {
            var url = AttendanceApi + "?" + GetQuery(("studentId", studentId.ToString()), ("courseCode", courseCode));

            var res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Attendance>>(result);
        }

        public async Task<UpdateResult> RegisterCourses(int studentId, List<int> courseIds)
        {
            var url = RegisterCoursesApi + "?" + GetQuery(("studentId", studentId.ToString()));
            var res = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(courseIds), Encoding.UTF8, "application/json"));

            if (!res.IsSuccessStatusCode)
                return UpdateResult.UpdatedFailed;

            return UpdateResult.Updated;
        }

        public async Task<List<SemesterCourse>> GetCourses(int studentId, bool registered = true)
        {
            var url = (registered ? RegisteredCourses : UnRegisteredCourses) + "?" + GetQuery(("studentId", studentId.ToString()));

            var res = await _httpClient.GetAsync(url);


            Console.WriteLine("Hello\nHello\nHello\nHello\nHello\nHello\nHello\nHello\nHello\n");

            if (!res.IsSuccessStatusCode)
                return null;

            Console.WriteLine("Hello\nHello\nHello\nHello\nHello\nHello\nHello\nHello\nHello\n");

            var result = await res.Content.ReadAsStringAsync();

            Console.Write(result);

            return JsonConvert.DeserializeObject<List<SemesterCourse>>(result);
        }

        public async Task<List<StudentSemester>> GetStudentSemesters(int studentId)
        {
            var url = SemesterCoursesResult + "?" + GetQuery(("studentId", studentId.ToString()));
            var res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<List<StudentSemester>>(result);
        }



        public async Task<Student> Authenticate(AuthenticationModel auth)
        {
            var url = AuthenticateStudent + "?" + GetQueryString(auth);
            var res = await _httpClient.GetAsync(url);

            Console.WriteLine("\nx\nx\nx\nx\nx\n" + url + "\n" + res.StatusCode.ToString() + "\n\n\n\n\n");

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Student>(result);
            }

            return null;
        }

        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public async Task<Student> GetStudent(int studentId)
        {
            var url = StudentApi + "?" + GetQuery(("studentId", studentId.ToString()));
            var res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Student>(result);
        }

        private string GetQuery(params (string, string)[] queries)
        {
            var queryParse = HttpUtility.ParseQueryString("");

            foreach (var query in queries)
            {
                queryParse[query.Item1] = query.Item2;
            }

            return queryParse.ToString();
        }

    }
}
