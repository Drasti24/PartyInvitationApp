How to Run the PartyInvitationApp

Prerequisites -
- Install .NET 9.0 SDK
- Install SQLite

Steps to Run
1. Clone the repository and navigate to the project folder.
2. Restore dependencies
   'dotnet restore'
3. Apply database migrations and seed initial data
   'dotnet ef database update'
4. Run the application
   'dotnet run'
5. Open the browser and navigate to
   'https://localhost:7142'

Now you can manage parties, send invitations, and track responses!

Email Configuration
This application sends invitations via email using Gmail SMTP. To enable email functionality:
- Enable "Less secure apps" or App Passwords in your Gmail account.
- Update appsettings.json or environment variables with your credentials:
- 
  {
  
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
  
    "SmtpPort": 587,
  
    "SenderEmail": "your-email@gmail.com",
  
    "SenderPassword": "your-app-password"
  }
}
