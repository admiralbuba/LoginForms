namespace Login
{
    public partial class LoginForm : Form
    {
        int attempts = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernametTextBox.Text;
            string password = SHA256Helper.GetHash(passwordTextBox.Text);
            using ApplicationContext db = new();
            var user = db.UserDatas.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                if (user.LastFailedAttemptDate != null)
                {
                    var interval = DateTime.Now - user.LastFailedAttemptDate;
                    if (interval.Value.Seconds > 10)
                        Login(user, password);
                    else
                        MessageBox.Show("User was blocked for 10 seconds");
                }
                else if (attempts >= 3)
                    MessageBox.Show("User was blocked for 10 seconds");
                else
                    Login(user, password);
            }
            else
                MessageBox.Show("Unable to Login, incorrect credentials.");
            db.SaveChanges();
        }
        private void Login(UserData user, string password)
        {
            if (password == user.PasswordHash)
            {
                switch (user.Role)
                {
                    case Roles.User:
                        MessageBox.Show("Successfully Logged in, " + user.UserName);
                        UserForm userForm = new();
                        userForm.Show();
                        this.Hide();
                        break;
                    case Roles.Manager:
                        MessageBox.Show("Successfully Logged in, " + user.UserName);
                        ManagerForm managerForm = new();
                        managerForm.Show();
                        this.Hide();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Unable to Login, incorrect credentials.");
                attempts++;
                if (attempts >= 3)
                {
                    MessageBox.Show("User was blocked for 10 seconds");
                    user.LastFailedAttemptDate = DateTime.Now;
                }
            }
        }
    }
}