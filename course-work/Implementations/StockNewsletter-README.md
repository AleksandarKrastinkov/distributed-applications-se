<<<<<<< HEAD
# 📈 Stock Newsletter

A modern ASP.NET Core web application for tracking stock earnings, managing watchlists, and staying updated with market information.

## ✨ Features

### 🗓️ Earnings Calendar
- View upcoming earnings reports for the next 3 months
- Search and filter earnings by company symbol or name
- Real-time data from AlphaVantage API
- Intelligent caching to optimize API usage

### 📊 Historical Earnings
- Detailed quarterly and annual earnings data
- EPS (Earnings Per Share) information
- Earnings surprises and estimates
- Clean, tabular data presentation

### 📋 Personal Watchlist
- Add stocks to your personal watchlist
- Smart stock search with autocomplete
- Add notes for each stock
- Manage your portfolio tracking

### 🔍 Quick Stock Search
- Fast stock lookup from the homepage
- Popular stock quick-access buttons
- Seamless integration with earnings data
- Real-time search validation

### 🔐 User Authentication
- Secure user registration and login
- Personal data isolation
- Session management

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 9.0 (C#)
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript
- **Database**: Entity Framework Core with SQL Server
- **API**: AlphaVantage Financial Data API
- **Caching**: In-Memory Caching
- **Authentication**: ASP.NET Core Identity

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB is fine for development)
- [AlphaVantage API Key](https://www.alphavantage.co/support/#api-key) (Free tier available)

### Installation

1. **Clone the repository**
   ```bash
   git clone <your-repository-url>
   cd StockNewsletter
   ```

2. **Configure the database connection**
   
   Update `appsettings.json` with your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StockNewsletterDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Add your AlphaVantage API Key**
   
   Create `appsettings.Development.json` (this file is gitignored):
   ```json
   {
     "AlphaVantage": {
       "ApiKey": "YOUR_API_KEY_HERE"
     }
   }
   ```

4. **Install dependencies and run migrations**
   ```bash
   dotnet restore
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Open your browser**
   
   Navigate to `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure

```
StockNewsletter/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── EarningsController.cs
│   ├── WatchlistController.cs
│   └── AccountController.cs
├── Models/               # Data Models
│   ├── CompanyEarnings.cs
│   ├── EarningsDaily.cs
│   └── WatchList.cs
├── Services/             # Business Logic
│   └── AlphaVantageService.cs
├── Views/                # Razor Views
│   ├── Home/
│   ├── Earnings/
│   ├── Watchlist/
│   └── Shared/
├── Data/                 # Database Context
│   └── AppDbContext.cs
├── Migrations/           # EF Core Migrations
└── wwwroot/             # Static Files
    ├── css/
    ├── js/
    └── lib/
```

## 🔧 Configuration

### API Settings

The application uses the AlphaVantage API for financial data. Configure your API key in `appsettings.Development.json`:

```json
{
  "AlphaVantage": {
    "ApiKey": "YOUR_API_KEY_HERE"
  }
}
```

### Caching Configuration

The application implements intelligent caching to optimize API usage:

- **Earnings Calendar**: Cached for 7 days
- **Historical Earnings**: Cached for 24 hours
- **Stock Options**: Cached for 7 days

### Database Configuration

The application uses Entity Framework Core with SQL Server. Update the connection string in `appsettings.json` for your environment.

## 📊 API Usage

### AlphaVantage API Limits

- **Free Tier**: 25 requests per day
- **Premium**: Higher limits available

The application includes fallback data when API limits are reached.

### Supported Endpoints

- `EARNINGS_CALENDAR`: Upcoming earnings reports
- `EARNINGS`: Historical earnings data

## 🎨 User Interface

### Responsive Design
- Mobile-friendly Bootstrap 5 interface
- Clean, modern design
- Intuitive navigation

### Key Pages
- **Home**: Dashboard with quick search and market overview
- **Earnings Calendar**: Comprehensive earnings schedule
- **Historical Earnings**: Detailed earnings analysis
- **Watchlist**: Personal stock tracking
- **Privacy**: Privacy policy and disclaimers

## 🔒 Security Features

- User authentication and authorization
- Data isolation per user
- Input validation and sanitization
- CSRF protection
- Secure session management

## 🚀 Deployment

### Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release
```

Make sure to:
1. Update connection strings for production database
2. Set production API keys
3. Configure HTTPS certificates
4. Set up proper logging

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is for educational and informational purposes only. Not affiliated with AlphaVantage or any financial service provider.

## ⚠️ Disclaimer

This application is for educational purposes only. Do not use for illegal activities. Always verify financial data from official sources before making investment decisions.

## 🆘 Support

If you encounter any issues:

1. Check the [Issues](../../issues) page
2. Review the configuration steps
3. Verify your API key is valid
4. Check the application logs

## 🔄 Updates

- **v1.0.0**: Initial release with core functionality
- **v1.1.0**: Added Quick Stock Search and improved UI
- **v1.2.0**: Enhanced caching and error handling

---

**Built with ❤️ using ASP.NET Core** 
=======
## Introduction to Distributed applications
<sup>This repository is part of the **Distributed applications** for part time students of Software Engineering at Plovdiv University.</sup>\
This course is designed to provide an overview of the principles and practices involved in building distributed applications. It emphasizes the importance of creating robust distributed applications that align with program specifications for software projects.

Participants will also explore the theory and formal processes of application architecture and the development of modern applications. The course covers modern distributed architectures, including SOA, Microservices, Client-Server, and N-Tier, as well as technologies such as RESTful, SOAP, gRPC, and GraphQL web services. By the end of the course, students will be equipped with the knowledge and skills to design, develop, and deploy scalable and secure distributed systems.

### POLL

### Competencies
Upon successful completion of this course, participants will:
#### Knowledge:
* Understanding of distributed architectures such as SOA, Microservices, Client-Server, and N-Tier.
* Ability to design and implement RESTful, SOAP, gRPC, and GraphQL web services.
* Knowledge of cloud computing concepts, including serverless architecture and cloud databases.
* Proficiency in event-driven architecture and IoT systems.
#### Skills:
* Develop distributed applications using modern technologies.
* Implement CRUD operations with pagination and sorting.
* Secure web services and client applications.
* Deploy applications to cloud platforms and manage cloud resources.

### Course Structure
The course is structured into several key sections, each focusing on a different aspect of distributed applications. The sections are designed to be followed in order.
* [Course Roadmap](https://miro.com/app/board/o9J_lLSa4dg=/?share_link_id=774145796745)

### Getting Started
To get started with the course, clone this repository and follow the instructions in each section. If you have any questions or run into any issues, please open an issue and we'll do our best to help.
* [Exercises need tools](https://github.com/pkyurkchiev/distributed-applications-se/blob/master/documentations/tools.md)
* [Old exercises](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/exercises)
* [Nuget console](https://github.com/pkyurkchiev/distributed-applications-se/blob/master/documentations/nuget-console.md)
* [Test project authentication](https://github.com/pkyurkchiev/distributed-applications-se/blob/master/documentations/project-authentication.md)
* [Setup firebase](https://github.com/pkyurkchiev/distributed-applications-se/blob/master/documentations/setup-firebase.md)

### Days Schedule
* First week
  * presentations - [Distributed application](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-01.pdf), [Network](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-02.pdf)
* Second week: 
  * presentations - [SOAP Web services](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-03.pdf), [RESTful Web services](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-04.pdf)
* Third week:
  * presentations -  [gRPC Web services](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-05.pdf), [Example - gRPC Web services](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/examples/GrpcGreeter)
* Fourth week:
  * presentations - [Distributed architectures: Client - Server and N - Tier](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-06.pdf)
* Fifth week:
  * presentations - [Distributed architectures: SOA](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-07.pdf), [Distributed architectures: Microservices](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/presentations/Lecture-08.pdf)
* Six week:
  * presentations - 
* Seven week:
  * presentations - 

### Course assignment
* [Task](https://github.com/pkyurkchiev/distributed-applications-se/blob/master/course-work/README.md)
* [Assessment Requirements](https://github.com/pkyurkchiev/distributed-applications-se/tree/master/documentations/assessment-requirements.md)

### License
This course is licensed under the MIT License. See the LICENSE file for more details.
>>>>>>> origin/master
