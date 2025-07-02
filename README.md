# CredentialValidator

## What is it?

A C# .NET Web API project that is designed to validates user input.

## How to run:

1. `gh repo clone CharlieGo19/credential-validator-api`
2. `cd CredentialValidator`
3. `$env:ASPNETCORE_ENVIRONMENT="Development"` or `export ASPNETCORE_ENVIRONMENT=Development`
4. To run the application: `dotnet run`
5. To run tests (from the root): `dotnet test`

## Sample request/response

**POST `/validate`**

**Request JSON**
```json
{
  "username": "exampleUser",
  "password": "notapassword",
  "certificatestring": "CERTIFICATE_DATA_HERE"
}
```
**Response**
```json
{
    "IsValid": true,
    "Errors": []
}
```

## Notes

- Make sure you have .NET 8 SDK installed.
- Modify ports and settings in `launchSettings.json` or environment variables as needed.
- API will be available on: `https://localhost:7295` or `http://localhost:5155` (redirected to 7295)
- Swagger UI on `https://localhost:7295/swagger/index.html`

