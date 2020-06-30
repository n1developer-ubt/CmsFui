using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using CmsFui.Models.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingContentView : ContentView
    {
        private readonly StudentController _studentController;
        public event RegisterCoursesContentView.ActionPerformed Updated;
        public SettingContentView()
        {
            InitializeComponent();
            _studentController = new StudentController();
        }

        public async Task Load()
        {
            var student = await _studentController.GetStudent(Global.CurrentStudent.Id);
            Name.Text = student.Name;
            Email.Text = student.Email;
            Password.Text = student.Password;
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {

            var student = new Student { Id = Global.CurrentStudent.Id,Name = this.Name.Text, Email = Email.Text, Password = Password.Text};

            var result = await _studentController.UpdateStudent(student);

            Updated?.Invoke(result, result == StudentController.UpdateResult.Updated?"Updated Successfully":"Failed To Update");
        }
    }
}