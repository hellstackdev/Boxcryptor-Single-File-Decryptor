# GitHub Actions Workflows Guide

This document explains how to use the GitHub Actions workflows in this repository.

## CI Workflow (`main.yml`)

This workflow automatically runs on every push to the `main` or `master` branch and on pull requests to these branches.

### Jobs:

#### 1. Build Job
- Checks out the repository
- Sets up .NET 8.0
- Restores dependencies
- Builds the project in Release configuration
- Caches and uploads build artifacts for the test job

#### 2. Test Job
- Runs after the build job completes
- Downloads the build artifacts
- Runs all tests against the built assemblies

### How to use it:
- You don't need to do anything special to trigger this workflow; it runs automatically on pushes and pull requests.
- Check the status of the workflow runs in the "Actions" tab of your GitHub repository.

## CD Workflow (`release.yml`)

This workflow creates release artifacts for Windows, Linux, and macOS.

### Jobs:

#### 1. Build Release Job
- Checks out the repository
- Sets up .NET 8.0
- Restores dependencies and builds the project
- Creates self-contained executables for Windows, Linux, and macOS
- Uploads build outputs as artifacts

#### 2. Package Job
- Runs after the build-release job completes
- Downloads the build outputs
- Creates zip archives of the builds
- Uploads the zip archives as artifacts
- If triggered by a GitHub release, attaches the zip files to the release

### How to trigger it:
1. **Manual Trigger:**
   - Go to the "Actions" tab in your GitHub repository
   - Select the ".NET CD - Release" workflow
   - Click "Run workflow"
   - Select the branch you want to run it on (usually `main` or `master`)
   - Click "Run workflow"

2. **Via GitHub Release:**
   - Go to the "Releases" section in your GitHub repository
   - Click "Create a new release"
   - Choose a tag (e.g., `v1.0.0`)
   - Enter a release title and description
   - Click "Publish release"
   - The workflow will run automatically and attach the build artifacts to the release

### Accessing the artifacts:
1. Go to the "Actions" tab in your GitHub repository
2. Find the workflow run you're interested in
3. Scroll down to the "Artifacts" section
4. Click on the artifact you want to download (`BCFileDecryptor-windows`, `BCFileDecryptor-linux`, or `BCFileDecryptor-macos`)

If you've created a GitHub release, the artifacts will also be available for download in the "Assets" section of the release page.

## Best Practices

1. **Versioning:**
   - Use semantic versioning (e.g., `v1.0.0`, `v1.1.0`, `v2.0.0`)
   - Tag important commits to make them easy to track

2. **Release Notes:**
   - Provide clear, concise release notes with each GitHub release
   - Document new features, bug fixes, and breaking changes

3. **CI/CD:**
   - Always check that the CI workflow passes before merging pull requests
   - Create releases for stable, user-facing versions
   - Test release artifacts before announcing them to users
