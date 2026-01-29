# SpendfulnessCli: A Deep Dive into Clean Architecture and SOLID Principles

**Duration:** 45 minutes  
**Presenter:** [Your Name]  
**Repository:** KitCli/SpendfulnessCli

---

## Table of Contents
1. [Introduction & Overview](#1-introduction--overview) (5 minutes)
2. [Architecture Deep Dive](#2-architecture-deep-dive) (10 minutes)
3. [Composition Over Inheritance](#3-composition-over-inheritance) (5 minutes)
4. [SOLID Principles in Practice](#4-solid-principles-in-practice) (10 minutes)
5. [DRY: Don't Repeat Yourself](#5-dry-dont-repeat-yourself) (5 minutes)
6. [YAGNI: You Aren't Gonna Need It](#6-yagni-you-arent-gonna-need-it) (5 minutes)
7. [Additional Beneficial Talking Points](#7-additional-beneficial-talking-points) (3 minutes)
8. [Q&A](#8-qa) (2 minutes)

---

## 1. Introduction & Overview
**Duration: 5 minutes**

### What is SpendfulnessCli?

SpendfulnessCli is a **terminal-based financial management CLI** built in C# that integrates with YNAB (You Need A Budget). It's a real-world example of how to build a maintainable, extensible command-line application using modern software engineering principles.

### Project Stats
- **41 C# projects** organized in a modular architecture
- **~11,700 lines of code** 
- **13 Architecture Decision Records** documenting key design choices
- **Comprehensive test coverage** across multiple test projects

### Core Functionality
```bash
# Example Commands
/database create
/user create --user-name Joshua
/settings create --name YnabApiKey --value <your-key>
/spare-money --minus-savings true
```

### Why This Project Matters

This isn't just a CLI tool—it's a **masterclass in software architecture**:
- Clean separation of concerns
- Extensible plugin-based architecture
- Type-safe command parsing and execution
- Comprehensive documentation through ADRs
- Real-world application of SOLID, DRY, and YAGNI

---

## 2. Architecture Deep Dive
**Duration: 10 minutes**

### High-Level Architecture

```
┌─────────────────────────────────────────────────────┐
│                  CLI Application                     │
│  (CliApp - Main Loop & User Interaction)            │
└────────────────────┬────────────────────────────────┘
                     │
         ┌───────────▼──────────────┐
         │   CLI Workflow           │
         │   (Session Management)   │
         └───────────┬──────────────┘
                     │
         ┌───────────▼──────────────┐
         │  CLI Workflow Run        │
         │  (Command Execution)     │
         └───────────┬──────────────┘
                     │
    ┌────────────────┼────────────────┐
    │                │                │
    ▼                ▼                ▼
┌─────────┐   ┌──────────┐   ┌───────────┐
│ Parser  │   │ Command  │   │  MediatR  │
│         │   │ Provider │   │           │
└─────────┘   └──────────┘   └───────────┘
```

### The Three-Tier State Machine Architecture

#### Tier 1: CliApp (Session Loop)
**Responsibility:** User interaction and session lifecycle

```csharp
public abstract class CliApp
{
    public async Task Run()
    { 
        OnSessionStart();
        
        while (_workflow.Status != CliWorkflowStatus.Stopped)
        {
            var run = _workflow.NextRun();
            OnRunCreated(run);
            
            var ask = Io.Ask();              // Get user input
            var runTask = run.RespondToAsk(ask);  // Start execution
            OnRunStarted(run, ask);
            
            var outcomes = await runTask;    // Wait for completion
            Io.Say(outcomes);                // Display results
            OnRunComplete(run, outcomes);
        }
        
        OnSessionEnd(_workflow.Runs);
    }
}
```

**Key Design Patterns:**
- **Template Method Pattern:** Base class with lifecycle hooks
- **REPL Pattern:** Read-Eval-Print-Loop for interactive sessions
- **Inversion of Control:** I/O abstraction for testability

#### Tier 2: CliWorkflow (Session State Machine)
**Responsibility:** Session-level state management

**States:** `Started` → `Stopped`

**Key Responsibilities:**
- Create and track workflow runs
- Resolve dependencies from DI container
- Control session lifecycle
- Maintain run history for debugging

```csharp
public class CliWorkflow
{
    private List<CliWorkflowRun> _runs = [];
    public CliWorkflowStatus Status = CliWorkflowStatus.Started;

    public CliWorkflowRun NextRun()
    {
        var state = new CliWorkflowRunState();
        var parser = _serviceProvider.GetRequiredService<CliInstructionParser>();
        var commandProvider = _serviceProvider.GetRequiredService<CliWorkflowCommandProvider>();
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        
        var run = new CliWorkflowRun(state, parser, commandProvider, mediator);
        _runs.Add(run);
        return run;
    }
}
```

#### Tier 3: CliWorkflowRun (Command State Machine)
**Responsibility:** Individual command execution

**States:** 
```
NotInitialized → Created → Running → {InvalidAsk | Exceptional} → Finished
                                  ↘ Finished
```

**Execution Pipeline:**
1. **Validate** input (not null/empty)
2. **Parse** text into structured instruction
3. **Route** to appropriate command generator
4. **Execute** via MediatR
5. **Handle** errors gracefully
6. **Record** state transitions with timing

```csharp
public async Task<CliCommandOutcome[]> RespondToAsk(string? ask)
{
    _state.ChangeTo(ClIWorkflowRunStateType.Created);
    
    if (!IsValidAsk(ask))
    {
        _state.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
        return [new CliCommandNothingOutcome()];
    }

    try
    {
        _state.ChangeTo(ClIWorkflowRunStateType.Running);
        
        var instruction = _parser.Parse(ask!);
        var command = _commandProvider.GetCommand(instruction);
        return await _mediator.Send(command);
    }
    catch (Exception exception)
    {
        _state.ChangeTo(ClIWorkflowRunStateType.Exceptional);
        return [new CliCommandExceptionOutcome(exception)];
    }
    finally
    {
        _state.ChangeTo(ClIWorkflowRunStateType.Finished);
    }
}
```

### The Instruction Parser: Three-Stage Pipeline

**Problem:** Converting user text into strongly-typed commands

**Solution:** Three-stage pipeline with plugin-based type system

#### Stage 1: Token Indexing
**Responsibility:** Identify positions of tokens in raw input

```
Input:  "/spare-money --minus-savings true"
Output: PrefixIndex=0, NameIndex=1-12, ArgIndex=13...
```

#### Stage 2: Token Extraction
**Responsibility:** Extract string values from positions

```
Output: {
    Prefix: "/",
    Name: "spare-money",
    Arguments: { "minus-savings": "true" }
}
```

#### Stage 3: Argument Building
**Responsibility:** Convert strings to typed arguments

**Builder Pattern for Type Detection:**
```csharp
public interface IConsoleInstructionArgumentBuilder
{
    bool For(string? argumentValue);  // Can handle this value?
    ConsoleInstructionArgument Create(string name, string? value);
}
```

**Builders:**
- `IntConsoleInstructionArgumentBuilder`
- `DecimalConsoleInstructionArgumentBuilder`
- `DateOnlyConsoleInstructionArgumentBuilder`
- `GuidConsoleInstructionArgumentBuilder`
- `StringConsoleInstructionArgumentBuilder`
- `BoolConsoleInstructionArgumentBuilder` (fallback, must register last)

**Result:**
```csharp
public record ConsoleInstruction(
    string? Prefix,
    string? Name,
    string? SubName,
    IEnumerable<ConsoleInstructionArgument> Arguments
);
```

### Command Execution via MediatR

**Pattern:** CQRS (Command Query Responsibility Segregation)

```csharp
// Command Definition
public record FilterTransactionsOnPayeeNameEqualsCliCommand(
    string PayeeName
) : CliCommand;

// Handler Implementation
public class FilterTransactionsOnPayeeNameEqualsCliCommandHandler 
    : IRequestHandler<FilterTransactionsOnPayeeNameEqualsCliCommand, CliCommandOutcome[]>
{
    public async Task<CliCommandOutcome[]> Handle(
        FilterTransactionsOnPayeeNameEqualsCliCommand command, 
        CancellationToken cancellationToken)
    {
        var filter = new EqualsValuedAppliedFilter<Transaction, string>(
            transaction => transaction.PayeeName,
            command.PayeeName
        );
        
        return OutcomeAs(filter);
    }
}
```

**Benefits:**
- ✅ Decoupled command routing from execution
- ✅ Easy to add pipeline behaviors (logging, validation)
- ✅ Handlers can be in separate assemblies
- ✅ Simple to test in isolation

---

## 3. Composition Over Inheritance
**Duration: 5 minutes**

### The Problem with Deep Inheritance Hierarchies

```csharp
// ❌ BAD: Deep inheritance = rigid, hard to change
public abstract class BaseCommand { }
public abstract class DataCommand : BaseCommand { }
public abstract class TransactionCommand : DataCommand { }
public class SpecificTransactionCommand : TransactionCommand { }
```

### SpendfulnessCli's Approach: Composition

#### 1. Command Composition via Records

```csharp
// ✅ GOOD: Flat hierarchy with composition
public record CliCommand : IRequest<CliCommandOutcome[]>;

public record FilterTransactionsCliCommand(
    List<CliCommandProperty> Properties  // Composed behavior
) : CliCommand;

public record FilterTransactionsOnPayeeNameEqualsCliCommand(
    string PayeeName
) : CliCommand;
```

**Key Points:**
- Commands are **simple data carriers** (records)
- Behavior composed through **properties and handlers**
- No deep inheritance chains

#### 2. Aggregator Composition

```csharp
// Base aggregator provides infrastructure
public abstract class CliAggregator<TAggregate>
{
    protected List<Func<CliAggregator<TAggregate>, CliAggregator<TAggregate>>> 
        _beforeAggregationOperations = [];
    
    public CliAggregator<TAggregate> BeforeAggregation(
        Func<CliAggregator<TAggregate>, CliAggregator<TAggregate>> operation)
    {
        _beforeAggregationOperations.Add(operation);
        return this;
    }
}

// Concrete aggregator focused on specific logic
public class CategoryDeductedAmountAggregator : CliAggregator<decimal>
{
    // Only implements core aggregation logic
    protected override decimal OnAggregate() 
    {
        // Specific calculation
    }
}

// Usage: Compose behavior fluently
var aggregator = new CategoryDeductedAmountAggregator(accounts, categoryGroups)
    .BeforeAggregation(a => a.FilterToTypes(AccountType.Checking))
    .BeforeAggregation(a => a.FilterToCriticalCategories());
```

**Benefits:**
- ✅ Flexible behavior composition
- ✅ Operations can be mixed and matched
- ✅ Each class has single responsibility
- ✅ Easy to test individual operations

#### 3. Factory Pattern for Extensibility

```csharp
// Plugin-based command generation
public interface ICliCommandPropertyFactory
{
    bool CanCreateProperty(CliCommandOutcome outcome);
    CliCommandProperty CreateProperty(CliCommandOutcome outcome);
}

// Register multiple implementations
services.AddSingleton<ICliCommandPropertyFactory, MessageCliCommandPropertyFactory>();
services.AddSingleton<ICliCommandPropertyFactory, AggregatorCliCommandPropertyFactory<T>>();
```

**Benefits:**
- ✅ Open/Closed Principle: Add new factories without modifying existing code
- ✅ Runtime polymorphism through interfaces
- ✅ Dependency injection handles composition

### Composition Wins in SpendfulnessCli

1. **Outcome System:** Multiple outcome types composed from base `CliCommandOutcome`
2. **Filter System:** Filters compose through `BeforeAggregation`/`AfterAggregation`
3. **Property System:** Properties dynamically composed from outcomes
4. **I/O Abstraction:** Different I/O implementations without changing core logic

---

## 4. SOLID Principles in Practice
**Duration: 10 minutes**

### S - Single Responsibility Principle (SRP)

**Definition:** A class should have one, and only one, reason to change.

#### Example 1: Three-Tier State Machine Separation

```csharp
// ✅ CliApp: Responsible ONLY for user interaction loop
public abstract class CliApp
{
    public async Task Run()
    {
        while (_workflow.Status != CliWorkflowStatus.Stopped)
        {
            var ask = Io.Ask();      // User interaction
            var outcomes = await run.RespondToAsk(ask);
            Io.Say(outcomes);        // User interaction
        }
    }
}

// ✅ CliWorkflow: Responsible ONLY for session management
public class CliWorkflow
{
    public CliWorkflowRun NextRun() { /* Create runs */ }
    public void Stop() { Status = CliWorkflowStatus.Stopped; }
}

// ✅ CliWorkflowRun: Responsible ONLY for command execution
public class CliWorkflowRun
{
    public async Task<CliCommandOutcome[]> RespondToAsk(string? ask)
    {
        // Parse → Route → Execute → Return
    }
}
```

**Why It Matters:**
- ✅ User interaction changes? Modify `CliApp`
- ✅ Session logic changes? Modify `CliWorkflow`
- ✅ Command execution changes? Modify `CliWorkflowRun`
- ✅ Each class has exactly one reason to change

#### Example 2: Instruction Parser Separation

```csharp
// ✅ ConsoleInstructionTokenIndexer: Only finds token positions
public class ConsoleInstructionTokenIndexer
{
    public ConsoleInstructionTokenIndexes Index(string input) { }
}

// ✅ ConsoleInstructionTokenExtractor: Only extracts token strings
public class ConsoleInstructionTokenExtractor
{
    public ConsoleInstructionTokenExtraction Extract(string input, indexes) { }
}

// ✅ ConsoleInstructionParser: Only orchestrates the pipeline
public class ConsoleInstructionParser
{
    public ConsoleInstruction Parse(string input)
    {
        var indexes = _indexer.Index(input);
        var extraction = _extractor.Extract(input, indexes);
        return BuildInstruction(extraction);
    }
}

// ✅ IConsoleInstructionArgumentBuilder: Only converts one type
public class IntConsoleInstructionArgumentBuilder
{
    public bool For(string? value) => int.TryParse(value, out _);
    public ConsoleInstructionArgument Create(string name, string? value) { }
}
```

**SRP Benefits in SpendfulnessCli:**
- Each component is easy to understand
- Changes are localized
- Testing is straightforward
- Code is more maintainable

---

### O - Open/Closed Principle (OCP)

**Definition:** Software entities should be open for extension but closed for modification.

#### Example 1: Command Registration via DI

```csharp
// ✅ OPEN for extension: Add new commands without modifying core
services.AddKeyedTransient<ICliCommandGenerator>(
    "filter-transactions-on-payee-name-equals",
    (sp, key) => new FilterTransactionsOnPayeeNameEqualsCliCommandGenerator()
);

// Core routing logic NEVER changes:
public CliCommand GetCommand(CliInstruction instruction)
{
    var generator = _serviceProvider
        .GetKeyedService<ICliCommandGenerator>(instruction.Name);
    return generator.Generate(instruction);
}
```

**Result:**
- ✅ Add 100 new commands → Zero changes to `CliWorkflowCommandProvider`
- ✅ Core system is **closed for modification**
- ✅ New functionality through **extension only**

#### Example 2: Argument Type Builders

```csharp
// ✅ Add new type support by implementing interface
public class GuidConsoleInstructionArgumentBuilder 
    : IConsoleInstructionArgumentBuilder
{
    public bool For(string? value) => Guid.TryParse(value, out _);
    public ConsoleInstructionArgument Create(string name, string? value)
    {
        return new TypedConsoleInstructionArgument<Guid>(
            name, 
            Guid.Parse(value!)
        );
    }
}

// Register in DI
services.AddSingleton<IConsoleInstructionArgumentBuilder, 
                      GuidConsoleInstructionArgumentBuilder>();
```

**Result:**
- ✅ Parser automatically uses new builder
- ✅ No changes to parsing logic
- ✅ Extensible type system

#### Example 3: Outcome Factories

```csharp
// ✅ Add new property types through factory pattern
public class AggregatorCliCommandPropertyFactory<TAggregate> 
    : ICliCommandPropertyFactory
{
    public bool CanCreateProperty(CliCommandOutcome outcome)
        => outcome is CliCommandAggregatorOutcome<TAggregate>;
    
    public CliCommandProperty CreateProperty(CliCommandOutcome outcome)
    {
        var aggregatorOutcome = (CliCommandAggregatorOutcome<TAggregate>)outcome;
        return new AggregatorCliCommandProperty<TAggregate>(
            aggregatorOutcome.Aggregator
        );
    }
}
```

**Result:**
- ✅ Add new outcome → property conversions without touching core
- ✅ `CliWorkflowCommandProvider` remains unchanged
- ✅ New factories automatically discovered via DI

---

### L - Liskov Substitution Principle (LSP)

**Definition:** Derived classes must be substitutable for their base classes.

#### Example 1: CliCommand Hierarchy

```csharp
// Base command contract
public record CliCommand : IRequest<CliCommandOutcome[]>
{
    public string GetInstructionName() { /* ... */ }
}

// All derived commands are fully substitutable
public record FilterTransactionsCliCommand(...) : CliCommand;
public record ExportCsvCliCommand(...) : CliCommand;
public record ExitCliCommand() : CliCommand;

// MediatR works with ANY command - perfect substitutability
var outcome = await _mediator.Send(command);  // Works for all commands
```

**Why LSP Holds:**
- ✅ All commands implement same interface
- ✅ All return `CliCommandOutcome[]`
- ✅ No unexpected behavior changes
- ✅ Base class assumptions never violated

#### Example 2: Aggregator Substitutability

```csharp
// Base aggregator defines contract
public abstract class CliAggregator<TAggregate>
{
    public TAggregate Aggregate() { /* Template method */ }
    protected abstract TAggregate OnAggregate();
}

// All implementations honor the contract
public class TransactionAverageAcrossYearAggregator 
    : CliListAggregator<TransactionYearAverageAggregate> { }

public class CategoryDeductedAmountAggregator 
    : CliAggregator<decimal> { }

// Can be used polymorphically
public void ProcessAggregator<T>(CliAggregator<T> aggregator)
{
    var result = aggregator.Aggregate();  // Works for all aggregators
}
```

#### Example 3: I/O Abstraction

```csharp
// Interface defines contract
public interface ICliCommandOutcomeIo
{
    string? Ask();
    void Say(CliCommandOutcome[] outcomes);
}

// All implementations are substitutable
public class ConsoleCliCommandOutcomeIo : ICliCommandOutcomeIo
{
    public string? Ask() => Console.ReadLine();
    public void Say(CliCommandOutcome[] outcomes) { /* Display */ }
}

public class TestCliCommandOutcomeIo : ICliCommandOutcomeIo
{
    public string? Ask() => _testInput.Dequeue();
    public void Say(CliCommandOutcome[] outcomes) => _testOutput.Add(outcomes);
}

// CliApp works with ANY implementation
protected CliApp(ICliWorkflow workflow, ICliCommandOutcomeIo io)
{
    _workflow = workflow;
    Io = io;  // Perfect substitutability
}
```

**LSP Benefits:**
- ✅ Easy to test with mock implementations
- ✅ Can switch implementations at runtime
- ✅ No surprises or unexpected behaviors

---

### I - Interface Segregation Principle (ISP)

**Definition:** Clients should not be forced to depend on interfaces they don't use.

#### Example 1: Focused Interfaces

```csharp
// ✅ GOOD: Small, focused interfaces
public interface ICliCommandGenerator
{
    CliCommand Generate(CliInstruction instruction);
}

public interface ICliCommandPropertyFactory
{
    bool CanCreateProperty(CliCommandOutcome outcome);
    CliCommandProperty CreateProperty(CliCommandOutcome outcome);
}

public interface ICliCommandHandler<TCommand>
{
    Task<CliCommandOutcome[]> Handle(TCommand command, CancellationToken ct);
}

// ❌ BAD: Would violate ISP
public interface ICliCommandEverything
{
    CliCommand Generate(CliInstruction instruction);
    bool CanCreateProperty(CliCommandOutcome outcome);
    CliCommandProperty CreateProperty(CliCommandOutcome outcome);
    Task<CliCommandOutcome[]> Handle(CliCommand command);
    void Validate(CliCommand command);
    // ... many more methods
}
```

**Why Focused Interfaces Win:**
- ✅ Implement only what you need
- ✅ Easier to understand
- ✅ Simpler to test
- ✅ More flexible composition

#### Example 2: Command Handler Interfaces

```csharp
// ✅ Handlers only implement what they need
public class ExitCliCommandHandler 
    : IRequestHandler<ExitCliCommand, CliCommandOutcome[]>
{
    // Only implements Handle method
    public Task<CliCommandOutcome[]> Handle(
        ExitCliCommand command, 
        CancellationToken ct)
    {
        _workflow.Stop();
        return AsyncOutcomeAs("Exiting...");
    }
}
```

**No Unnecessary Methods:**
- Doesn't need to implement parsing
- Doesn't need to implement generation
- Doesn't need to implement validation
- **Only handles the command**

---

### D - Dependency Inversion Principle (DIP)

**Definition:** High-level modules should not depend on low-level modules. Both should depend on abstractions.

#### Example 1: CliApp Depends on Abstractions

```csharp
// ✅ GOOD: Depends on abstractions
public abstract class CliApp
{
    private readonly ICliWorkflow _workflow;         // Abstraction
    protected readonly ICliCommandOutcomeIo Io;       // Abstraction
    
    protected CliApp(ICliWorkflow workflow, ICliCommandOutcomeIo io)
    {
        _workflow = workflow;
        Io = io;
    }
}

// ❌ BAD: Would violate DIP
public abstract class CliApp
{
    private readonly ConsoleWorkflow _workflow;       // Concrete class
    protected readonly ConsoleIo Io;                  // Concrete class
}
```

**Benefits of Abstractions:**
- ✅ Can inject different implementations
- ✅ Easy to test with mocks
- ✅ Low coupling between components
- ✅ High-level logic stays stable

#### Example 2: Workflow Depends on Abstractions

```csharp
// ✅ Workflow depends on abstractions
public class CliWorkflowRun
{
    private readonly ICliInstructionParser _parser;           // Abstraction
    private readonly ICliWorkflowCommandProvider _provider;   // Abstraction
    private readonly IMediator _mediator;                     // Abstraction
    
    public async Task<CliCommandOutcome[]> RespondToAsk(string? ask)
    {
        var instruction = _parser.Parse(ask);            // Through interface
        var command = _provider.GetCommand(instruction); // Through interface
        return await _mediator.Send(command);            // Through interface
    }
}
```

**Dependency Flow:**
```
High-Level (CliWorkflowRun)
        ↓ depends on ↓
    Abstractions (Interfaces)
        ↑ implemented by ↑
Low-Level (Concrete implementations)
```

#### Example 3: Dependency Injection Throughout

```csharp
// All dependencies injected through DI container
services.AddSingleton<ICliInstructionParser, ConsoleInstructionParser>();
services.AddSingleton<ICliWorkflowCommandProvider, CliWorkflowCommandProvider>();
services.AddSingleton<ICliCommandOutcomeIo, ConsoleCliCommandOutcomeIo>();

// Concrete classes instantiated by DI, injected as abstractions
var app = serviceProvider.GetRequiredService<SpendfulnessCliApp>();
await app.Run();
```

**DIP Benefits:**
- ✅ Loose coupling between modules
- ✅ Easy to swap implementations
- ✅ Testability through mocking
- ✅ Stable high-level policies

---

## 5. DRY: Don't Repeat Yourself
**Duration: 5 minutes**

**Definition:** Every piece of knowledge should have a single, unambiguous representation in the system.

### DRY in SpendfulnessCli

#### 1. Aggregator Pattern for Data Transformation

**Problem:** Same filtering/aggregation logic repeated across commands

```csharp
// ❌ BAD: Repeated logic
public class Command1Handler
{
    public async Task<Outcome> Handle(Command1 cmd)
    {
        var filteredTransactions = transactions
            .Where(t => t.Date >= startDate)
            .Where(t => t.Amount > 0)
            .GroupBy(t => t.Category);
        // ... more logic
    }
}

public class Command2Handler
{
    public async Task<Outcome> Handle(Command2 cmd)
    {
        var filteredTransactions = transactions  // DUPLICATE!
            .Where(t => t.Date >= startDate)
            .Where(t => t.Amount > 0)
            .GroupBy(t => t.Category);
        // ... more logic
    }
}
```

**✅ Solution: Reusable Aggregators**

```csharp
// Single source of truth for aggregation logic
public class TransactionMonthTotalAggregator : CliListAggregator<MonthTotalAggregate>
{
    protected override List<MonthTotalAggregate> OnAggregate()
    {
        return Transactions
            .GroupBy(t => new { t.Date.Year, t.Date.Month })
            .Select(g => new MonthTotalAggregate(
                Year: g.Key.Year,
                Month: g.Key.Month,
                Total: g.Sum(t => t.Amount)
            ))
            .ToList();
    }
}

// Reused across multiple commands
var aggregator = new TransactionMonthTotalAggregator(transactions)
    .BeforeAggregation(a => a.FilterToDateRange(start, end))
    .AfterAggregation(a => a.OrderByYear());

var results = aggregator.Aggregate();
```

**DRY Benefits:**
- ✅ Logic defined once, used everywhere
- ✅ Bug fixes propagate automatically
- ✅ Consistent behavior across commands
- ✅ Easier to test centralized logic

#### 2. Command Handler Base Class

```csharp
// ✅ Common outcome creation logic in base class
public abstract class CliCommandHandler
{
    // Single place for outcome creation patterns
    protected static CliCommandOutcome[] OutcomeAs()
        => [new CliCommandNothingOutcome()];
    
    protected static CliCommandOutcome[] OutcomeAs(CliTable table)
        => [new CliCommandTableOutcome(table)];
    
    protected static CliCommandOutcome[] OutcomeAs(string message)
        => [new CliCommandOutputOutcome(message)];
    
    protected static Task<CliCommandOutcome[]> AsyncOutcomeAs(string message)
        => Task.FromResult(OutcomeAs(message));
}

// All handlers inherit common patterns
public class MyCommandHandler : CliCommandHandler
{
    public async Task<CliCommandOutcome[]> Handle(MyCommand cmd)
    {
        return AsyncOutcomeAs("Success!");  // No duplicate outcome creation
    }
}
```

#### 3. Extension Methods for Common Operations

```csharp
// ✅ String manipulation logic centralized
public static class StringExtensions
{
    public static string ReplaceCommandSuffix(this string commandName)
        => commandName.Replace("CliCommand", string.Empty);
    
    public static string ToLowerSplitString(this string input, char separator)
        => string.Join(separator, SplitOnUpperCase(input)).ToLower();
}

// Used consistently across all commands
public record CliCommand
{
    public string GetInstructionName()
        => GetType().Name
            .ReplaceCommandSuffix()            // Reused logic
            .ToLowerSplitString('-');          // Reused logic
}
```

#### 4. Service Collection Extensions

```csharp
// ✅ Registration patterns encapsulated
public static class CommandServiceCollectionExtensions
{
    public static IServiceCollection AddCommandArtefacts(
        this IServiceCollection services)
    {
        return services
            .AddSingleton<ICliCommandArtefactFactory, PageNumberCliCommandArtefactFactory>()
            .AddSingleton<ICliCommandArtefactFactory, PageSizeCliCommandArtefactFactory>()
            .AddSingleton<ICliCommandArtefactFactory, RanCliCommandArtefactFactory>();
    }
}

// Single call instead of repeating registrations everywhere
services.AddCommandArtefacts();
```

#### 5. Constants for Configuration

```csharp
// ✅ Single source of truth for parsing rules
public static class ConsoleInstructionConstants
{
    public const char DefaultCommandNameSeparator = '-';
    public const string DefaultNamePrefix = "/";
    public const string DefaultArgumentPrefix = "--";
    public const char DefaultSpaceCharacter = ' ';
}

// Used throughout parser - change once, affects everywhere
if (input.StartsWith(ConsoleInstructionConstants.DefaultNamePrefix))
{
    // ... parsing logic
}
```

### Key DRY Patterns

1. **Aggregators** → Reusable data transformation
2. **Base Classes** → Common functionality inheritance
3. **Extension Methods** → Shared utilities
4. **Factory Pattern** → Consistent object creation
5. **Constants** → Configuration centralization
6. **DI Extensions** → Service registration patterns

### When NOT to DRY

```csharp
// ⚠️ Sometimes duplication is OK if abstractions would be forced

// These two methods look similar but serve different purposes
public class Indexer
{
    private int FindPrefixIndex(string input) { }  // Prefix-specific logic
    private int FindNameIndex(string input) { }    // Name-specific logic
}

// Forcing them into one method would create artificial coupling
// DON'T abstract just because code looks similar
```

**Rule of Three:** Wait until you have three instances before abstracting

---

## 6. YAGNI: You Aren't Gonna Need It
**Duration: 5 minutes**

**Definition:** Don't implement features until they are actually needed.

### YAGNI in SpendfulnessCli

#### 1. Simple Session State Machine

```csharp
// ✅ GOOD: Just two states - all that's needed
public enum CliWorkflowStatus
{
    Started,
    Stopped
}

// ❌ BAD: Would violate YAGNI
public enum CliWorkflowStatus
{
    Started,
    Paused,        // Not needed yet
    Suspended,     // Not needed yet
    Hibernating,   // Not needed yet
    Resuming,      // Not needed yet
    Stopped
}
```

**Why Simple Wins:**
- ✅ No unused states to maintain
- ✅ No complex state transition logic for unused features
- ✅ Easy to understand
- ✅ Can add more states later if actually needed

#### 2. Focused I/O Interface

```csharp
// ✅ GOOD: Only two methods needed
public interface ICliCommandOutcomeIo
{
    string? Ask();                          // Get input
    void Say(CliCommandOutcome[] outcomes); // Show output
}

// ❌ BAD: Would violate YAGNI
public interface ICliCommandOutcomeIo
{
    string? Ask();
    void Say(CliCommandOutcome[] outcomes);
    void SayWithColor(string message, ConsoleColor color);  // Not needed
    void SayWithAnimation(string message);                   // Not needed
    void PlaySound(string soundFile);                        // Not needed
    void ShowProgressBar(int percentage);                    // Not needed
}
```

**Benefits:**
- ✅ Simpler implementations
- ✅ Easier to test
- ✅ Less coupling to console-specific features
- ✅ Add features when actually needed

#### 3. Minimal Outcome Types

**Start Simple:**
```csharp
// Initial implementation - just what's needed
public abstract class CliCommandOutcome { }
public class CliCommandNothingOutcome : CliCommandOutcome { }
public class CliCommandOutputOutcome(string output) : CliCommandOutcome { }
public class CliCommandTableOutcome(CliTable table) : CliCommandOutcome { }
```

**Add When Needed:**
```csharp
// Added later when filtering feature required it
public class FilterCliCommandOutcome(CliListAggregatorFilter filter) 
    : CliCommandOutcome { }

// Added when aggregation feature required it
public class CliCommandAggregatorOutcome<TAggregate>(CliAggregator<TAggregate> aggregator) 
    : CliCommandOutcome { }
```

**YAGNI Process:**
1. Start with minimal set of outcomes
2. Wait for actual need
3. Add new outcome type
4. No speculative "might need this" outcomes

#### 4. No Premature Optimization

**From ADR09 - Stopwatch Logic:**
```csharp
// Simple timing - no complex performance monitoring
private readonly Stopwatch _stopwatch = new Stopwatch();

public void ChangeTo(ClIWorkflowRunStateType stateTypeToChangeTo)
{
    var currentState = CanChangeTo(stateTypeToChangeTo);
    UpdateStopwatch(stateTypeToChangeTo);
    
    var stateChange = new RecordedCliWorkflowRunStateChange(
        _stopwatch.ElapsedTicks,  // Simple timing
        currentState, 
        stateTypeToChangeTo
    );
}
```

**No Premature Features:**
- ❌ No distributed tracing (not needed yet)
- ❌ No performance metrics database (not needed yet)
- ❌ No monitoring dashboards (not needed yet)
- ✅ Just simple stopwatch timing (what's actually needed)

#### 5. Constraint Documentation

**From ADR09:**
> "**Run History Memory:** The workflow keeps all runs in memory (`_runs` list). For long-running sessions with many commands, this could consume significant memory. Currently not exposed or used beyond creation."

**YAGNI Decision:**
- Could implement: Persistence, cleanup, pruning
- Actual decision: Leave it simple until it's a problem
- Document the constraint for future reference

#### 6. Minimal Dependency Registration

```csharp
// ✅ GOOD: Register only what's actually used
public static class CliServiceCollectionExtensions
{
    public static IServiceCollection AddCliApp(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICliInstructionParser, ConsoleInstructionParser>()
            .AddSingleton<ICliWorkflow, CliWorkflow>()
            .AddSingleton<ICliCommandOutcomeIo, ConsoleCliCommandOutcomeIo>();
    }
}

// ❌ BAD: Would violate YAGNI
public static IServiceCollection AddCliApp(this IServiceCollection services)
{
    return services
        .AddSingleton<ICliInstructionParser, ConsoleInstructionParser>()
        .AddSingleton<IAdvancedCliInstructionParser, AdvancedParser>()  // Not needed
        .AddSingleton<ICliInstructionValidator, InstructionValidator>()  // Not needed
        .AddSingleton<ICliInstructionCache, InstructionCache>()          // Not needed
        // ... registering for hypothetical future needs
}
```

### Evidence from ADRs

**ADR02 - Database Choice:**
> "In order to avoid something too wide for what I'm ultimately trying to achieve here (unless in the future I want to provide logins... one step at a time), I chose SQL Lite to keep the database local to the user."

**YAGNI Decision:**
- ✅ SQLite - simple, fits current needs
- ❌ Not PostgreSQL/SQL Server - would be over-engineering
- ❌ Not multi-user auth system - not needed yet
- **"One step at a time"** - perfect YAGNI mindset

### YAGNI Benefits

1. **Faster Development:** Build only what's needed
2. **Less Code to Maintain:** No unused features to debug
3. **Simpler Architecture:** Easier to understand
4. **Easy to Extend:** Add features when actually needed
5. **Lower Risk:** Don't build features based on speculation

### When to Break YAGNI

**Acceptable exceptions:**
- Infrastructure that's expensive to add later (e.g., logging hooks)
- Security features (better safe than sorry)
- Abstractions that enable testing
- Features required by architectural patterns (e.g., lifecycle hooks)

### YAGNI != Poor Design

```csharp
// ✅ YAGNI-compliant BUT extensible
public abstract class CliApp
{
    // Lifecycle hooks make extension easy
    protected virtual void OnSessionStart() { }
    protected virtual void OnRunCreated(ICliWorkflowRun run) { }
    protected virtual void OnRunStarted(ICliWorkflowRun run, string? ask) { }
    protected virtual void OnRunComplete(ICliWorkflowRun run, CliCommandOutcome[] outcomes) { }
    protected virtual void OnSessionEnd(List<ICliWorkflowRun> runs) { }
}
```

**Not Violating YAGNI Because:**
- Hooks are empty (zero cost if unused)
- Enable extension without modification (OCP)
- Documented use cases in ADR
- Minimal implementation overhead

---

## 7. Additional Beneficial Talking Points
**Duration: 3 minutes**

### 1. Architecture Decision Records (ADRs)

**What are ADRs?**
- Lightweight architectural documentation
- Captures **why** decisions were made, not just **what**
- Template: Context → Problem → Solution → Constraints → Q&A

**SpendfulnessCli's ADR Approach:**
- **13 ADRs** documenting key architectural decisions
- Examples:
  - ADR07: Instruction Parser design
  - ADR08: CLI Concept and lifecycle
  - ADR09: Workflow state machine
  - ADR10: Command properties pattern

**Benefits:**
- ✅ Future developers understand reasoning
- ✅ Prevents repeating past mistakes
- ✅ Great onboarding documentation
- ✅ Facilitates informed refactoring

**Example from ADR01:**
> "**Constraint:** Bool Builder Last to Be Injected - The BoolInstructionArgumentBuilder must be registered last in the DI container because it acts as a fallback."

**This saves hours of debugging!**

---

### 2. Testability by Design

**Built for Testing from Day One:**

```csharp
// Abstraction enables testing
public interface ICliCommandOutcomeIo
{
    string? Ask();
    void Say(CliCommandOutcome[] outcomes);
}

// Test implementation
public class TestCliCommandOutcomeIo : ICliCommandOutcomeIo
{
    private readonly Queue<string> _testInputs = new();
    private readonly List<CliCommandOutcome[]> _testOutputs = new();
    
    public string? Ask() => _testInputs.Dequeue();
    public void Say(CliCommandOutcome[] outcomes) => _testOutputs.Add(outcomes);
}

// Test becomes trivial
[Fact]
public async Task Should_Process_User_Input()
{
    var io = new TestCliCommandOutcomeIo();
    io.EnqueueInput("/exit");
    
    var app = new SpendfulnessCliApp(workflow, io);
    await app.Run();
    
    Assert.Equal("Exiting...", io.GetOutput());
}
```

**Test Projects:**
- `Cli.Abstractions.Tests`
- `Cli.Commands.Abstractions.Tests`
- `Cli.Tests`
- `Cli.Workflow.Tests`
- `Cli.Workflow.IntegrationTests`
- Multiple domain-specific test projects

---

### 3. Type Safety Throughout

**Strongly-Typed Command Pipeline:**

```csharp
// Input: String
"/filter-transactions --payee-name Amazon"

// ↓ Parser converts to typed instruction
ConsoleInstruction {
    Name = "filter-transactions",
    Arguments = [
        TypedConsoleInstructionArgument<string>("payee-name", "Amazon")
    ]
}

// ↓ Generator creates typed command
FilterTransactionsOnPayeeNameEqualsCliCommand {
    PayeeName = "Amazon"
}

// ↓ Handler receives typed command
Task<CliCommandOutcome[]> Handle(
    FilterTransactionsOnPayeeNameEqualsCliCommand command
)

// ✅ No string parsing in handlers
// ✅ Compile-time type checking
// ✅ IntelliSense support
// ✅ Refactoring-safe
```

---

### 4. Modular Project Structure

**41 Projects organized by concern:**

```
CLI Framework (Reusable):
├── Cli                          // Core CLI application
├── Cli.Abstractions             // Base abstractions
├── Cli.Commands.Abstractions    // Command pattern
├── Cli.Instructions             // Instruction parsing
├── Cli.Workflow                 // Workflow state machine

Domain (Spendfulness-specific):
├── SpendfulnessCli              // Main application
├── SpendfulnessCli.Commands.*   // Domain commands
├── Spendfulness.Database.*      // Data access
├── Spendfulness.Aggregation     // Business logic

Integration:
├── Ynab                         // External API client
├── Spendfulness.OpenAI          // AI integration
```

**Benefits:**
- ✅ Clear boundaries between layers
- ✅ Reusable CLI framework
- ✅ Independent deployment/testing
- ✅ Parallel development possible

---

### 5. The Power of Records

```csharp
// ✅ Commands as immutable records
public record FilterTransactionsOnPayeeNameEqualsCliCommand(
    string PayeeName
) : CliCommand;

// ✅ Aggregates as immutable records
public record TransactionYearAverageAggregate(
    string Year,
    decimal AverageAmount,
    int PercentageChange
);
```

**Record Benefits:**
- Immutable by default (thread-safe)
- Value-based equality (great for testing)
- Concise syntax (less boilerplate)
- Structural pattern matching
- Perfect for data carriers

---

### 6. Fail-Fast Philosophy

```csharp
// ✅ Validate state transitions
private ClIWorkflowRunStateType CanChangeTo(ClIWorkflowRunStateType newState)
{
    if (!IsValidTransition(currentState, newState))
    {
        throw new ImpossibleStateChangeException(
            $"Cannot transition from {currentState} to {newState}"
        );
    }
    return currentState;
}

// ✅ Explicit exceptions for invalid operations
if (generator == null)
{
    throw new NoCommandGeneratorException(
        "Did not find generator for " + instruction.Name
    );
}
```

**Benefits:**
- ✅ Bugs caught early in development
- ✅ Clear error messages
- ✅ State machine integrity guaranteed
- ✅ Easier debugging

---

### 7. Extension Methods for Readability

```csharp
// ✅ Fluent, readable code
public string GetInstructionName()
    => GetType().Name
        .ReplaceCommandSuffix()
        .ToLowerSplitString(CliInstructionConstants.DefaultCommandNameSeparator);

// Instead of:
public string GetInstructionName()
{
    var name = GetType().Name;
    name = name.Replace("CliCommand", string.Empty);
    var parts = SplitOnUpperCase(name);
    return string.Join("-", parts).ToLower();
}
```

**Readability Wins:**
- Method names describe intent
- Chainable operations
- Less noise, more signal
- Easier to understand at a glance

---

## 8. Q&A
**Duration: 2 minutes**

### Common Questions

**Q: Why build a custom CLI framework instead of using an existing library?**
- **A:** Full control over architecture, perfect for learning/teaching SOLID principles, zero external dependencies, exactly fits the problem domain

**Q: Isn't 41 projects over-engineered?**
- **A:** Each project has single responsibility, enables parallel development, clear boundaries, reusable components. It's a teaching project demonstrating enterprise patterns.

**Q: How do you handle breaking changes?**
- **A:** ADRs document constraints, interfaces provide stability points, good test coverage catches regressions

**Q: What would you do differently?**
- **A:** Some potential improvements documented in ADRs (e.g., ADR01 DI ordering, ADR09 stopwatch logic). This is intentional - perfect is enemy of good.

**Q: How do you onboard new developers?**
- **A:** Start with ADRs → Read CONCEPTS.md → Explore test projects → Implement a simple command

---

## Summary: Key Takeaways

### Architecture Wins
1. **Three-tier state machine** separates concerns perfectly
2. **Plugin-based extensibility** via DI and factories
3. **Type-safe pipeline** from string input to typed commands
4. **Comprehensive ADRs** document all major decisions

### SOLID Application
- **SRP:** Every class has one reason to change
- **OCP:** Extend through DI, never modify core
- **LSP:** All abstractions are truly substitutable
- **ISP:** Focused interfaces, no bloat
- **DIP:** Depend on abstractions everywhere

### DRY Benefits
- Aggregators eliminate data transformation duplication
- Base classes provide common functionality
- Extension methods centralize utilities
- Constants ensure single source of truth

### YAGNI Discipline
- Simple state machines (just what's needed)
- Minimal interfaces (Ask/Say, Start/Stop)
- Features added incrementally as required
- Document constraints, not premature solutions

### What Makes This Special
- **Real-world complexity** (not a toy example)
- **Production-quality** architecture
- **Comprehensive documentation** (ADRs, comments)
- **Teaching tool** for modern C# and design patterns
- **Extensible foundation** for future features

---

## References

### Repository
- **GitHub:** KitCli/SpendfulnessCli
- **Architecture Docs:** `/ADR` directory
- **Concepts:** `CONCEPTS.md`

### Key ADRs to Read
1. **ADR07** - Instruction Parser (three-stage pipeline)
2. **ADR08** - CLI Concept (main loop design)
3. **ADR09** - Workflow Concept (state machines)
4. **ADR10** - Command Properties (inter-command communication)

### Further Learning
- MediatR CQRS pattern
- Template Method Pattern
- State Machine Pattern
- Dependency Injection in .NET
- Domain-Driven Design principles

---

## Thank You!

**Questions? Discussion?**

*"Good architecture is less about perfection and more about making the right trade-offs explicit."*

**Contact:** [Your Email/GitHub]
**Repository:** https://github.com/KitCli/SpendfulnessCli
