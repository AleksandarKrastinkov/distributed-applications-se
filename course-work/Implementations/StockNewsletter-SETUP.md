# 🔧 Setup Guide - Stock Newsletter

This guide will help you set up the Stock Newsletter application securely with your AlphaVantage API key.

## 🔑 API Key Configuration

### Step 1: Get Your AlphaVantage API Key

1. Visit [AlphaVantage](https://www.alphavantage.co/support/#api-key)
2. Sign up for a free account
3. Get your API key (free tier includes 25 requests per day)

### Step 2: Secure Configuration Setup

**⚠️ IMPORTANT: Never commit your API key to version control!**

#### Option A: Using appsettings.Development.json (Recommended)

1. Create `appsettings.Development.json` in the `StockNewsletter` folder:
   ```json
   {
     "AlphaVantage": {
       "ApiKey": "YOUR_ACTUAL_API_KEY_HERE"
     }
   }
   ```

2. This file is automatically ignored by git (see `.gitignore`)

#### Option B: Using User Secrets (Most Secure)

1. Navigate to the project directory:
   ```bash
   cd StockNewsletter
   ```

2. Initialize user secrets:
   ```bash
   dotnet user-secrets init
   ```

3. Set your API key:
   ```bash
   dotnet user-secrets set "AlphaVantage:ApiKey" "YOUR_ACTUAL_API_KEY_HERE"
   ```

4. Verify it was set:
   ```bash
   dotnet user-secrets list
   ```

#### Option C: Environment Variables

1. Set environment variable:
   
   **Windows (PowerShell):**
   ```powershell
   $env:AlphaVantage__ApiKey = "YOUR_ACTUAL_API_KEY_HERE"
   ```
   
   **Windows (Command Prompt):**
   ```cmd
   set AlphaVantage__ApiKey=YOUR_ACTUAL_API_KEY_HERE
   ```
   
   **Linux/Mac:**
   ```bash
   export AlphaVantage__ApiKey="YOUR_ACTUAL_API_KEY_HERE"
   ```

## 🗄️ Database Setup

### Step 1: Install Entity Framework Tools

```bash
dotnet tool install --global dotnet-ef
```

### Step 2: Update Database Connection String

Edit `appsettings.json` and update the connection string if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StockNewsletterDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Step 3: Run Database Migrations

```bash
cd StockNewsletter
dotnet ef database update
```

## 🚀 Running the Application

### Development Mode

```bash
cd StockNewsletter
dotnet run
```

The application will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### Production Mode

```bash
dotnet run --environment Production
```

## 🔒 Security Checklist

- [ ] ✅ API key is NOT in `appsettings.json`
- [ ] ✅ API key is in `appsettings.Development.json` (gitignored)
- [ ] ✅ Or API key is in User Secrets
- [ ] ✅ Or API key is in environment variables
- [ ] ✅ `.gitignore` file is in place
- [ ] ✅ No sensitive data in version control

## 🧪 Testing Your Setup

1. Start the application
2. Navigate to the home page
3. Try the Quick Stock Search with "AAPL"
4. Check the Earnings Calendar
5. Verify data loads correctly

## 🚨 Troubleshooting

### API Key Issues

**Problem**: "AlphaVantage API key is required" error
**Solution**: 
- Verify your API key is set correctly
- Check the configuration method you're using
- Restart the application after setting the key

**Problem**: "Rate limit" or "API error" messages
**Solution**:
- You've hit the 25 requests/day limit (free tier)
- Wait until the next day or upgrade your plan
- The app will show fallback data when limits are hit

### Database Issues

**Problem**: Database connection errors
**Solution**:
- Ensure SQL Server LocalDB is installed
- Run `dotnet ef database update`
- Check the connection string in `appsettings.json`

### Build Issues

**Problem**: Compilation errors
**Solution**:
- Ensure you have .NET 9.0 SDK installed
- Run `dotnet restore`
- Check for missing dependencies

## 📁 File Structure After Setup

```
StockNewsletter/
├── appsettings.json                    # ✅ Safe to commit
├── appsettings.Development.json        # ❌ Contains API key - gitignored
├── appsettings.json.template          # ✅ Template file - safe to commit
├── Controllers/
├── Models/
├── Services/
├── Views/
└── ...
```

## 🔄 Configuration Priority

ASP.NET Core loads configuration in this order (later sources override earlier ones):

1. `appsettings.json`
2. `appsettings.{Environment}.json`
3. User Secrets (Development only)
4. Environment Variables
5. Command Line Arguments

## 📞 Support

If you encounter issues:

1. Check this setup guide
2. Verify your API key is valid at [AlphaVantage](https://www.alphavantage.co/)
3. Check the application logs
4. Review the main [README.md](README.md)

---

**🔐 Remember: Keep your API keys secure and never commit them to version control!** 