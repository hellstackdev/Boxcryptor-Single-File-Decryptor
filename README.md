# Boxcryptor Single File Decryptor

[![.NET CI](https://github.com/hellstackdev/boxcryptor-single-file-decryptor/actions/workflows/main.yml/badge.svg)](https://github.com/hellstackdev/boxcryptor-single-file-decryptor/actions/workflows/main.yml)
[![.NET CD - Release](https://github.com/hellstackdev/boxcryptor-single-file-decryptor/actions/workflows/release.yml/badge.svg)](https://github.com/hellstackdev/boxcryptor-single-file-decryptor/actions/workflows/release.yml)

This project provides a command-line utility for decrypting Boxcryptor-encrypted files. It's built with .NET 8.0 and requires the Microsoft.AspNetCore.Cryptography.KeyDerivation package.

## Requirements

- .NET 8.0 SDK or runtime
- Microsoft.AspNetCore.Cryptography.KeyDerivation NuGet package (automatically restored when building)

## Building the Project

### Using build scripts

The project includes convenient build scripts for both Linux/macOS and Windows:

**Linux/macOS:**
```bash
./build.sh
```

**Windows:**
```cmd
build.bat
```

These scripts will build the project, run tests, and optionally create standalone executables for multiple platforms.

### Manual build

To build the project manually, use the .NET CLI:

```bash
dotnet build
```

### Creating standalone executables

To create standalone executables that don't require .NET to be installed:

```bash
dotnet publish BCFileDecryptor/BCFileDecryptor.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish/win-x64
```

Replace `win-x64` with `linux-x64` or `osx-x64` for other platforms.

## Usage

```
BCFileDecryptor [path to .bckey file] [path to encrypted file] [pwd] [path for output (optional)]
```

Where:
- `[path to .bckey file]` is the path to your Boxcryptor key file
- `[path to encrypted file]` is the path to the encrypted file you want to decrypt
- `[pwd]` is your Boxcryptor password
- `[path for output (optional)]` is the output path for the decrypted file (if not provided, it will be derived from the encrypted file name)

## Testing

The project includes a comprehensive test suite built with xUnit. The tests cover all the helper classes used in the decryption process:

- `Base64Helper`: Tests for Base64 encoding and decoding operations
- `HashHelper`: Tests for the SHA256 HMAC hash computation
- `AESHelper`: Tests for AES decryption functionality
- `PBKDF2Helper`: Tests for key derivation using PBKDF2
- `RSAHelper`: Tests for RSA decryption
- `IntegrationTests`: Tests for the overall workflow (with skipped tests that require real files)

To run the tests, use the .NET CLI:

```bash
dotnet test
```

## Project Structure

- **BCFileDecryptor**: The main application containing the decryption logic
  - `BCFileDecryptor.cs`: Main entry point and decryption workflow
  - `AccountData.cs`: Handles parsing of the .bckey file
  - `AESHelper.cs`: Provides AES encryption/decryption functions
  - `Base64Helper.cs`: Handles Base64 encoding/decoding
  - `FileData.cs`: Parses encrypted file headers
  - `HashHelper.cs`: Computes SHA256 HMAC hashes
  - `PBKDF2Helper.cs`: Implements PBKDF2 key derivation
  - `RSAHelper.cs`: Handles RSA decryption

- **BCFileDecryptorTests**: Unit tests for the application
  - `Base64HelperTests.cs`: Tests for Base64 encoding/decoding (5 tests)
  - `HashHelperTests.cs`: Tests for HMAC functionality (5 tests)
  - `AESHelperTests.cs`: Tests for AES encryption/decryption (5 tests)
  - `PBKDF2HelperTests.cs`: Tests for key derivation (6 tests)
  - `RSAHelperTests.cs`: Tests for RSA decryption (3 tests)
  - `IntegrationTests.cs`: Tests for the overall workflow (3 tests, currently skipped)

## Test Coverage

The test suite includes 28 tests in total, covering:
- 25 passing unit tests for the helper classes
- 3 integration tests (currently skipped as they require real Boxcryptor files)

These tests ensure:
- Correct Base64 encoding and decoding
- Proper HMAC hash generation and verification
- Accurate AES decryption with various parameters
- Correct PBKDF2 key derivation
- Proper handling of RSA decryption
- Error handling for invalid inputs

## Continuous Integration/Deployment

This project uses GitHub Actions with separate workflows for CI and CD:

### CI Workflow (.NET CI)
- Automatically runs on each push and pull request
- Builds the project using .NET 8.0
- Runs all unit tests
- Ensures code quality across platforms

### CD Workflow (.NET CD - Release)
- Triggered manually or when a new release is created on GitHub
- Generates standalone executables for Windows, Linux, and macOS
- Creates zip archives of the builds
- Uploads build artifacts for easy downloading
- Attaches the binaries to GitHub releases (when triggered by a release)

This separation allows for:
- Fast feedback on code changes via the CI pipeline
- Controlled releases via the manual CD pipeline
- Clean, versioned releases with properly packaged artifacts

## Notes

- This tool only supports decryption of files encrypted with Boxcryptor.
- The application currently supports the bc01 file version.
- The decryption process requires the correct .bckey file and password for the encrypted files.
- The project has been updated from .NET Core 3.1 to .NET 8.0.
- Cross-platform builds are available through the CI/CD pipeline.
