# AGENTS.md

## Build Commands
- Build: Use Unity Editor or `Unity.exe -batchmode -quit -projectPath . -buildTarget StandaloneWindows64 -executeMethod BuildScript.Build`
- Test: Unity Test Framework via `Window > General > Test Runner` or run specific test with `Unity.exe -runTests -projectPath . -testPlatform EditMode -testResults results.xml -testCategory "TestSession"`
- Lint: Unity's built-in code analysis and warnings (WarningLevel 4 in csproj)

## Code Style Guidelines

### Imports & Namespaces
- Use `using` statements at top, sorted alphabetically
- Namespace structure: `Evalve.App.*`, `Evalve.Client.*`, `Evalve.Contracts.*`
- Avoid `using static` except for extension methods

### Formatting & Types
- C# 9.0 language version
- PascalCase for classes, methods, properties
- camelCase for private fields, local variables
- Use `var` for local type inference when type is obvious
- Prefer expression-bodied members for single-line methods

### Architecture Patterns
- MVP pattern: View (MonoBehaviour), Presenter, Model classes
- Dependency Injection with VContainer
- Command pattern for user actions
- State machine for application flow
- Async/await for network operations

### Error Handling
- Use exceptions for error conditions
- Log errors with custom `ILogger` interface
- Validate inputs in public methods
- Handle async exceptions properly

### Unity Specific
- `[SerializeField]` for private inspector fields
- Prefer `[Header("")]` for organizing inspector
- Use `MonoBehaviour` only for Unity lifecycle components
- Separate data models from Unity components

### Testing
- Test classes inherit from `MonoBehaviour` for Unity integration
- Use `[SerializeField]` for test dependencies
- Integration tests in `Assets/Evalve/App/Tests/`