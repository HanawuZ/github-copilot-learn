# Copilot Instructions: GitHub Security Workflow

## Overview
This repository uses GitHub Actions and CodeQL to scan for security vulnerabilities in C# code. The workflow file is located at `.github/workflows/github-security.yml`.

## How to Trigger a Scan
- Push or pull request to the `main` branch
- Weekly scheduled scan

## Troubleshooting Autobuild Errors
If you see an error like:

```
Error: Could not auto-detect a suitable build method.
```

Replace the `autobuild` step in your workflow with a custom build step:

```yaml
      # - name: Autobuild
      #   uses: github/codeql-action/autobuild@v3
      - name: Build with dotnet
        run: dotnet build HelloWorld.csproj --configuration Release
```

## Simulating Vulnerabilities
To test the security scan, add a C# file (for example, `MaliciousSample.cs`) with code that contains vulnerabilities such as:


Example:

```csharp
// MaliciousSample.cs
using System;
using System.Data.SqlClient;

namespace MaliciousSample
{
  class Program
  {
    static void Main(string[] args)
    {
      // Hardcoded password (security risk)
      string password = "SuperSecret123!";
      string username = "admin";

      // Unsafe SQL query (SQL injection risk)
      string userInput = "' OR '1'='1";
      string query = "SELECT * FROM Users WHERE username = '" + userInput + "' AND password = '" + password + "'";

      using (SqlConnection connection = new SqlConnection("Server=myServer;Database=myDB;User Id=" + username + ";Password=" + password + ";"))
      {
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          Console.WriteLine(reader["username"]);
        }
      }
    }
  }
}
### Python Example

To test the security scan with Python, add a `.py` file containing insecure code, such as hardcoded credentials or unsafe SQL queries.

Example:

```python
# malicious_sample.py
import sqlite3

def insecure_query(user_input):
    # Hardcoded password (security risk)
    password = "SuperSecret123!"
    username = "admin"

    # Unsafe SQL query (SQL injection risk)
    query = f"SELECT * FROM users WHERE username = '{user_input}' AND password = '{password}'"

    conn = sqlite3.connect('example.db')
    cursor = conn.cursor()
    cursor.execute(query)
    for row in cursor.fetchall():
        print(row)
    conn.close()
```
```

## References
- [GitHub CodeQL Documentation](https://docs.github.com/en/code-security/code-scanning/automatically-scanning-code-for-vulnerabilities-and-errors-with-codeql)
- [.NET Security Best Practices](https://learn.microsoft.com/en-us/dotnet/standard/security/)

---
For further help, contact your repository administrator or check the GitHub Actions logs for details.