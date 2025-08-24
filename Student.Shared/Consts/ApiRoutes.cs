namespace Student.Shared.Consts
{
    public static class ApiRoutes
    {
        #region Get All Routes
        public const string BaseGetAll = "GetAll";
        public const string GetAllCourses = $"{BaseGetAll}Courses";
        public const string GetAllEnrolledCourses = $"{BaseGetAll}EnrolledCourses";
        #endregion

        #region Get By Id
        public const string BaseGetAllById = "GetById";
        public const string GetCoursesByStudentId = $"{BaseGetAllById}/CoursesLinkedToStudents";
        #endregion

        #region Add
        public const string BaseAdd = "Create";
        public const string AddCourse = $"{BaseAdd}Courses";
        #endregion

        #region register student
        public const string AddLinkBetweenStudentAndCourse = $"RegisterStudentForCourse";
        #endregion

        #region Update by Id
        public const string BaseUpdateById = "UpdateById";
        public const string UpdateCourse = $"{BaseUpdateById}/Courses";
        #endregion

        #region deregister student from course by Id
        public const string DeleteLinkBetweenStudentAndCourse = $"DeRegisterStudentFromCourse";
        #endregion

        #region account routes
        public const string Login = "Login";
        public const string Logout = "Logout";
        public const string ForgotPassword = "ForgotPassword";
        public const string ResetPassword = "ResetPassword";
        public const string Register = "Register";
        #endregion

    }
}
