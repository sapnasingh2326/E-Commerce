namespace ECommerceWeb.VModel
{
    public class LoginVM
    {
        public class LoginVModel
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
            //[BindProperty]
            //[Required(ErrorMessage = "Username is required")]
            //public string Username { get; set; }

            //[BindProperty]
            //[Required(ErrorMessage = "Password is required")]
            //[DataType(DataType.Password)]
            //public string Password { get; set; }

            //public void OnGet()
            //{

            //}
            //public async Task<IActionResult> OnPostAsync()
            //{
            //    if (!ModelAdminDashboard.IsValid)
            //    {
            //        return Page();
            //    }
            //    bool isValidUser = (Username == "testuser" && Password == "password"); // Example validation

            //    if (isValidUser)
            //    {
            //        return RedirectToPage("/Index");
            //    }
            //    else
            //    {
            //        ModelAdminDashboard.AddModelError(string.Empty, "Invalid login attempt.");
            //        return Page();
            //    }
            //}
        }
    }
}
